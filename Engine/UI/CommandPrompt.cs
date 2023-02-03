using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Engine.UI;
class CommandPrompt {
    public ITokenizer Tokenizer { get; set; } = new EscapingTokenizer();

    public string[] Request(string message = "", string marker = "> ") {
        if (!string.IsNullOrEmpty(message)) {
            Console.WriteLine(message);
        }
        Console.Write(marker);
        string input = Console.ReadLine()!;
        return Tokenizer.Tokenize(input);
    }
}
