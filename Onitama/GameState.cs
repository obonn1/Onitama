// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Onitama
{
    public class GameState
    {
        public Square[,] Grid { get; set; } = new Square[5, 5];

        public Card? ActiveCard { get; set; } = null;

        public bool IsMenuOpen { get; set; } = false;

        public int TutorialStep { get; set; } = 1;

        public BoardItem? activeCardLocation = null;

        public Point? ActiveSquare { get; set; }

        public Team CurrentTeam { get; set; }

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

        public PointF GridOrigin { get; set; }

        public List<Point> PossibleMoves { get; private set; } = new();

        public Point RedMaster { get; set; } = new Point(4, 2);

        public Point BlueMaster { get; set; } = new Point(0, 2);

        /// <summary>
        /// Initializes a new instance of the <see cref="GameState"/> class.
        /// </summary>
        public GameState()
        {
            this.BlueStudents = new List<Point>();
            this.RedStudents = new List<Point>();
            Random random = new();
            for (int i = 0; i < this.Grid.GetLength(0); i++)
            {
                for (int j = 0; j < this.Grid.GetLength(1); j++)
                {
                    if ((i == 0 && j == 2) || (i == 4 && j == 2)) this.Grid[i, j] = new Square();
                    else if (i == 0)
                    {
                        this.Grid[i, j] = new Square(Team.Blue);
                        this.BlueStudents.Add(new Point(i, j));
                    }
                    else if (i == 4)
                    {
                        this.Grid[i, j] = new Square(Team.Red);
                        this.RedStudents.Add(new Point(i, j));
                    }
                    else
                    {
                        this.Grid[i, j] = new Square();
                    }
                }
;
            }

            this.Grid[0, 2].IsMaster = true;
            this.Grid[0, 2].Team = Team.Blue;
            this.Grid[4, 2].IsMaster = true;
            this.Grid[4, 2].Team = Team.Red;
            this.CurrentTeam = random.Next(2) == 0 ? Team.Red : Team.Blue;
            while (this.Cards.Count < 5)
            {
                var randomCard = Card.Deck[random.Next(Card.Deck.Length)];
                if (!this.Cards.Contains(randomCard))
                {
                    this.Cards.Add(randomCard);
                }
            }

            this.BlueCards = new Card[2] { this.Cards[0], this.Cards[1] };
            this.RedCards = new Card[2] { Card.Invert(this.Cards[2]), Card.Invert(this.Cards[3]) };
            this.NeutralCard = this.Cards[4];
        }

        public void MouseUp(BoardItem item, Point point)
        {
            if (this.IsGameOver)
            {
                return;
            }

            if (this.IsMenuOpen)
            {
                if (item == BoardItem.BlueSurrender)
                {
                    this.CurrentTeam = Team.Blue;
                    this.IsGameOver = true;
                    this.IsMenuOpen = false;
                }

                if (item == BoardItem.RedSurrender)
                {
                    this.CurrentTeam = Team.Red;
                    this.IsGameOver = true;
                    this.IsMenuOpen = false;
                }

                if (item == BoardItem.Tutorial)
                {
                    this.TutorialStep = 1;
                    this.IsMenuOpen = false;
                }

                if (item == BoardItem.CloseMenu || item == BoardItem.OffMenu)
                {
                    this.IsMenuOpen = false;
                }

                if (item == BoardItem.CloseGame)
                {
                    Application.Exit();
                }
            }
            else if (item == BoardItem.Menu) this.IsMenuOpen = true;
            // deactivate Cards
            else if (item == this.activeCardLocation)
            {
                this.activeCardLocation = null;
                this.ActiveCard = null;
            }

            // activate Cards
            else if (item.ToString().Contains("Card"))
            {
                if ((item == BoardItem.BlueCard1 || item == BoardItem.BlueCard2) && this.CurrentTeam == Team.Blue)
                {
                    this.activeCardLocation = item;
                    this.ActiveCard = item switch
                    {
                        BoardItem.BlueCard1 => this.BlueCards[0],
                        BoardItem.BlueCard2 => this.BlueCards[1],
                        _ => null
                    };
                }

                if ((item == BoardItem.RedCard1 || item == BoardItem.RedCard2) && this.CurrentTeam == Team.Red)
                {
                    this.activeCardLocation = item;
                    this.ActiveCard = item switch
                    {
                        BoardItem.RedCard1 => this.RedCards[0],
                        BoardItem.RedCard2 => this.RedCards[1],
                        _ => null
                    };
                }
            }

            // deactivate square
            if (this.ActiveSquare == point)
            {
                this.ActiveSquare = null;
                this.PossibleMoves = new();
            }

            // activate square
            else if (item == BoardItem.Square && (this.Grid[point.X, point.Y].Team == this.CurrentTeam))
            {
                this.ActiveSquare = point;
            }

            // Loads possible moves
            this.PossibleMoves = new();
            if (this.ActiveSquare != null && this.ActiveCard != null)
            {

                for (var i = 0; i < 5; i++)
                {
                    for (var j = 0; j < 5; j++)
                    {
                        if (this.ActiveCard != null && this.ActiveCard.Moves.Contains(new Size(i - this.ActiveSquare.Value.X, j - this.ActiveSquare.Value.Y)) && this.Grid[i, j].Team != this.CurrentTeam)
                        {
                            this.PossibleMoves.Add(new Point(i, j));
                        }
                    }
                }
            }

            // move
            if (this.ActiveSquare != null && this.PossibleMoves.Contains(point))
            {
                this.Move((Point)this.ActiveSquare, point);
            }
        }

        public void Move(Point active, Point target)
        {

            if ((this.Grid[target.X, target.Y].IsMaster == true)
                || this.Grid[active.X, active.Y].IsMaster && (this.Grid[active.X, active.Y].Team == Team.Red && target == new Point(0, 2))
                || this.Grid[active.X, active.Y].IsMaster && (this.Grid[active.X, active.Y].Team == Team.Blue && target == new Point(4, 2)))
            {
                this.IsGameOver = true;
                return;
            }

            if (this.Grid[active.X, active.Y].Team == Team.Blue)
            {
                if (this.Grid[active.X, active.Y].IsMaster)
                {
                    this.BlueMaster = target;
                }

                this.BlueStudents.Remove(active);
                this.BlueStudents.Add(target);
                this.BlueCards = this.BlueCards.Append(this.NeutralCard).ToArray();
                List<Card> c = new();
                foreach (Card card in this.BlueCards)
                {
                    if (card != this.ActiveCard)
                    {
                        c.Add(card);
                    }
                }

                this.BlueCards = c.ToArray();
                this.NeutralCard = this.ActiveCard!;
            }

            if (this.Grid[active.X, active.Y].Team == Team.Red)
            {

                if (this.Grid[active.X, active.Y].IsMaster)
                {
                    this.RedMaster = target;
                }

                this.RedStudents.Remove(active);
                this.RedStudents.Add(target);
                this.RedCards = this.RedCards.Prepend(Card.Invert(this.NeutralCard!)).ToArray();
                List<Card> c = new();

                foreach (Card card in this.RedCards)
                {
                    if (card != this.ActiveCard)
                    {
                        c.Add(card);
                    }
                }

                this.RedCards = c.ToArray();
                this.NeutralCard = Card.Invert(this.ActiveCard!);
            }

            if (this.Grid[active.X, active.Y].Team == Team.Red && this.Grid[target.X, target.Y].Team == Team.Blue)
            {
                this.BlueStudents.Remove(target);
            }

            if (this.Grid[active.X, active.Y].Team == Team.Blue && this.Grid[target.X, target.Y].Team == Team.Red)
            {
                this.RedStudents.Remove(target);
            }

            this.Grid.SetValue(this.Grid[active.X, active.Y], target.X, target.Y);
            this.Grid.SetValue(new Square(), active.X, active.Y);
            this.CurrentTeam = this.CurrentTeam == Team.Blue ? Team.Red : Team.Blue;
            this.ResetActive();
        }

        public void ResetActive()
        {
            this.ActiveCard = null;
            this.ActiveSquare = null;
            this.activeCardLocation = null;
            this.PossibleMoves = new();
        }

        public List<Point> CanMoveSquares(Point point)
        {
            List<Point> result = new();
            for (var j = 0; j < 5; j++)
            {
                for (var i = 0; i < 5; i++)
                {
                    if (this.ActiveCard is not null && this.ActiveCard.Moves.Contains(new Size(i - point.X, j - point.Y)))
                    {
                        result.Add(new Point(i, j));
                    }
                }
            }

            return result;
        }
    }
}
