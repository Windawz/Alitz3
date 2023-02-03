using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public class SystemSchedule : IEnumerable<ISystem> {
    public SystemSchedule(ISystem[] systems) {
        _systems = systems;
    }

    private readonly ISystem[] _systems;

    public IEnumerator<ISystem> GetEnumerator() {
        foreach (var system in _systems) {
            yield return system;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}
