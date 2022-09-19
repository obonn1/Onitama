namespace Onitama;

public class GameVisuals : DrawTools
{
    public static float CornerRadius { get; } = 0.1f;

    public static Font Font { get; set; } = new("Arial", 0.275f, GraphicsUnit.Pixel);

    public PointF GridOrigin { get; } = new(2.5f, 1.75f);

    public int TutorialStep { get; set; } = 1;

    public GameVisuals()
    {
        BoardButtons = new()
    {
        new(BoardItem.BlueCard1, blueCard1Bounds),
        new(BoardItem.BlueCard2, blueCard2Bounds),
        new(BoardItem.RedCard1, redCard1Bounds),
        new(BoardItem.RedCard2, redCard2Bounds),
        new(BoardItem.OpenMenu, menuButton),
    };
    }

    public List<Point> BlueStudents { get; set; } = new();

    public List<Point> RedStudents { get; set; } = new();

    public List<Point> PossibleMoves { get; set; } = new();

    public Point BlueMaster { get; set; }

    public Point RedMaster { get; set; }

    public Point MouseOverXY { get; set; }

    public Point? ActiveStudent { get; set; }

    public Card? ActiveCard { get; set; }

    public BoardItem MouseOverItem { get; set; }

    public Card[]? BlueCards { get; set; }

    public Card[]? RedCards { get; set; }

    public Card? NeutralCard { get; set; }

    public Team CurrentTeam { get; set; }

    public bool IsMenuOpen { get; set; }
    public ActiveWindow ActiveWindow { get; set; }

    public static RectangleF PlayAgain { get; } = new(4.25f, 3.88f, 1.5f, 0.5f);
    public static RectangleF MenuBox { get; } = new(3.5f, 1f, 3f, 5f);

    public List<(BoardItem Item, RectangleF Bounds)> BoardButtons { get; }

    public List<MenuButton> MenuButtons { get; } = new()
    {
        new MenuButton("New Game", new(4f, 1.8f, 2f, 0.5f), Font, BoardItem.NewGame),
        new MenuButton("Surrender Blue", new(4f, 2.6f, 2f, 0.5f), Font, BoardItem.BlueSurrender),
        new MenuButton("Surrender Red", new(4f, 3.4f, 2f, 0.5f), Font, BoardItem.RedSurrender),
        new MenuButton("Tutorial", new(4f, 4.2f, 2f, 0.5f), Font, BoardItem.Tutorial),
        new MenuButton("Close Game", new(4f, 5f, 2f, 0.5f), Font, BoardItem.CloseGame),
        new MenuButton("X", new(6.2f, 1.1f, 0.2f, 0.2f), TutorialFont, BoardItem.CloseMenu),
    };

    private static RectangleF blueCard1Bounds = new(.425f, 1.88f, 1.8f, 2.25f);
    private static RectangleF blueCard2Bounds = new(.425f, 4.36f, 1.8f, 2.25f);
    private static RectangleF redCard1Bounds = new(7.7f, 1.88f, 1.8f, 2.25f);
    private static RectangleF redCard2Bounds = new(7.7f, 4.36f, 1.8f, 2.25f);
    private static RectangleF menuButton = new(0.1f, 0.1f, 0.6f, 0.225f);

    public void DrawGame(Graphics g)
    {
        DrawBoard(g);
        DrawState(g);

        switch (ActiveWindow)
        {
            case ActiveWindow.Board:
                break;
            case ActiveWindow.Tutorial:
                DrawTutorial(g);
                break;
            case ActiveWindow.GameOver:
                DrawGameOver(g);
                break;
            case ActiveWindow.Menu:
                DrawMenu(g);
                break;
            default:
                break;
        }
    }

    private void DrawBoard(Graphics g)
    {
        RectangleF board = new(0.05f, 0.05f, 9.9f, 6.9f);
        RectangleF mat = new(0.15f, 1.65f, 9.7f, 5.2f);
        RectangleF grid = new(2.5f, 1.75f, 5f, 5f);

        DrawRoundedBox(g, board, new SolidBrush(Color.Green), BlackPen, 0);
        g.FillRoundedRectangleF(MatBrush, mat, .1f);
        DrawRoundedBox(g, grid, new SolidBrush(Color.Olive), BlackPen, 0);
        DrawRoundedTextBox(g, "MENU", TutorialFont, menuButton, BlackPen, MoccasinBrush, BlackBrush, 0.05f, StringFormats.Center);
        for (var y = 0; y < 5; y++)
        {
            for (var x = 0; x < 5; x++)
            {
                g.DrawRectangle(new Pen(Color.DarkOliveGreen, 0.03f), GridToView(x, y).X + 0.1f, GridToView(x, y).Y + 0.1f, 0.8f, 0.8f);
            }
        }

        RectangleF templeBlue = new(GridToView(0, 2).X + 0.075f, GridToView(0, 2).Y + 0.075f, .85f, .85f);
        RectangleF templeRed = new(GridToView(4, 2).X + 0.075f, GridToView(0, 2).Y + 0.075f, .85f, .85f);
        g.DrawRoundedRectangleF(new Pen(Color.DarkBlue, 0.075f), templeBlue, 0.1f);
        g.DrawRoundedRectangleF(new Pen(Color.DarkRed, 0.075f), templeRed, 0.1f);
    }

    public void DrawState(Graphics g)
    {
        foreach (var piece in BlueStudents)
        {
            var place = new RectangleF(piece.X + GridOrigin.X + 0.1f, piece.Y + GridOrigin.Y + 0.1f, 0.8f, 0.8f);
            g.DrawImage(Resources.blueStudentImg, place);
        }

        foreach (var piece in RedStudents)
        {
            g.DrawImage(Resources.redStudentImg, new RectangleF(piece.X + GridOrigin.X + 0.1f, piece.Y + GridOrigin.Y + 0.1f, 0.8f, 0.8f));
        }

        g.DrawImage(Resources.blueMasterImg, new RectangleF(BlueMaster.X + GridOrigin.X + 0.1f - 0.25f, BlueMaster.Y + GridOrigin.Y, 1.3f, 1.3f));
        g.DrawImage(Resources.redMasterImg, new RectangleF(RedMaster.X + GridOrigin.X + 0.1f - 0.25f, RedMaster.Y + GridOrigin.Y, 1.3f, 1.3f));
        if (ActiveStudent != null)
        {
            g.DrawRoundedRectangleF(new Pen(Color.DarkOrange, 0.1f), new(ActiveStudent.Value.X + GridOrigin.X + 0.075f, ActiveStudent.Value.Y + GridOrigin.Y + 0.075f, 0.85f, 0.85f), 0.4f);

            if (ActiveCard != null)
            {
                foreach (var square in PossibleMoves)
                {
                    g.DrawRoundedRectangleF(new Pen(Color.White, 0.05f), new(square.X + GridOrigin.X + 0.1f, square.Y + GridOrigin.Y + 0.1f, 0.8f, 0.8f), 0.4f);
                }
            }
        }

        var cards = new Card[4]
        {
            BlueCards![0],
            BlueCards[1],
            RedCards![0],
            RedCards[1],
        };

        foreach (var card in cards)
        {
            if (card is null)
            {
                break;
            }

            var isActive = ActiveCard == card;
            var team = Array.IndexOf(cards, card) < 2 ? Team.Blue : Team.Red;
            var location = Array.IndexOf(cards, card) switch
            {
                0 => blueCard1Bounds,
                1 => blueCard2Bounds,
                2 => redCard1Bounds,
                3 => redCard2Bounds,
                _ => throw new Exception(),
            };
            DrawCard(g, card, location, isActive, team);
        }

        RectangleF neutralCardBG = new(3f, 0.15f, 4f, 1.4f);
        DrawRoundedTextBox(g, NeutralCard!.Name, Font, neutralCardBG, null, MoccasinBrush, BlackBrush, CornerRadius, StringFormats.Center);

        NeutralCard!.DrawCardGrid(g, new PointF(3.025f, 0.175f), 1.25f);
        Card.Invert(NeutralCard).DrawCardGrid(g, new PointF(5.625f, 0.175f), 1.25f);

        RectangleF redTurnBG = new(7.45f, 0.95f, 1.8f, .35f);
        RectangleF blueTurnBG = new(.25f, 0.95f, 1.8f, .35f);
        Brush teamBrush = CurrentTeam == Team.Blue ? DarkBlueBrush : DarkRedBrush;

        DrawRoundedTextBox(g, "YOUR TURN", Font, CurrentTeam == Team.Blue ? blueTurnBG : redTurnBG, BlackPen, teamBrush, MoccasinBrush, 0.02f, StringFormats.Center);
    }

    private void DrawTutorial(Graphics g)
    {
        RectangleF cornerBox = new(0.15f, 0.15f, 2.7f, 1.4f);
        RectangleF instructionsBox = new(0.15f, 0.5f, 2.7f, 1f);
        DrawRoundedTextBox(g, $"HOW TO PLAY {TutorialStep}/3", TitleFont, cornerBox, BlackPen, MoccasinBrush, BlackBrush, CornerRadius, StringFormats.CenterTop);

        switch (TutorialStep)
        {
            case 1:
                g.DrawString(
                    """
                    Each team has five pieces, four Students 
                    and one Master 
                         
                    All pieces can defeat an 
                    opposing piece by moving into their spot
                    """,
                    TutorialFont,
                    BlackBrush,
                    instructionsBox,
                    StringFormats.CenterTop);
                break;
            case 2:
                g.DrawString(
                    """
                    The cards on your side of the board show 
                    possible piece movement. 

                    Choose a card, then a piece to move. 

                    After moving, the card used will switch with
                    the neutral card on top.
                    """,
                    TutorialFont,
                    BlackBrush,
                    instructionsBox,
                    StringFormats.CenterTop);
                break;
            case 3:
                g.DrawString(
                    """
                    To win: 
                    Defeat the opposing Master 

                    or 

                    Move your Master into the opposing Temple
                    """,
                    TutorialFont,
                    BlackBrush,
                    instructionsBox,
                    StringFormats.CenterTop);
                g.DrawRectangle(HighlightPen, GridOrigin.X + 0.07f, GridOrigin.Y + 2.07f, .85f, .85f);
                g.DrawRectangle(HighlightPen, GridOrigin.X + 4.07f, GridOrigin.Y + 2.07f, .85f, .85f);
                break;
        }
    }

    private void DrawGameOver(Graphics g)
    {
        RectangleF gameOverBanner = new(3, 2.5f, 4, 2);
        DrawRoundedTextBox(g, $"{CurrentTeam.ToString().ToUpper()} WINS!!!", new Font("Arial", 0.5f, FontStyle.Bold, GraphicsUnit.Pixel), gameOverBanner, new Pen(Color.Black, 0.05f), MoccasinBrush, BlackBrush, 0.5f, StringFormats.Center);
        DrawRoundedTextBox(g, "Play Again", new Font("Arial", 0.225f, FontStyle.Bold, GraphicsUnit.Pixel), PlayAgain, new Pen(Color.Black, 0.02f), WhiteBrush, BlackBrush, 0.1f, StringFormats.Center);
    }

    private void DrawMenu(Graphics g)
    {
        DrawRoundedTextBox(g, "MENU", TitleFont, MenuBox, BlackPen, MoccasinBrush, BlackBrush, CornerRadius, StringFormats.CenterTop);


        foreach (var button in MenuButtons)
        {
            DrawMenuButton(g, button);
        }
    }

    private static void DrawCard(Graphics g, Card card, RectangleF location, bool isActive, Team team)
    {
        var borderPen = team == Team.Blue ? new Pen(Color.Blue, 0.1f) : new Pen(Color.Red, 0.1f);
        DrawRoundedTextBox(g, card.Name, Font, location, isActive ? borderPen : null, MoccasinBrush, BlackBrush, CornerRadius, StringFormats.CenterBottom);
        card.DrawCardGrid(g, new(location.X + 0.05f, location.Y + 0.05f), 1.6f);
    }

    private static void DrawRoundedBox(Graphics g, RectangleF bounds, Brush backgroundBrush, Pen? borderPen, float cornerRadius)
    {
        if (borderPen is not null)
        {
            g.DrawRoundedRectangleF(borderPen, bounds, cornerRadius);
        }

        g.FillRoundedRectangleF(backgroundBrush, bounds, cornerRadius);
    }

    private static void DrawRoundedTextBox(Graphics g, string text, Font font, RectangleF bounds, Pen? borderPen, Brush backgroundBrush, Brush textBrush, float cornerRadius, StringFormat stringFormat)
    {
        DrawRoundedBox(g, bounds, backgroundBrush, borderPen, cornerRadius);
        g.DrawString(text, font, textBrush, bounds, stringFormat);
    }

    private static void DrawMenuButton(Graphics g, MenuButton button)
    {
        DrawRoundedTextBox(g, button.Text, button.Font, button.Bounds, BlackPen, MatBrush, BlackBrush, button.CornerRadius, StringFormats.Center);
    }

    private PointF GridToView(float x, float y)
    {
        return new PointF(x + GridOrigin.X, y + GridOrigin.Y);
    }
}
