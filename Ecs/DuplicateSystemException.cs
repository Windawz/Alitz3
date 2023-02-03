using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Ecs;
public class DuplicateSystemException : SystemScheduleException {
    public DuplicateSystemException(ISystem[] duplicates) : base(MakeMessage(duplicates)) {
        Duplicates = duplicates;
    }

    public ISystem[] Duplicates { get; }

    private static string MakeMessage(ISystem[] duplicates) {
        var sb = new StringBuilder();
        sb.Append("Duplicate systems in schedule: ")
            .AppendJoin(", ", duplicates.Select(d => d.GetType().FullName));
        return sb.ToString();
    }
}
