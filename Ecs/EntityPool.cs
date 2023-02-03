using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public class EntityPool : IEnumerable<Entity> {
    private readonly List<Entity> _entities = new();
    private readonly Stack<(int, Entity.UnderlyingType)> _sparePositionsAndIds = new();

    public Entity Add() {
        Entity entity;
        if (_entities.Count == 0) {
            entity = new(0, 0);
            _entities.Add(entity);
        } else if (_sparePositionsAndIds.Count > 0) {
            var (i, id) = _sparePositionsAndIds.Pop();
            Entity.UnderlyingType version = checked(_entities[i].Version + 1);
            entity = new(id, version);
            _entities[i] = entity;
        } else {
            Entity.UnderlyingType id = checked(_entities.Last().Id + 1);
            entity = new(id, 0);
            _entities.Add(entity);
        }
        return entity;
    }

    public bool Remove(Entity entity) {
        int i = _entities.IndexOf(entity);
        if (i == -1) {
            return false;
        }
        _sparePositionsAndIds.Push((i, _entities[i].Id));
        _entities[i] = new Entity(Entity.NullId, _entities[i].Version);
        return true;
    }

    public bool Contains(Entity entity) {
        return _entities.Contains(entity);
    }

    public IEnumerator<Entity> GetEnumerator() =>
        _entities.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}
