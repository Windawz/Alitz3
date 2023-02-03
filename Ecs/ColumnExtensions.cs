using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public static class ColumnExtensions {
    public delegate void ColumnForEachAction<T1>(ref T1 component) where T1 : struct;
    
    public delegate void ColumnForEachAction<T1, T2>(
        ref T1 c1, ref T2 c2)
        where T1 : struct where T2 : struct;
    
    public delegate void ColumnForEachAction<T1, T2, T3>(
        ref T1 c1, ref T2 c2, ref T3 c3)
        where T1 : struct where T2 : struct where T3 : struct;
    
    public delegate void ColumnForEachAction<T1, T2, T3, T4>(
        ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4)
        where T1 : struct where T2 : struct where T3 : struct where T4 : struct;
    
    public delegate void ColumnForEachAction<T1, T2, T3, T4, T5>(
        ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5)
        where T1 : struct where T2 : struct where T3 : struct where T4 : struct where T5 : struct;
    
    public delegate void ColumnForEachAction<T1, T2, T3, T4, T5, T6>(
        ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6)
        where T1 : struct where T2 : struct where T3 : struct where T4 : struct where T5 : struct where T6 : struct;
    
    public delegate void ColumnForEachAction<T1, T2, T3, T4, T5, T6, T7>(
        ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7)
        where T1 : struct where T2 : struct where T3 : struct where T4 : struct where T5 : struct where T6 : struct where T7 : struct;
    
    public delegate void ColumnForEachAction<T1, T2, T3, T4, T5, T6, T7, T8>(
        ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7, ref T8 c8)
        where T1 : struct where T2 : struct where T3 : struct where T4 : struct where T5 : struct where T6 : struct where T7 : struct where T8 : struct;
    
    public delegate void ColumnForEachActionWithEntity<T1>(Entity entity, ref T1 component) where T1 : struct;
    
    public delegate void ColumnForEachActionWithEntity<T1, T2>(
        Entity entity, ref T1 c1, ref T2 c2)
        where T1 : struct where T2 : struct;
    
    public delegate void ColumnForEachActionWithEntity<T1, T2, T3>(
        Entity entity, ref T1 c1, ref T2 c2, ref T3 c3)
        where T1 : struct where T2 : struct where T3 : struct;
    
    public delegate void ColumnForEachActionWithEntity<T1, T2, T3, T4>(
        Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4)
        where T1 : struct where T2 : struct where T3 : struct where T4 : struct;
    
    public delegate void ColumnForEachActionWithEntity<T1, T2, T3, T4, T5>(
        Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5)
        where T1 : struct where T2 : struct where T3 : struct where T4 : struct where T5 : struct;
    
    public delegate void ColumnForEachActionWithEntity<T1, T2, T3, T4, T5, T6>(
        Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6)
        where T1 : struct where T2 : struct where T3 : struct where T4 : struct where T5 : struct where T6 : struct;
    
    public delegate void ColumnForEachActionWithEntity<T1, T2, T3, T4, T5, T6, T7>(
        Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7)
        where T1 : struct where T2 : struct where T3 : struct where T4 : struct where T5 : struct where T6 : struct where T7 : struct;
    
    public delegate void ColumnForEachActionWithEntity<T1, T2, T3, T4, T5, T6, T7, T8>(
        Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7, ref T8 c8)
        where T1 : struct where T2 : struct where T3 : struct where T4 : struct where T5 : struct where T6 : struct where T7 : struct where T8 : struct;

    public static void ForEach<T1>(this IColumn<T1> column, ColumnForEachAction<T1> action)
        where T1 : struct {
        foreach (Entity entity in column.Entities) {
            action(ref column.GetByRef(entity));
        }
    }

    public static void ForEach<T1, T2>(this (IColumn<T1>, IColumn<T2>) columns, ColumnForEachAction<T1, T2> action)
        where T1 : struct
        where T2 : struct {
        var entities = columns.Item1.Entities
            .Intersect(columns.Item2.Entities);
        foreach (Entity entity in entities) {
            action(
                ref columns.Item1.GetByRef(entity),
                ref columns.Item2.GetByRef(entity));
        }
    }

    public static void ForEach<T1, T2, T3>(this (IColumn<T1>, IColumn<T2>, IColumn<T3>) columns, ColumnForEachAction<T1, T2, T3> action)
        where T1 : struct
        where T2 : struct
        where T3 : struct {
        var entities = columns.Item1.Entities
            .Intersect(columns.Item2.Entities)
            .Intersect(columns.Item3.Entities);
        foreach (Entity entity in entities) {
            action(
                ref columns.Item1.GetByRef(entity),
                ref columns.Item2.GetByRef(entity),
                ref columns.Item3.GetByRef(entity));
        }
    }

    public static void ForEach<T1, T2, T3, T4>(this (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>) columns, ColumnForEachAction<T1, T2, T3, T4> action)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct {
        var entities = columns.Item1.Entities
            .Intersect(columns.Item2.Entities)
            .Intersect(columns.Item3.Entities)
            .Intersect(columns.Item4.Entities);
        foreach (Entity entity in entities) {
            action(
                ref columns.Item1.GetByRef(entity),
                ref columns.Item2.GetByRef(entity),
                ref columns.Item3.GetByRef(entity),
                ref columns.Item4.GetByRef(entity));
        }
    }

    public static void ForEach<T1, T2, T3, T4, T5>(this (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>, IColumn<T5>) columns, ColumnForEachAction<T1, T2, T3, T4, T5> action)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct {
        var entities = columns.Item1.Entities
            .Intersect(columns.Item2.Entities)
            .Intersect(columns.Item3.Entities)
            .Intersect(columns.Item4.Entities)
            .Intersect(columns.Item5.Entities);
        foreach (Entity entity in entities) {
            action(
                ref columns.Item1.GetByRef(entity),
                ref columns.Item2.GetByRef(entity),
                ref columns.Item3.GetByRef(entity),
                ref columns.Item4.GetByRef(entity),
                ref columns.Item5.GetByRef(entity));
        }
    }

    public static void ForEach<T1, T2, T3, T4, T5, T6>(this (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>, IColumn<T5>, IColumn<T6>) columns, ColumnForEachAction<T1, T2, T3, T4, T5, T6> action)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct {
        var entities = columns.Item1.Entities
            .Intersect(columns.Item2.Entities)
            .Intersect(columns.Item3.Entities)
            .Intersect(columns.Item4.Entities)
            .Intersect(columns.Item5.Entities)
            .Intersect(columns.Item6.Entities);
        foreach (Entity entity in entities) {
            action(
                ref columns.Item1.GetByRef(entity),
                ref columns.Item2.GetByRef(entity),
                ref columns.Item3.GetByRef(entity),
                ref columns.Item4.GetByRef(entity),
                ref columns.Item5.GetByRef(entity),
                ref columns.Item6.GetByRef(entity));
        }
    }

    public static void ForEach<T1, T2, T3, T4, T5, T6, T7>(this (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>, IColumn<T5>, IColumn<T6>, IColumn<T7>) columns, ColumnForEachAction<T1, T2, T3, T4, T5, T6, T7> action)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct {
        var entities = columns.Item1.Entities
            .Intersect(columns.Item2.Entities)
            .Intersect(columns.Item3.Entities)
            .Intersect(columns.Item4.Entities)
            .Intersect(columns.Item5.Entities)
            .Intersect(columns.Item6.Entities)
            .Intersect(columns.Item7.Entities);
        foreach (Entity entity in entities) {
            action(
                ref columns.Item1.GetByRef(entity),
                ref columns.Item2.GetByRef(entity),
                ref columns.Item3.GetByRef(entity),
                ref columns.Item4.GetByRef(entity),
                ref columns.Item5.GetByRef(entity),
                ref columns.Item6.GetByRef(entity),
                ref columns.Item7.GetByRef(entity));
        }
    }

    public static void ForEach<T1, T2, T3, T4, T5, T6, T7, T8>(this (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>, IColumn<T5>, IColumn<T6>, IColumn<T7>, IColumn<T8>) columns, ColumnForEachAction<T1, T2, T3, T4, T5, T6, T7, T8> action)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
        where T8 : struct {
        var entities = columns.Item1.Entities
            .Intersect(columns.Item2.Entities)
            .Intersect(columns.Item3.Entities)
            .Intersect(columns.Item4.Entities)
            .Intersect(columns.Item5.Entities)
            .Intersect(columns.Item6.Entities)
            .Intersect(columns.Item7.Entities)
            .Intersect(columns.Item8.Entities);
        foreach (Entity entity in entities) {
            action(
                ref columns.Item1.GetByRef(entity),
                ref columns.Item2.GetByRef(entity),
                ref columns.Item3.GetByRef(entity),
                ref columns.Item4.GetByRef(entity),
                ref columns.Item5.GetByRef(entity),
                ref columns.Item6.GetByRef(entity),
                ref columns.Item7.GetByRef(entity),
                ref columns.Item8.GetByRef(entity));
        }
    }

    public static void ForEach<T1>(this IColumn<T1> column, ColumnForEachActionWithEntity<T1> action)
        where T1 : struct {
        foreach (Entity entity in column.Entities) {
            action(
                entity,
                ref column.GetByRef(entity));
        }
    }

    public static void ForEach<T1, T2>(this (IColumn<T1>, IColumn<T2>) columns, ColumnForEachActionWithEntity<T1, T2> action)
        where T1 : struct
        where T2 : struct {
        var entities = columns.Item1.Entities
            .Intersect(columns.Item2.Entities);
        foreach (Entity entity in entities) {
            action(
                entity,
                ref columns.Item1.GetByRef(entity),
                ref columns.Item2.GetByRef(entity));
        }
    }

    public static void ForEach<T1, T2, T3>(this (IColumn<T1>, IColumn<T2>, IColumn<T3>) columns, ColumnForEachActionWithEntity<T1, T2, T3> action)
        where T1 : struct
        where T2 : struct
        where T3 : struct {
        var entities = columns.Item1.Entities
            .Intersect(columns.Item2.Entities)
            .Intersect(columns.Item3.Entities);
        foreach (Entity entity in entities) {
            action(
                entity,
                ref columns.Item1.GetByRef(entity),
                ref columns.Item2.GetByRef(entity),
                ref columns.Item3.GetByRef(entity));
        }
    }

    public static void ForEach<T1, T2, T3, T4>(this (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>) columns, ColumnForEachActionWithEntity<T1, T2, T3, T4> action)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct {
        var entities = columns.Item1.Entities
            .Intersect(columns.Item2.Entities)
            .Intersect(columns.Item3.Entities)
            .Intersect(columns.Item4.Entities);
        foreach (Entity entity in entities) {
            action(
                entity,
                ref columns.Item1.GetByRef(entity),
                ref columns.Item2.GetByRef(entity),
                ref columns.Item3.GetByRef(entity),
                ref columns.Item4.GetByRef(entity));
        }
    }

    public static void ForEach<T1, T2, T3, T4, T5>(this (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>, IColumn<T5>) columns, ColumnForEachActionWithEntity<T1, T2, T3, T4, T5> action)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct {
        var entities = columns.Item1.Entities
            .Intersect(columns.Item2.Entities)
            .Intersect(columns.Item3.Entities)
            .Intersect(columns.Item4.Entities)
            .Intersect(columns.Item5.Entities);
        foreach (Entity entity in entities) {
            action(
                entity,
                ref columns.Item1.GetByRef(entity),
                ref columns.Item2.GetByRef(entity),
                ref columns.Item3.GetByRef(entity),
                ref columns.Item4.GetByRef(entity),
                ref columns.Item5.GetByRef(entity));
        }
    }

    public static void ForEach<T1, T2, T3, T4, T5, T6>(this (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>, IColumn<T5>, IColumn<T6>) columns, ColumnForEachActionWithEntity<T1, T2, T3, T4, T5, T6> action)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct {
        var entities = columns.Item1.Entities
            .Intersect(columns.Item2.Entities)
            .Intersect(columns.Item3.Entities)
            .Intersect(columns.Item4.Entities)
            .Intersect(columns.Item5.Entities)
            .Intersect(columns.Item6.Entities);
        foreach (Entity entity in entities) {
            action(
                entity,
                ref columns.Item1.GetByRef(entity),
                ref columns.Item2.GetByRef(entity),
                ref columns.Item3.GetByRef(entity),
                ref columns.Item4.GetByRef(entity),
                ref columns.Item5.GetByRef(entity),
                ref columns.Item6.GetByRef(entity));
        }
    }

    public static void ForEach<T1, T2, T3, T4, T5, T6, T7>(this (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>, IColumn<T5>, IColumn<T6>, IColumn<T7>) columns, ColumnForEachActionWithEntity<T1, T2, T3, T4, T5, T6, T7> action)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct {
        var entities = columns.Item1.Entities
            .Intersect(columns.Item2.Entities)
            .Intersect(columns.Item3.Entities)
            .Intersect(columns.Item4.Entities)
            .Intersect(columns.Item5.Entities)
            .Intersect(columns.Item6.Entities)
            .Intersect(columns.Item7.Entities);
        foreach (Entity entity in entities) {
            action(
                entity,
                ref columns.Item1.GetByRef(entity),
                ref columns.Item2.GetByRef(entity),
                ref columns.Item3.GetByRef(entity),
                ref columns.Item4.GetByRef(entity),
                ref columns.Item5.GetByRef(entity),
                ref columns.Item6.GetByRef(entity),
                ref columns.Item7.GetByRef(entity));
        }
    }

    public static void ForEach<T1, T2, T3, T4, T5, T6, T7, T8>(this (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>, IColumn<T5>, IColumn<T6>, IColumn<T7>, IColumn<T8>) columns, ColumnForEachActionWithEntity<T1, T2, T3, T4, T5, T6, T7, T8> action)
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
        where T8 : struct {
        var entities = columns.Item1.Entities
            .Intersect(columns.Item2.Entities)
            .Intersect(columns.Item3.Entities)
            .Intersect(columns.Item4.Entities)
            .Intersect(columns.Item5.Entities)
            .Intersect(columns.Item6.Entities)
            .Intersect(columns.Item7.Entities)
            .Intersect(columns.Item8.Entities);
        foreach (Entity entity in entities) {
            action(
                entity,
                ref columns.Item1.GetByRef(entity),
                ref columns.Item2.GetByRef(entity),
                ref columns.Item3.GetByRef(entity),
                ref columns.Item4.GetByRef(entity),
                ref columns.Item5.GetByRef(entity),
                ref columns.Item6.GetByRef(entity),
                ref columns.Item7.GetByRef(entity),
                ref columns.Item8.GetByRef(entity));
        }
    }

    public static TComponent? TryGetComponent<TComponent>(this IColumn<TComponent> column, Entity entity)
        where TComponent : struct {
        if (column.TryGetComponent(entity, out var component)) {
            return component;
        } else {
            return null;
        }
    }
}
