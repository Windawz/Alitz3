using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
internal class DefaultColumnFactory : IColumnFactory {
    public IColumn<TComponent> Create<TComponent>() where TComponent : struct =>
        throw new NotImplementedException();
}
