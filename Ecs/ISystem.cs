using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public interface ISystem {
    int Priority { get; }

    void Run(Querier querier);
    bool DependsOn(Type systemType);
}
