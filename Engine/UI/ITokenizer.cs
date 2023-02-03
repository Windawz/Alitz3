using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Engine.UI;
interface ITokenizer {
    char[] Separators { get; set; }

    string[] Tokenize(string value);
}
