using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Common;
internal static class TupleExtensions {
    public static IEnumerable<Type> GetElementTypes<TTuple>() where TTuple : ITuple =>
        typeof(TTuple).GetGenericArguments();

    public static ElementAccessor<TTuple> GetElement<TTuple>(this TTuple tuple, int index) where TTuple : ITuple {
        return new ElementAccessor<TTuple>(tuple, index);
    }

    public readonly struct ElementAccessor<TTuple> where TTuple : ITuple {
        internal ElementAccessor(TTuple tuple, int index) {
            _tuple = tuple;
            _memberName = MakeMemberName(index);
        }

        private readonly TTuple _tuple;
        private readonly string _memberName;

        public T To<T>() {
            var tupleType = typeof(TTuple);

            if (!TryGetValueFromProperty(out object result) && !TryGetValueFromField(out result)) {
                throw new ArgumentOutOfRangeException("index", $"Failed to find item by index (member name: {_memberName})");
            }

            return (T)result;
        }

        private bool TryGetValueFromProperty([NotNullWhen(true)] out object value) {
            PropertyInfo? propInfo = _tuple.GetType().GetProperty(_memberName);
            if (propInfo is not null) {
                value = propInfo.GetGetMethod()!.Invoke(_tuple, null)!;
                return true;
            } else {
                value = null!;
                return false;
            }
        }

        private bool TryGetValueFromField([NotNullWhen(true)] out object value) {
            FieldInfo? fieldInfo = _tuple.GetType().GetField(_memberName);
            if (fieldInfo is not null) {
                value = fieldInfo.GetValue(_tuple)!;
                return true;
            } else {
                value = null!;
                return false;
            }
        }

        private static string MakeMemberName(int index) =>
            $"Item{index + 1}";
    }
}
