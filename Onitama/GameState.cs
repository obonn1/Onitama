namespace Onitama
{
    public class GameState
    {
        public Square[,] Grid { get; set; } = new Square[5, 5];

        public Card? ActiveCard { get; set; } = null;

        public Point? ActiveSquare { get; set; }

        public BoardItem? ActiveCardLocation { get; set; }

        public Team CurrentTeam { get; set; }

        public bool IsMenuOpen { get; set; } = false;

        public int TutorialStep { get; set; } = 1;

        public bool IsGameOver { get; set; }

        public List<Card> Cards { get; set; } = new List<Card>(5);

        public Card[] BlueCards { get; set; }

        public Card[] RedCards { get; set; }

        public Card NeutralCard { get; set; }

        public List<Point> BlueStudents { get; set; } = new List<Point>()
        {
            new Point(0, 0),
            new Point(0, 1),
            new Point(0, 3),
            new Point(0, 4),
        };

        public List<Point> RedStudents { get; set; } = new List<Point>()
        {
            new Point(4, 0),
            new Point(4, 1),
            new Point(4, 3),
            new Point(4, 4),
        };

        public (BoardItem, Point)? MouseDownLocation { get; set; }

        public List<Point> PossibleMoves { get; set; } = new();

        public Point RedMaster { get; set; } = new Point(4, 2);

        public Point BlueMaster { get; set; } = new Point(0, 2);

        public GameState()
        {
            BlueStudents = new List<Point>();
            RedStudents = new List<Point>();
            Random random = new();
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if ((i == 0 && j == 2) || (i == 4 && j == 2))
                    {
                        Grid[i, j] = new Square();
                    }
                    else if (i == 0)
                    {
                        Grid[i, j] = new Square(Team.Blue);
                        BlueStudents.Add(new Point(i, j));
                    }
                    else if (i == 4)
                    {
                        Grid[i, j] = new Square(Team.Red);
                        RedStudents.Add(new Point(i, j));
                    }
                    else
                    {
                        Grid[i, j] = new Square();
                    }
                }
            }

            Grid[0, 2].IsMaster = true;
            Grid[0, 2].Team = Team.Blue;
            Grid[4, 2].IsMaster = true;
            Grid[4, 2].Team = Team.Red;
            CurrentTeam = random.Next(2) == 0 ? Team.Red : Team.Blue;
            while (Cards.Count < 5)
            {
                Card? randomCard = Card.Deck[random.Next(Card.Deck.Length)];
                if (!Cards.Contains(randomCard))
                {
                    Cards.Add(randomCard);
                }
            }

            BlueCards = new Card[2] { Cards[0], Cards[1] };
            RedCards = new Card[2] { Card.Invert(Cards[2]), Card.Invert(Cards[3]) };
            NeutralCard = Cards[4];
        }

        public void MouseUp(BoardItem item, Point point)
        {
            if (IsGameOver)
            {
                return;
            }

            if (IsMenuOpen)
            {
                if (item == BoardItem.BlueSurrender)
                {
                    CurrentTeam = Team.Blue;
                    IsGameOver = true;
                    IsMenuOpen = false;
                }

                if (item == BoardItem.RedSurrender)
                {
                    CurrentTeam = Team.Red;
                    IsGameOver = true;
                    IsMenuOpen = false;
                }

                if (item == BoardItem.Tutorial)
                {
                    TutorialStep = 1;
                    IsMenuOpen = false;
                }

                if (item == BoardItem.CloseMenu || item == BoardItem.OffMenu)
                {
                    IsMenuOpen = false;
                }

                if (item == BoardItem.CloseGame)
                {
                    Application.Exit();
                }
            }
            else if (item == BoardItem.Menu)
            {
                IsMenuOpen = true;
            }

            // deactivate Cards
            else if (item == ActiveCardLocation)
            {
                ActiveCardLocation = null;
                ActiveCard = null;
            }

            // activate Cards
            else if (item.ToString().Contains("Card"))
            {
                if ((item == BoardItem.BlueCard1 || item == BoardItem.BlueCard2) && CurrentTeam == Team.Blue)
                {
                    ActiveCardLocation = item;
                    ActiveCard = item switch
                    {
                        BoardItem.BlueCard1 => BlueCards[0],
                        BoardItem.BlueCard2 => BlueCards[1],
                        _ => null,
                    };
                }

                if ((item == BoardItem.RedCard1 || item == BoardItem.RedCard2) && CurrentTeam == Team.Red)
                {
                    ActiveCardLocation = item;
                    ActiveCard = item switch
                    {
                        BoardItem.RedCard1 => RedCards[0],
                        BoardItem.RedCard2 => RedCards[1],
                        _ => null,
                    };
                }
            }

            // deactivate square
            if (ActiveSquare == point)
            {
                ActiveSquare = null;
                PossibleMoves = new();
            }

            // activate square
            else if (item == BoardItem.Square && (Grid[point.X, point.Y].Team == CurrentTeam))
            {
                ActiveSquare = point;
            }

            // Loads possible moves
            PossibleMoves = new();
            if (ActiveSquare != null && ActiveCard != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (ActiveCard != null && ActiveCard.Moves.Contains(new Size(i - ActiveSquare.Value.X, j - ActiveSquare.Value.Y)) && Grid[i, j].Team != CurrentTeam)
                        {
                            PossibleMoves.Add(new Point(i, j));
                        }
                    }
                }
            }

            // move
            if (ActiveSquare != null && PossibleMoves.Contains(point))
            {
                Move((Point)ActiveSquare, point);
            }
        }

        public void Move(Point active, Point target)
        {
            if ((Grid[target.X, target.Y].IsMaster == true)
                || ((Grid[active.X, active.Y].IsMaster && Grid[active.X, active.Y].Team == Team.Red && target == new Point(0, 2))
                || (Grid[active.X, active.Y].IsMaster && (Grid[active.X, active.Y].Team == Team.Blue && target == new Point(4, 2)))))
            {
                IsGameOver = true;
                return;
            }

            if (Grid[active.X, active.Y].Team == Team.Blue)
            {
                if (Grid[active.X, active.Y].IsMaster)
                {
                    BlueMaster = target;
                }

                BlueStudents.Remove(active);
                BlueStudents.Add(target);
                BlueCards = BlueCards.Append(NeutralCard).ToArray();
                List<Card> c = new();
                foreach (Card card in BlueCards)
                {
                    if (card != ActiveCard)
                    {
                        c.Add(card);
                    }
                }

                BlueCards = c.ToArray();
                NeutralCard = ActiveCard!;
            }

            if (Grid[active.X, active.Y].Team == Team.Red)
            {
                if (Grid[active.X, active.Y].IsMaster)
                {
                    RedMaster = target;
                }

                RedStudents.Remove(active);
                RedStudents.Add(target);
                RedCards = RedCards.Prepend(Card.Invert(NeutralCard!)).ToArray();
                List<Card> c = new();

                foreach (Card card in RedCards)
                {
                    if (card != ActiveCard)
                    {
                        c.Add(card);
                    }
                }

                RedCards = c.ToArray();
                NeutralCard = Card.Invert(ActiveCard!);
            }

            if (Grid[active.X, active.Y].Team == Team.Red && Grid[target.X, target.Y].Team == Team.Blue)
            {
                BlueStudents.Remove(target);
            }

            if (Grid[active.X, active.Y].Team == Team.Blue && Grid[target.X, target.Y].Team == Team.Red)
            {
                RedStudents.Remove(target);
            }

            Grid.SetValue(Grid[active.X, active.Y], target.X, target.Y);
            Grid.SetValue(new Square(), active.X, active.Y);
            CurrentTeam = CurrentTeam == Team.Blue ? Team.Red : Team.Blue;
            ResetActive();
        }

        public void ResetActive()
        {
            ActiveCard = null;
            ActiveSquare = null;
            ActiveCardLocation = null;
            PossibleMoves = new();
        }

        public List<Point> CanMoveSquares(Point point)
        {
            List<Point> result = new();
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (ActiveCard is not null && ActiveCard.Moves.Contains(new Size(i - point.X, j - point.Y)))
                    {
                        result.Add(new Point(i, j));
                    }
                }
            }

            return result;
        }
    }
}
