using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Alitz3.Collections;

namespace Alitz3.Ecs;
internal static class AlitzListExtensions {
    public static void EnsureCount<T>(this AlitzList<T> list, int count, T item) {
        if (count <= list.Count) {
            return;
        }
        list.Resize(count, item);
    }

    public static void EnsureCount<T>(this AlitzList<T> list, int count, Func<T> generator) {
        if (count <= list.Count) {
            return;
        }
        list.Resize(count, generator);
    }
}
