using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public class ComponentCollection : IEnumerable<KeyValuePair<Type, IColumn>> {
    public ComponentCollection(IColumnFactory columnFactory) {
        _dict = new Dictionary<Type, IColumn>();
        ColumnFactory = columnFactory;
    }

    private readonly IDictionary<Type, IColumn> _dict;

    public IEnumerable<Type> ComponentTypes => _dict.Keys;
    public IEnumerable<IColumn> Columns => _dict.Values;
    public int Count => _dict.Count;
    public IColumnFactory ColumnFactory { get; set; }

    public IColumn<TComponent> Column<TComponent>() where TComponent : struct {
        var componentType = typeof(TComponent);
        if (_dict.TryGetValue(componentType, out var existingColumn)) {
            return (IColumn<TComponent>)existingColumn;
        } else {
            var newColumn = ColumnFactory.Create<TComponent>();
            _dict.Add(componentType, newColumn);
            return newColumn;
        }
    }

    public void Clear() =>
        _dict.Clear();

    public bool Contains(Type componentType) =>
        _dict.ContainsKey(componentType);

    public IEnumerator<KeyValuePair<Type, IColumn>> GetEnumerator() =>
        _dict.GetEnumerator();
    
    public bool Remove(Type componentType) =>
        _dict.Remove(componentType);

    public bool TryGetColumn(Type componentType, [MaybeNullWhen(false)] out IColumn column) =>
        _dict.TryGetValue(componentType, out column);

    IEnumerator IEnumerable.GetEnumerator() => _dict.GetEnumerator();
}
