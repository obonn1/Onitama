namespace Onitama;

public sealed class InteractiveRectangle
{
    public string Name { get; set; }
    public RectangleF Bounds { get; set; }
    public bool IsDrawn { get; set; }

    public InteractiveRectangle(string name, RectangleF bounds, bool isDrawn = false)
    {
        Name = name;
        Bounds = bounds;
        IsDrawn = isDrawn;
    }
}
