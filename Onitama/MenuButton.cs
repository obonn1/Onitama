namespace Onitama;

public sealed record MenuButton(
    string Text,
    RectangleF Bounds,
    Font Font,
    float CornerRadius = 0.1f);
