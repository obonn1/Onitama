using System.ComponentModel;

namespace Onitama;

internal class OniBoard : GraphicsControl
{
    [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Game Game { get; set; } = new Game();

    public Color GridColor { get; set; } = Color.Green;

    public override Color BackColor { get; set; } = Color.DarkGray;

    public override Font Font { get; set; } = new Font("Arial", 0.02f, GraphicsUnit.Pixel);

    public GameVisuals Visuals { get; set; }

    public OniBoard()
    {
        ViewSize = new SizeF(10, 7);
        Visuals = new(Game);
    }

    public (BoardItem Item, Point Point)? FindItem(PointF point)
    {
        var squareX = 7;
        var squareY = 7;
        if (Game.ActiveScreen == Screens.Menu)
        {
            foreach (var button in Visuals.MenuButtons)
            {
                if (button.Bounds.Contains(point))
                {
                    return (button.Item, new Point(10, 10));
                }
            }
            if (!GameVisuals.MenuBox.Contains(point))
            {
                return (BoardItem.OffMenu, new Point(10, 10));
            }
        }
        else if (Game.ActiveScreen == Screens.Board)
        {
            foreach (var button in Visuals.BoardButtons)
            {
                if (button.Bounds.Contains(point))
                {
                    return (button.Item, new Point(10, 10));
                }
            }
        }
        else if (Game.ActiveScreen == Screens.GameOver)
        {
            if (GameVisuals.PlayAgain.Contains(point))
            {
                return (BoardItem.PlayAgain, new Point(10, 10));
            }
        }

        for (var i = 0; i < 5; i++)
        {
            if ((point.X > Visuals.GridOrigin.X + i + 0.1f) && (point.X < Visuals.GridOrigin.X + i + 0.9f))
            {
                squareX = i;
                break;
            }
        }

        for (var i = 0; i < 5; i++)
        {
            if ((point.Y > Visuals.GridOrigin.Y + i + 0.1f) && (point.Y < Visuals.GridOrigin.Y + i + 0.9f))
            {
                squareY = i;
                break;
            }
        }

        if (squareX != 7 && squareY != 7)
        {
            return (BoardItem.Square, new Point(squareX, squareY));
        }

        return null;
    }

    protected override void VisualsDraw(Graphics g)
    {
        if (0 < Game.TutorialStep && Game.TutorialStep < 4)
        {
            Game.ActiveScreen = Screens.Tutorial;
        }
        Visuals.DrawGame(g, Game);
    }

    protected override void ViewMouseMove(float x, float y, MouseButtons buttons)
    {
    }

    protected override void ViewMouseDown(float x, float y, MouseButtons buttons)
    {
        var location = FindItem(new PointF(x, y));
        if (buttons == MouseButtons.Left && location != null)
        {
            Game.MouseDownLocation = location;
            Invalidate();
        }
    }

    protected override void ViewMouseUp(float x, float y, MouseButtons buttons)
    {
        var location = FindItem(new PointF(x, y));

        if (location is null) Game.MouseUp(null, new Point(0, 0));
        if (location is not null && buttons == MouseButtons.Left && location == Game.MouseDownLocation)
        {
            Game.MouseUp(location!.Value.Item, location.Value.Point);
        }
        Game.MouseDownLocation = null;
        Invalidate();
    }
}

public enum Team
{
    Red,
    Blue,
}

public enum BoardItem
{
    BlueCard1,
    BlueCard2,
    RedCard1,
    RedCard2,
    Square,
    PlayAgain,
    OpenMenu,
    NewGame,
    BlueSurrender,
    RedSurrender,
    CloseMenu,
    Tutorial,
    CloseGame,
    OffMenu,
}

public enum Screens
{
    Board,
    Menu,
    Tutorial,
    GameOver,
}
