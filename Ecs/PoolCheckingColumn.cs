using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
internal class PoolCheckingColumn<TComponent> : IColumn<TComponent> where TComponent : struct {
    public PoolCheckingColumn(IColumn<TComponent> column, EntityPool entityPool) {
        _column = column;
        _entityPool = entityPool;
    }

    private readonly IColumn<TComponent> _column;
    private readonly EntityPool _entityPool;

    public TComponent this[Entity entity] {
        get => _column[entity];
        set => _column[entity] = value;
    }

    public IEnumerable<Entity> Keys => _column.Keys;
    public IEnumerable<TComponent> Values => _column.Values;
    public int Count => _column.Count;

    public void Add(Entity entity, TComponent component) {
        if (!_entityPool.Contains(entity)) {
            throw new ArgumentException("Entity does not exist in associated entity pool", nameof(entity));
        }
        _column.Add(entity, component);
    }

    public bool ContainsKey(Entity key) => _column.ContainsKey(key);
    public IEnumerator<KeyValuePair<Entity, TComponent>> GetEnumerator() => _column.GetEnumerator();
    public bool Remove(Entity entity) => _column.Remove(entity);
    public bool TryGetValue(Entity key, [MaybeNullWhen(false)] out TComponent value) => _column.TryGetValue(key, out value);
    IEnumerator IEnumerable.GetEnumerator() => _column.GetEnumerator();
}
