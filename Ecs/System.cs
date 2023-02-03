using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public abstract class System : ISystem {
    protected System(int priority, params Type[] dependencies) {
        Priority = priority;
        _dependenceChecker = (systemType) =>
            dependencies.Contains(systemType);
    }

    private readonly Func<Type, bool> _dependenceChecker;

    public int Priority { get; }

    public bool DependsOn(Type systemType) =>
        _dependenceChecker(systemType);

    public abstract void Run(Querier querier);
}
