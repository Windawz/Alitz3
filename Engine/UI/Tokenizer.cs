using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Engine.UI;
class EscapingTokenizer : ITokenizer {
    public EscapingTokenizer() {
        Separators = new[] { ' ', '\t' };
        Escapes = new[] { '"', '\'' };
        Options = TokenizerOptions.RemoveEmptyEntries | TokenizerOptions.TrimEntries;
    }

    public char[] Separators { get; set; }
    public char[] Escapes { get; set; }
    public TokenizerOptions Options { get; set; }

    public string[] Tokenize(string value) {
        var tokens = new List<string>();

        char? escape = null;
        var tokenBuilder = new StringBuilder();

        void BuildToken() {
            tokens.Add(tokenBuilder.ToString());
            tokenBuilder.Clear();
        }

        for (var i = 0; i < value.Length; i++) {
            var c = value[i];
            if (escape is null && Separators.Contains(c)) {
                BuildToken();
            } else if (Escapes.Contains(c)) {
                if (escape is null) {
                    escape = c;
                } else if (c == escape) {
                    escape = null;
                } else {
                    tokenBuilder.Append(c);
                }
            } else {
                tokenBuilder.Append(c);
            }
        }

        if (escape is not null) {
            throw new ArgumentException("Incomplete escape character pair", nameof(value));
        }
        if (tokenBuilder.Length > 0) {
            BuildToken();
        }

        if (Options.HasFlag(TokenizerOptions.TrimEntries)) {
            for (var i = 0; i < tokens.Count; i++) {
                tokens[i] = tokens[i].Trim();
            }
        }
        if (Options.HasFlag(TokenizerOptions.RemoveEmptyEntries)) {
            tokens.RemoveAll(token => token.Length == 0);
        }

        return tokens.ToArray();
    }
}
