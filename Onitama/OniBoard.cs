namespace Onitama;

internal class OniBoard : GraphicsControl
{
    public GameState GameState { get; set; } = new GameState();

    public Color GridColor { get; set; } = Color.Green;

    public override Font Font { get; set; } = new Font("Arial", 0.02f, GraphicsUnit.Pixel);

    public GameVisuals Visuals { get; set; }

    public OniBoard()
    {
        ViewSize = new SizeF(10, 7);
        Visuals = new()
        {
            CurrentTeam = GameState.CurrentTeam,
            BlueStudents = GameState.BlueStudents,
            RedStudents = GameState.RedStudents,
            RedMaster = GameState.RedMaster,
            BlueMaster = GameState.BlueMaster,
        };
        BackColor = Color.DarkGray;
    }

    public (BoardItem Item, Point Point)? FindItem(PointF point)
    {
        var squareX = 7;
        var squareY = 7;
        if (GameState.ActiveWindow == ActiveWindow.Menu)
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
        else if (GameState.ActiveWindow == ActiveWindow.Board)
        {
            foreach (var button in Visuals.BoardButtons)
            {
                if (button.Bounds.Contains(point))
                {
                    return (button.Item, new Point(10, 10));
                }
            }
        }
        else if (GameState.ActiveWindow == ActiveWindow.GameOver)
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
        if (0 < GameState.TutorialStep && GameState.TutorialStep < 4)
        {
            GameState.ActiveWindow = ActiveWindow.Tutorial;
        }
        Visuals.ActiveWindow = GameState.ActiveWindow;
        Visuals.BlueCards = GameState.BlueCards;
        Visuals.RedCards = GameState.RedCards;
        Visuals.NeutralCard = GameState.NeutralCard;
        Visuals.CurrentTeam = GameState.CurrentTeam;
        Visuals.DrawGame(g);
    }

    protected override void ViewMouseMove(float x, float y, MouseButtons buttons)
    {
    }

    protected override void ViewMouseDown(float x, float y, MouseButtons buttons)
    {
        var location = FindItem(new PointF(x, y));
        if (buttons == MouseButtons.Left && location != null)
        {
            GameState.MouseDownLocation = location;
            Invalidate();
        }
    }

    protected override void ViewMouseUp(float x, float y, MouseButtons buttons)
    {
        var location = FindItem(new PointF(x, y));

        if (location != null && buttons == MouseButtons.Left && location == GameState.MouseDownLocation)
        {
            Visuals.ActiveWindow = GameState.ActiveWindow;
            GameState.MouseUp(location.Value.Item, location.Value.Point);
            CopyPropertiesToVisuals();
        }
        GameState.MouseDownLocation = null;
        Invalidate();
    }

    private void CopyPropertiesToVisuals()
    {
        Visuals.CurrentTeam = GameState.CurrentTeam;
        Visuals.ActiveCard = GameState.ActiveCard;
        Visuals.ActiveStudent = GameState.ActiveSquare;
        Visuals.BlueStudents = GameState.BlueStudents;
        Visuals.RedStudents = GameState.RedStudents;
        Visuals.RedMaster = GameState.RedMaster;
        Visuals.BlueMaster = GameState.BlueMaster;
        Visuals.PossibleMoves = GameState.PossibleMoves;
        Visuals.ActiveWindow = GameState.ActiveWindow;
        Visuals.TutorialStep = GameState.TutorialStep;
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

public enum ActiveWindow
{
    Board,
    Menu,
    Tutorial,
    GameOver,
}
