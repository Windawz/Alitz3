using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Alitz3.Engine.Graphics;
readonly struct Color : IEquatable<Color> {
    public Color(byte r, byte g, byte b) {
        R = r;
        G = g;
        B = b;
    }
    public Color(byte r, byte g, byte b, byte a) {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    private static readonly Dictionary<string, Color> _namedColors = new(CaseInsensitiveEqualityComparer.Ordinal) {
        { "black", new(0, 0, 0) },
        { "darkblue", new(0, 0, 128) },
        { "darkgreen", new(0, 128, 0) },
        { "darkcyan", new(0, 128, 128) },
        { "darkred", new(128, 0, 0) },
        { "darkmagenta", new(128, 0, 128) },
        { "darkyellow", new(128, 128, 0) },
        { "gray", new(192, 192, 192) },
        { "darkgray", new(128, 128, 128) },
        { "blue", new(0, 0, 255) },
        { "green", new(0 ,255, 0) },
        { "cyan", new(0, 255, 255) },
        { "red", new(255, 0, 0) },
        { "magenta", new(255, 0, 255) },
        { "yellow", new(255, 255, 0) },
        { "white", new(255, 255, 255) },
    };

    public static IReadOnlyDictionary<string, Color> NamedColors =>
        _namedColors;
    public static Color Black => _namedColors["black"];
    public static Color DarkBlue => _namedColors["darkblue"];
    public static Color DarkGreen => _namedColors["darkgreen"];
    public static Color DarkCyan => _namedColors["darkcyan"];
    public static Color DarkRed => _namedColors["darkred"];
    public static Color DarkMagenta => _namedColors["darkmagenta"];
    public static Color DarkYellow => _namedColors["darkyellow"];
    public static Color Gray => _namedColors["gray"];
    public static Color DarkGray => _namedColors["darkgray"];
    public static Color Blue => _namedColors["blue"];
    public static Color Green => _namedColors["green"];
    public static Color Cyan => _namedColors["cyan"];
    public static Color Red => _namedColors["red"];
    public static Color Magenta => _namedColors["magenta"];
    public static Color Yellow => _namedColors["yellow"];
    public static Color White => _namedColors["white"];
    public byte R { get; init; }
    public byte G { get; init; }
    public byte B { get; init; }
    public byte A { get; init; } = 255;

    public static bool operator ==(Color left, Color right) => left.Equals(right);
    public static bool operator !=(Color left, Color right) => !left.Equals(right);

    public static bool TryParse(string input, out Color result) {
        return TryParseName(input, out result) || TryParseHex(input, out result);
    }
    public bool Equals(Color other) =>
        R == other.R
        && G == other.G
        && B == other.B
        && A == other.A;
    public override bool Equals(object? other) {
        return other is not null and Color color
            && color.Equals(this);
    }
    public override int GetHashCode() {
        return HashCode.Combine(R, G, B, A);
    }
    private static bool TryParseName(string input, out Color result) {
        bool succeeded = _namedColors.TryGetValue(input, out var color);
        result = succeeded ? color : default;
        return succeeded;
    }
    private static bool TryParseHex(string input, out Color result) {
        (byte r, byte g, byte b, byte a) = (0, 0, 0, 0);

        static bool TryParseHexDigitPair(string digitPair, out byte result) {
            var style = NumberStyles.HexNumber & ~NumberStyles.AllowHexSpecifier;
            return byte.TryParse(digitPair, style, CultureInfo.InvariantCulture, out result);
        }

        var succeeded = input.Length == 8
        && TryParseHexDigitPair(input[0..2], out r)
        && TryParseHexDigitPair(input[2..4], out g)
        && TryParseHexDigitPair(input[4..6], out b)
        && TryParseHexDigitPair(input[6..8], out a);

        result = succeeded ? new Color(r, g, b, a) : default;
        return succeeded;
    }
}
