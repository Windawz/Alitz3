using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Engine.Graphics;
class Image {
    public Image(char[,] charMap) {
        CharMap = charMap;
        Width = charMap.GetLength(1);
        Height = charMap.GetLength(0);
    }
    public Image(char[,] charMap, IDictionary<char, ColorPair> colorTable, char[,] colorMap) : this(charMap) {
        if (!DoDimensionsMatch(charMap, colorMap)) {
            throw new ArgumentException("Color map dimensions differ from char map dimensions", nameof(colorMap));
        }
        if (!HasOnlyTableColors(colorMap, colorTable)) {
            throw new ArgumentException("Color map contains indices not present in table", nameof(colorMap));
        }

        ColorTable = colorTable;
        ColorMap = colorMap;
    }

    public char[,] CharMap { get; }
    public IDictionary<char, ColorPair>? ColorTable { get; }
    public char[,]? ColorMap { get; }
    public int Width { get; }
    public int Height { get; }

    private static bool DoDimensionsMatch(char[,] charMap, char[,] colorMap) =>
        charMap.GetLength(0) == colorMap.GetLength(0) && charMap.GetLength(1) == colorMap.GetLength(1);
    private static bool HasOnlyTableColors(char[,] colorMap, IDictionary<char, ColorPair> colorTable) =>
        colorMap.Cast<char>().All(i => colorTable.ContainsKey(i));
    private static char[,] ParseCharMap(string[] lines) {
        char[,] charMap = GetPaddedCharMap(lines);
        FillCharMap(charMap, lines, ' ');
        return charMap;
    }
    private static void FillCharMap(char[,] charMap, string[] lines, char fallback) {
        for (int x = 0; x < charMap.GetLength(0); x++) {
            for (int y = 0; y < charMap.GetLength(1); y++) {
                (int i, int j) = (y, x);
                string line = lines[i];
                char c = j < line.Length ? line[j] : fallback;
                charMap[x, y] = c;
            }
        }
    }
    private static char[,] GetPaddedCharMap(string[] lines) {
        int width = lines.Length == 0 ? 0 : GetMaxLineLength(lines);
        int height = lines.Length;
        return new char[width, height];
    }
    private static int GetMaxLineLength(string[] lines) =>
        lines.Select(x => x.Length)
            .Max();
    private static bool TryParseColorTable(string[] lines, out Dictionary<int, ColorPair>? result) {
        result = new Dictionary<int, ColorPair>(lines.Length);
        foreach (string line in lines) {
            if (!TryParseColorBinding(line, out var pair)) {
                return false;
            }
            result.Add(pair.Key, pair.ColorPair);
        }
        return true;
    }
    private static bool TryParseColorBinding(string line, out (char Key, ColorPair ColorPair) result) {
        var options = StringSplitOptions.RemoveEmptyEntries
            | StringSplitOptions.TrimEntries;

        bool succeeded = true;
        result = default;

        Color foreColor = default;
        Color backColor = default;

        string[] binding = line.Split('=', 2, options);
        string[] colors;
        if (binding.Length == 2) {
            colors = binding[1].Split(',', 2, options);
        } else {
            colors = Array.Empty<string>();
        }

        succeeded = binding.Length == 2
            && binding[0].Length == 1
            && colors.Length == 2
            && Color.TryParse(colors[0], out foreColor)
            && Color.TryParse(colors[1], out backColor);

        if (succeeded) {
            result = (binding[0][0], new ColorPair(foreColor, backColor));
        }

        return succeeded;
    }
}
