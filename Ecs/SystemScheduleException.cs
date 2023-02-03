using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public class SystemScheduleException : Exception {
    public SystemScheduleException(string message) : base(message) { }
}
