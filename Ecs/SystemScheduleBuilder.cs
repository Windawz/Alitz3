using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public class SystemScheduleBuilder {
    private readonly List<ISystem> _systems = new();

    public IReadOnlyList<ISystem> Systems => _systems;

    public SystemScheduleBuilder Add(ISystem system) {
        int insertionIndex = 0;
        for (int i = _systems.Count - 1; i >= 0; i--) {
            bool found = system.DependsOn(_systems[i].GetType())
                || system.Priority <= _systems[i].Priority;
            if (found) {
                insertionIndex = i + 1;
                break;
            }
        }
        _systems.Insert(insertionIndex, system);
        return this;
    }

    public SystemSchedule Build() {
        ValidateDuplicates();
        ValidateCircularDependencies();
        return new SystemSchedule(_systems.ToArray());
    }

    private void ValidateDuplicates() {
        var duplicatedSystems = _systems.Where(system => {
            var type = system.GetType();
            int occurenceCount = _systems.Count(other => other.GetType() == system.GetType());
            return occurenceCount >= 2;
        });
        if (duplicatedSystems.Any()) {
            throw new DuplicateSystemException(duplicatedSystems.ToArray());
        }
    }

    private void ValidateCircularDependencies() {
        foreach (var system in _systems) {
            var dependencies = _systems.Where(other => system.DependsOn(other.GetType()));
            var systemType = system.GetType();
            var circularDependencies = dependencies.Where(other => other.DependsOn(systemType));
            if (circularDependencies.Any()) {
                throw new CircularDependencyException(system, circularDependencies.ToArray());
            }
        }
    }
}
