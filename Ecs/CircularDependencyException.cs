using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public class CircularDependencyException : SystemScheduleException {
    public CircularDependencyException(ISystem system, ISystem[] circularDependencies) : base(MakeMessage(system, circularDependencies)) {
        System = system;
        CircularDependencies = circularDependencies;
    }

    ISystem System { get; }
    ISystem[] CircularDependencies { get; }

    private static string MakeMessage(ISystem system, ISystem[] circularDependencies) {
        var sb = new StringBuilder();
        sb.Append("Circular dependency between system ")
            .Append(system.GetType().FullName)
            .Append(" and its dependencies ")
            .AppendJoin(", ", circularDependencies.Select(d => d.GetType().FullName));
        return sb.ToString();
    }
}
