using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alitz3.Engine.Graphics;
readonly struct ColorPair {
    public ColorPair(Color foreColor, Color backColor) {
        ForeColor = foreColor;
        BackColor = backColor;
    }

    public Color ForeColor { get; } = Color.Gray;
    public Color BackColor { get; } = Color.Black;
}
