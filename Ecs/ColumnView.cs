using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
internal class ColumnView<TComponent> : IColumn<TComponent> where TComponent : struct {
    public ColumnView(IColumn<TComponent> column, Func<Entity, bool> predicate) {
        _column = column;
        _viewedEntities = new HashSet<Entity>(column.Keys.Where(predicate));
    }

    private readonly IColumn<TComponent> _column;
    private readonly ISet<Entity> _viewedEntities;

    public IEnumerable<Entity> Entities => _column
        .Keys
        .Where(k => _viewedEntities.Contains(k));

    public IEnumerable<TComponent> Components => _column
        .Where(kv => _viewedEntities.Contains(kv.Key))
        .Select(kv => kv.Value);

    public int Count =>
        _viewedEntities.Count;

    public TComponent this[Entity entity] {
        get {
            if (!_viewedEntities.Contains(entity)) {
                throw new ArgumentOutOfRangeException(nameof(entity));
            }
            return _column[entity];
        }
        set {
            if (!_viewedEntities.Contains(entity)) {
                throw new ArgumentOutOfRangeException(nameof(entity));
            }
            _column[entity] = value;
        }
    }

    public void Add(Entity entity, TComponent component) {
        _column.Add(entity, component);
        _viewedEntities.Add(entity);
    }
    
    public bool TryAdd(Entity entity, TComponent component) {
        if (!_column.TryAdd(entity, component)) {
            return false;
        }
        _viewedEntities.Add(entity);
        return true;
    }

    public bool Remove(Entity entity) {
        if (_viewedEntities.Remove(entity)) {
            _column.Remove(entity);
            return true;
        } else {
            return false;
        }
    }

    public bool ContainsEntity(Entity entity) =>
        _viewedEntities.Contains(entity);

    public bool TryGetComponent(Entity entity, [MaybeNullWhen(false)] out TComponent component) {
        if (_viewedEntities.Contains(entity)) {
            component = _column[entity];
            return true;
        } else {
            component = default;
            return false;
        }
    }

    public ref TComponent GetByRef(Entity entity) {
        if (!_viewedEntities.Contains(entity)) {
            throw new ArgumentOutOfRangeException(nameof(entity));
        }
        return ref _column.GetByRef(entity);
    }

    public IEnumerator<KeyValuePair<Entity, TComponent>> GetEnumerator() {
        var kvs = _column.Where(kv => _viewedEntities.Contains(kv.Key));
        foreach (var kv in kvs) {
            yield return kv;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}
