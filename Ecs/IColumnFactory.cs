using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public interface IColumnFactory {
    IColumn<TComponent> Create<TComponent>() where TComponent : struct;
}
