using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public class Querier {
    public Querier(Ecs ecs) {
        _ecs = ecs;
    }

    private readonly Ecs _ecs;

    public IColumn<T1> Get<T1>()
        where T1 : struct {
        return _ecs.Components.Column<T1>();
    }

    public (IColumn<T1>, IColumn<T2>) Get<T1, T2>()
        where T1 : struct
        where T2 : struct {
        return (
            _ecs.Components.Column<T1>(),
            _ecs.Components.Column<T2>());
    }

    public (IColumn<T1>, IColumn<T2>, IColumn<T3>) Get<T1, T2, T3>()
        where T1 : struct
        where T2 : struct
        where T3 : struct {
        return (
            _ecs.Components.Column<T1>(),
            _ecs.Components.Column<T2>(),
            _ecs.Components.Column<T3>());
    }

    public (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>) Get<T1, T2, T3, T4>()
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct {
        return (
            _ecs.Components.Column<T1>(),
            _ecs.Components.Column<T2>(),
            _ecs.Components.Column<T3>(),
            _ecs.Components.Column<T4>());
    }

    public (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>, IColumn<T5>) Get<T1, T2, T3, T4, T5>()
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct {
        return (
            _ecs.Components.Column<T1>(),
            _ecs.Components.Column<T2>(),
            _ecs.Components.Column<T3>(),
            _ecs.Components.Column<T4>(),
            _ecs.Components.Column<T5>());
    }

    public (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>, IColumn<T5>, IColumn<T6>) Get<T1, T2, T3, T4, T5, T6>()
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct {
        return (
            _ecs.Components.Column<T1>(),
            _ecs.Components.Column<T2>(),
            _ecs.Components.Column<T3>(),
            _ecs.Components.Column<T4>(),
            _ecs.Components.Column<T5>(),
            _ecs.Components.Column<T6>());
    }

    public (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>, IColumn<T5>, IColumn<T6>, IColumn<T7>) Get<T1, T2, T3, T4, T5, T6, T7>()
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct {
        return (
            _ecs.Components.Column<T1>(),
            _ecs.Components.Column<T2>(),
            _ecs.Components.Column<T3>(),
            _ecs.Components.Column<T4>(),
            _ecs.Components.Column<T5>(),
            _ecs.Components.Column<T6>(),
            _ecs.Components.Column<T7>());
    }

    public (IColumn<T1>, IColumn<T2>, IColumn<T3>, IColumn<T4>, IColumn<T5>, IColumn<T6>, IColumn<T7>, IColumn<T8>) Get<T1, T2, T3, T4, T5, T6, T7, T8>()
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
        where T7 : struct
        where T8 : struct {
        return (
            _ecs.Components.Column<T1>(),
            _ecs.Components.Column<T2>(),
            _ecs.Components.Column<T3>(),
            _ecs.Components.Column<T4>(),
            _ecs.Components.Column<T5>(),
            _ecs.Components.Column<T6>(),
            _ecs.Components.Column<T7>(),
            _ecs.Components.Column<T8>());
    }
}
