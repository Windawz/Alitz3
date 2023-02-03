using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Engine.UI;

[Flags]
enum TokenizerOptions {
    None = 0x0,
    RemoveEmptyEntries = 0x1,
    TrimEntries = 0x2,
}
