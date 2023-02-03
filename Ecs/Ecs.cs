using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public class Ecs {
    public Ecs(SystemSchedule schedule) : this(schedule, new DefaultColumnFactory()) { }
    public Ecs(SystemSchedule schedule, IColumnFactory columnFactory) {
        EntityPool = new EntityPool();
        Components = new ComponentCollection(
            GetDecoratedColumnFactory(columnFactory));
        SystemRunner = new SystemRunner(this, schedule);
    }

    public ComponentCollection Components { get; }
    public EntityPool EntityPool { get; }
    public SystemRunner SystemRunner { get; }

    private IColumnFactory GetDecoratedColumnFactory(IColumnFactory factory) =>
        new PoolCheckingColumnFactory(
            factory,
            EntityPool);
}
