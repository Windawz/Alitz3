using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
internal class PoolCheckingColumnFactory : IColumnFactory {
    public PoolCheckingColumnFactory(IColumnFactory columnFactory, EntityPool entityPool) {
        _columnFactory = columnFactory;
        _entityPool = entityPool;
    }

    private readonly IColumnFactory _columnFactory;
    private readonly EntityPool _entityPool;

    public IColumn<TComponent> Create<TComponent>() where TComponent : struct =>
        new PoolCheckingColumn<TComponent>(_columnFactory.Create<TComponent>(), _entityPool);
}
