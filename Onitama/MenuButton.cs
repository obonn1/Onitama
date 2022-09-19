namespace Onitama;

public sealed record MenuButton(
    string Text,
    RectangleF Bounds,
    Font Font,
    BoardItem Item,
    float CornerRadius = 0.1f);
