using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Alitz3.Collections;

namespace Alitz3.Ecs;
public class SparseColumn<TComponent> : IColumn<TComponent> where TComponent : struct {
    private AlitzList<Entity> _dense = new();
    private AlitzList<Entity> _sparse = new();
    private AlitzList<TComponent> _components = new();

    public TComponent this[Entity entity] {
        get {
            int index = TryGetDenseIndexVersioned(entity)
                ?? throw new ArgumentOutOfRangeException(nameof(entity));

            return _components[index];
        }

        set {
            int? maybeIndex = TryGetDenseIndexVersioned(entity);
            switch (maybeIndex) {
                case null:
                    Add(entity, value);
                    break;
                default:
                    int index = (int)maybeIndex;
                    _components[index] = value;
                    break;
            }
        }
    }

    public IEnumerable<Entity> Entities => _dense;
    public IEnumerable<TComponent> Components => _components;
    public int Count => _dense.Count;

    public void Add(Entity entity, TComponent component) {
        if (!TryAdd(entity, component)) {
            throw new ArgumentException("Entity already exists", nameof(entity));
        }
    }

    public bool TryAdd(Entity entity, TComponent component) {
        if (TryGetDenseIndexVersioned(entity) is not null) {
            return false;
        }

        int sparseIndex = ConvertToIndex(entity);
        int pos = _dense.Count;

        _dense.Add(entity);
        _components.Add(component);

        _sparse.EnsureCount(sparseIndex + 1, Entity.Null);
        // Sparse vector holds "fake" entities used as indices.
        // Their Id does not make sense and is only useful
        // as an index into the dense vector. Hence the conversion
        // of pos to an Id.
        // 
        // They're also used for their Null value to mark unoccupied spots in the sparse array.
        _sparse[sparseIndex] = new Entity(ConvertToId(pos), 0);

        return true;
    }

    public bool ContainsEntity(Entity entity) {
        return TryGetDenseIndexVersioned(entity) is null
            ? false
            : true;
    }

    public IEnumerator<KeyValuePair<Entity, TComponent>> GetEnumerator() {
        for (int i = 0; i < _dense.Count; i++) {
            yield return new(_dense[i], _components[i]);
        }
    }

    public bool Remove(Entity entity) {
        ValidateEntityForNull(entity);

        int sparseIndex = ConvertToIndex(entity);
        int? maybeDenseIndex = TryGetDenseIndexVersionless(sparseIndex);
        if (maybeDenseIndex is null) {
            return false;
        }

        int lastDenseElement = ConvertToIndex(_dense[_dense.Count - 1]);

        Swap(_dense, _dense.Count - 1, (int)maybeDenseIndex);
        Swap(_components, _components.Count - 1, (int)maybeDenseIndex);

        Swap(_sparse, lastDenseElement, sparseIndex);

        _dense.RemoveAt(_dense.Count - 1);
        _components.RemoveAt(_dense.Count - 1);

        _sparse[sparseIndex] = Entity.Null;

        return true;
    }

    public bool TryGetComponent(Entity entity, [MaybeNullWhen(false)] out TComponent component) {
        int? maybeDenseIndex = TryGetDenseIndexVersioned(entity);
        if (maybeDenseIndex is null) {
            component = default;
            return false;
        } else {
            component = _components[(int)maybeDenseIndex];
            return true;
        }
    }

    public ref TComponent GetByRef(Entity entity) {
        int denseIndex = TryGetDenseIndexVersioned(entity)
            ?? throw new ArgumentOutOfRangeException(nameof(entity));
        return ref _components[denseIndex];
    }

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    private int? TryGetDenseIndexVersionless(int sparseIndex) {
        if (sparseIndex >= _sparse.Count) {
            return null;
        }
        Entity sparseElement = _sparse[sparseIndex];
        if (sparseElement.IsNull) {
            return null;
        } else {
            return ConvertToIndex(sparseElement);
        }
    }

    private int? TryGetDenseIndexVersioned(Entity indexEntity) {
        ValidateEntityForNull(indexEntity);

        int sparseIndex = ConvertToIndex(indexEntity);
        int? maybeIndexIntoDense = TryGetDenseIndexVersionless(sparseIndex);
        if (maybeIndexIntoDense is null) {
            return null;
        }

        int indexIntoDense = (int)maybeIndexIntoDense;
        Entity denseElement = _dense[indexIntoDense];
        if (denseElement == indexEntity) {
            return indexIntoDense;
        } else {
            return null;
        }
    }

    private static void ValidateEntityForNull(Entity entity) {
        if (entity.IsNull) {
            throw new ArgumentException("Entity may not be null");
        }
    }

    private static int ConvertToIndex(Entity entity) =>
        Entity.UnderlyingType.ToInt32(entity.Id);

    private static Entity.UnderlyingType ConvertToId(int index) =>
        index < 0
        ? throw new ArgumentOutOfRangeException(nameof(index))
        : Entity.UnderlyingType.FromInt32(index);

    private static void Swap<T>(IList<T> list, int i, int j) =>
        (list[i], list[j]) = (list[j], list[i]);

    /*
     * =============================
     * On implementing sparse arrays
     * =============================
     * 
     * abc — Today at 18:49
     * the "sparse" part of a sparse array is just a collection of integers that map into the compacted array
     * the entity number (read: not the version) is used to index into that sparse array
     * if its not a valid index in the compacted array, you know the component isn't attached to a specific entity
     * you do store the entities that have the components in an array inside the sparse set
     * but you don't store it in sparse
     * you store it in an entirely separate array
     * 
     * abc — Today at 18:52
     * so you have the "dense" array which holds the actual component data
     * you have a list of entities that have the component
     * and that list has to share the same index with the dense array
     * such that if i index the list of entities, i can use the same index to get its component data
     * the reason you need this is to iterate the component pools in a query
     * and finally you have the sparse array which just stores indices into the dense array
     * and is indexed with an entity (but not the version part of it)
     * and to avoid running out of memory, you break the sparse into chunks of indices
     * rather than it being one monolithic allocation
     * so if you have 40 bits for your entity id
     * whatever the largest number you can store in 40 bits divided by the page size
     * 
     * abc — Today at 19:03
     * ```
     * const PAGE_SIZE = 4096;
     * 
     * struct ComponentIndexPage {
     *     int indices[PAGE_SIZE];
     * };
     *
     * template<class T> struct ComponentPool {
     *     array<T> componentValues;
     *     array<Entity> assignedEntities;
     *     ComponentIndexPage indexPages[EntityIndex::Max / PAGE_SIZE];
     * }
     * ```
     *
     * something like that
     * 
     * abc — Today at 19:09
     * and so when you query, you can just find the pool with the smallest assignedEntities array
     * and work your way up
     * 
     * abc — Today at 19:09
     * and so when you query, you can just find the pool with the smallest assignedEntities array
     * and work your way up
     * also this kind of ECS shines when you have a bunch of events firing which tends to happen in roguelikes
     * theres a few different ways you can actually do it
     * archetype based ECS have faster iteration speed so they'd be a better fit for simpler games
     * 
     * lambada impression — Today at 19:19
     * haven't got to that level yet
     * i need to make my own vector now lmao
     * actually
     * any component is at least 4 bytes
     * the CLR, and the process, have a memory allocation limit
     * so I'll never actually need a vector whose length and indexing operations are a 64 bit uint 
     * i need to change the Entity layout
     * shrink version to 16 bits
     * shrink entity id to 16 bits
     * 32 bits total
     * maybe 12 bits for version and 20 for id
     * i mean, what have i got to lose anyway
     * 
     * abc — Today at 19:37
     * but the only large array is indexPages
     * and that was divided into chunks of 4096
     * obviously you can run yourself out of memory but that can happen in many other game object architectures
     * really unlikely
     * everything else is dense
     * its not likely there are actually EntityIndex::Max values in a given component pool
     * its likely MUCH smaller
     * but theoretically the entity number can grow
     * but thats solved by paging
     * 
     * lambada impression — Today at 19:40
     * I just don't want to trigger an OutOfMemoryException by accident
     * 
     * abc — Today at 19:41
     * if that happens you have a different problem
     * 
     * lambada impression — Today at 19:41
     * seems like even if you allocate an array of 32 bit ints of length Int32.MaxValue, you can get one
     * 
     * abc — Today at 19:41
     * if you managed to trigger that with the paging policy in place it means one of two things happened
     * you had entity ids in increments of the PAGE_SIZE being given components and causing page waste
     * modify the size of a page to fix that or figure out why the allocations are occuring in such a way
     * or you just have a shitload of entities
     * you have to start loading stuff on/off a hard drive at that point
     * similar to how a database doesn't keep all its values in at once
     */
}
