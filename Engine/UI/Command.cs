using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Engine.UI;
class Command {
    public Command(Delegate @delegate) {
        Delegate = @delegate;
    }

    private readonly string _name;
    private readonly string[] _parameterNames;
    private readonly string[] _parameterDescriptions;

    public Delegate Delegate { get; }
    public string Name {
        get => _name ?? Delegate.Method.Name;
        init {
            if (string.IsNullOrWhiteSpace(value)) {
                throw new ArgumentException("Command name may not be empty or whitespace");
            }
            _name = value;
        }
    }
    public string Description { get; init; } = "";
    public string ResultName { get; init; } = "";
    public string ResultDescription { get; init; } = "";
    public string[] ParameterNames {
        get => _parameterNames;
        init {
            if (value.Any(s => string.IsNullOrWhiteSpace(s))) {
                throw new ArgumentException("Parameter name may not be empty");
            }
            _parameterNames = value;
        }
    }

    public object? Execute(params object?[] args) {
        var target = Delegate.Target;
        var result = Delegate.Method.Invoke(target, args);
        return result;
    }
    private static string ExtractName(Delegate @delegate) {
        return @delegate.Method.Name;
    }
    private static string[] ExtractParameterNames(Delegate @delegate) {
        return @delegate.Method.GetParameters()
            .Select(p => p.Name ?? "")
            .ToArray();
    }
}
