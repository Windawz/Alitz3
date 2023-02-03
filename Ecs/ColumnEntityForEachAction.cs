using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public delegate void ColumnEntityForEachAction<TComponent>(Entity entity, ref TComponent component) where TComponent : struct;
