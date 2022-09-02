namespace Onitama;

public abstract class DrawTools
{
    public static SolidBrush BlackBrush { get; } = new(Color.Black);

    public static SolidBrush RedBrush { get; } = new(Color.Red);

    public static SolidBrush BlueBrush { get; } = new(Color.Blue);

    public static SolidBrush DarkRedBrush { get; } = new(Color.DarkRed);

    public static SolidBrush DarkBlueBrush { get; } = new(Color.DarkBlue);

    public static SolidBrush WhiteBrush { get; } = new(Color.White);

    public static SolidBrush MoccasinBrush { get; } = new(Color.Moccasin);

    public static SolidBrush MatBrush { get; } = new(Color.FromArgb(245, 191, 90));

    public static Pen BlackPen { get; } = new(BlackBrush, 0.05f);

    public static Pen HighlightPen { get; } = new(WhiteBrush, 0.075f)
    {
        DashPattern = new float[] { 3.25F, 2.25F },
        DashCap = System.Drawing.Drawing2D.DashCap.Round,
    };

    public static Font TitleFont { get; } = new("Arial", 0.28f, FontStyle.Bold, GraphicsUnit.Pixel);

    public static Font TutorialFont { get; } = new("Arial", 0.125f, GraphicsUnit.Pixel);

    public static Font SmallFont { get; } = new("Arial", 0.075f, GraphicsUnit.Pixel);

    public static void DrawButton(Graphics g, MenuButton button)
    {
        g.DrawRoundedRectangleF(BlackPen, button.Bounds, button.CornerRadius);
        g.FillRoundedRectangleF(MatBrush, button.Bounds, button.CornerRadius);
        g.DrawString(button.Text, button.Font, BlackBrush, button.Bounds, StringFormats.Center);
    }
}
