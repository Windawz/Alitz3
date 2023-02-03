using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3;
public abstract class Singleton<TDerived> where TDerived : Singleton<TDerived> {
    protected Singleton() { }

    public static TDerived Instance { get; } = CreateInstance();

    private static TDerived CreateInstance() {
        return (TDerived)Activator.CreateInstance(typeof(TDerived), nonPublic: true)!;
    }
}
