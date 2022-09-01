using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    public sealed record MenuButton(
        string Text,
        RectangleF Bounds,
        Font Font,
        float CornerRadius = 0.1f);
}
