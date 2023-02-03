using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public class SystemRunner {
    public SystemRunner(Ecs ecs, SystemSchedule schedule) {
        _querier = new Querier(ecs);
        _schedule = schedule;
    }

    private readonly Querier _querier;
    private readonly SystemSchedule _schedule;

    public void Run() {
        foreach (var system in _schedule) {
            system.Run(_querier);
        }
    }
}
