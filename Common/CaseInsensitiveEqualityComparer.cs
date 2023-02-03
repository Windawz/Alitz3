using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3;
public class CaseInsensitiveEqualityComparer : EqualityComparer<string> {
    private CaseInsensitiveEqualityComparer(StringComparison comparison) {
        _comparison = comparison;
    }

    private readonly StringComparison _comparison;

    new public static CaseInsensitiveEqualityComparer Default { get; } =
        new(StringComparison.CurrentCultureIgnoreCase);
    public static CaseInsensitiveEqualityComparer Invariant { get; } =
        new(StringComparison.InvariantCultureIgnoreCase);
    public static CaseInsensitiveEqualityComparer Ordinal { get; } =
        new(StringComparison.OrdinalIgnoreCase);

    public override bool Equals(string? x, string? y) {
        return string.Equals(x, y, _comparison);
    }
    public override int GetHashCode([DisallowNull] string obj) {
        throw new NotImplementedException();
    }
}
