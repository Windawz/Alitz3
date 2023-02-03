using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public interface IColumn<TComponent> : IColumn, IReadOnlyDictionary<Entity, TComponent> where TComponent : struct {
    new TComponent this[Entity entity] { get; set; }
    TComponent IReadOnlyDictionary<Entity, TComponent>.this[Entity entity] {
        get => this[entity];
    }

    new Type Type => typeof(TComponent);
    IEnumerable<Entity> Entities { get; }
    IEnumerable<TComponent> Components { get; }
    Type IColumn.Type => Type;
    IEnumerable<Entity> IReadOnlyDictionary<Entity, TComponent>.Keys => Entities;
    IEnumerable<TComponent> IReadOnlyDictionary<Entity, TComponent>.Values => Components;

    void Add(Entity entity, TComponent component);
    bool TryAdd(Entity entity, TComponent component);
    bool Remove(Entity entity);
    bool ContainsEntity(Entity entity);
    bool TryGetComponent(Entity entity, [MaybeNullWhen(false)] out TComponent component);
    ref TComponent GetByRef(Entity entity);
    bool IReadOnlyDictionary<Entity, TComponent>.ContainsKey(Entity key) =>
        ContainsEntity(key);
    bool IReadOnlyDictionary<Entity, TComponent>.TryGetValue(Alitz3.Ecs.Entity key, [MaybeNullWhen(false)] out TComponent value) =>
        TryGetComponent(key, out value);
}
