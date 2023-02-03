using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Alitz3.Collections;

namespace Alitz3.Engine.UI;
class CommandDictionary : KeyExtractorDictionary<string, Command> {
    public CommandDictionary() : base(command => command.Name) { }
}
