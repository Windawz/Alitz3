using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Collections;
public class KeyExtractorDictionary<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull {
    public KeyExtractorDictionary(Func<TValue, TKey> extractor) {
        Extractor = extractor;
    }
    public KeyExtractorDictionary(
        Func<TValue, TKey> extractor,
        IEnumerable<KeyValuePair<TKey, TValue>> pairs) : base(pairs) {

        Extractor = extractor;
    }

    public Func<TValue, TKey> Extractor { get; }

    public void Add(TValue value) => Add(Extractor(value), value);
}
