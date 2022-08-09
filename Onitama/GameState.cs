using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    public class GameState
    {
        public Square[,] Grid { get; set; } = new Square[5, 5];
        public Card? ActiveCard { get; set; } = null;

        public BoardItem? activeCardLocation = null;
        public (int X, int Y)? ActiveSquare { get; set; }
        public Team CurrentTeam { get; set; }
        public bool IsGameOver { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>(5);
        public Card[] BlueCards { get; set; }
        public Card[] RedCards { get; set; }
        public Card? NeutralCard { get; set; }
        public List<(float, float)> BlueStudents { get; set; } = new List<(float, float)>();
        public List<(float, float)> RedStudents { get; set; } = new List<(float, float)>();
        public PointF mouseDownLocation { get; set; }
        public PointF mouseLocation { get; set; }
        public PointF GridOrigin { get; set; }

        public GameState()
        {
            Random random = new Random();
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (i == 0)
                    {
                        Grid[i, j] = new Square(Team.Blue);
                        BlueStudents.Add(((float)i, (float)j));
                    }
                    else if (i == 4)
                    {
                        Grid[i, j] = new Square(Team.Red);
                        RedStudents.Add(((float)i, (float)j));
                    }
                    else
                    {
                        Grid[i, j] = new Square();
                    }
                };
            }

            Grid[0, 2].IsMaster = true;
            Grid[4, 2].IsMaster = true;
            CurrentTeam = random.Next(2) == 1 ? Team.Red : Team.Blue;
            while (Cards.Count < 5)
            {
                var randomCard = Card.Deck[random.Next(Card.Deck.Length)];
                if (!Cards.Contains(randomCard))
                    Cards.Add(randomCard);
            }

            BlueCards = new Card[2] { Cards[0], Cards[1] };
            RedCards = new Card[2] { Card.Invert(Cards[2]), Card.Invert(Cards[3]) };
            NeutralCard = Cards[4];
        }

        public void MouseUp(float x, float y)
        {
            (BoardItem, (int X, int Y))? item = FindItem(new PointF(x, y));
            Square? clickedSquare = null;
            if (FindItem(new PointF(x, y)) != null)
            {
                //activate Cards
                if ((item.Value.Item1 != BoardItem.Square) && (activeCardLocation == null)) activeCardLocation = item.Value.Item1;
                //deactivate Cards
                if (item.Value.Item1 == activeCardLocation) ActiveCard = null;
                //
                if (item.Value.Item1 == BoardItem.Square) clickedSquare = Grid[item.Value.Item2.X, item.Value.Item2.Y];
                //activate square
                if ((clickedSquare != null)
                    && (ActiveSquare == null) 
                    && (clickedSquare.Team == CurrentTeam))
                {
                    ActiveSquare = item.Value.Item2;
                }
                //deactivate square
                if (ActiveSquare == item.Value.Item2) ActiveSquare = null;
                //move
                if (ActiveSquare != null
                    && CanMoveSquares(ActiveSquare.Value.X, ActiveSquare.Value.Y).Contains(FindItem(new PointF(item.Value.Item2.X, item.Value.Item2.Y))!.Value.Item2)) Move(Grid[ActiveSquare.Value.X, ActiveSquare.Value.Y], Grid[item.Value.Item2.X, item.Value.Item2.Y]);
            }
        }

        public (BoardItem, (int X, int Y))? FindItem(PointF point)
        {
            int squareX = 7;
            int squareY = 7;
            if (((point.X - 0.425f) * (2.225f - point.X)) > 0 && (((point.Y - 1.88f) * (4.13f - point.Y)) > 0)) return (BoardItem.BlueCard1, (0, 0));
            if (((point.X - 0.425f) * (2.225f - point.X)) > 0 && (((point.Y - 4.36f) * (6.61f - point.Y)) > 0)) return (BoardItem.BlueCard2, (0, 0));
            if (((point.X - 7.7f) * (9.5f - point.X)) > 0 && (((point.Y - 1.88f) * (4.13f - point.Y)) > 0)) return (BoardItem.RedCard1, (0, 0));
            if (((point.X - 7.7f) * (9.5f - point.X)) > 0 && (((point.Y - 4.36f) * (6.61f - point.Y)) > 0)) return (BoardItem.RedCard1, (0, 0));

            for (int i = 0; i < 4; i++)
            {
                if ((point.X > GridOrigin.X + i + 0.1f) && (point.X < GridOrigin.X + i + 0.9f)) squareX = i;
            }
            for (int i = 0; i < 4; i++)
            {
                if ((point.Y > GridOrigin.Y + i + 0.1f) && (point.Y < GridOrigin.Y + i + 0.9f)) squareX = i;
            }
            if ((point.X > GridOrigin.X/5 + 0.1f) && (point.X < GridOrigin.X / 5 + 0.9f)
                &&
               (point.Y > GridOrigin.Y/5 + 0.1f) && (point.Y < GridOrigin.Y/5 + 0.9f))
            {
                return (BoardItem.Square, (squareX, squareY));
            }
            return null;
        }

        public void Move(Square origin, Square target)
        {

            if (target.IsMaster == true 
                || (origin.Team == Team.Red && target == Grid[0, 2]) 
                || (origin.Team == Team.Blue && target == Grid[4, 2]))
            {
                IsGameOver = true;
            }
            target = origin;
            origin = new Square();
        }

        public List<(int X, int Y)> CanMoveSquares(int x, int y)
        {
            if (x < 0 || y < 0) throw new ArgumentOutOfRangeException();
            List<(int X, int Y)> result = new List<(int X, int Y)>();
            for (var i = 0; i < 5; i++)
                for (var j = 0; j < 5; j++)
                {
                    if (ActiveCard is not null && ActiveCard.Moves.Contains(new Size(i - x, j - y)))
                    {
                        result.Add((i, j));
                    }
                }
            return result;
        }

    }
    public enum Team { Red, Blue }
    public enum BoardItem
    {
        BlueCard1,
        BlueCard2,
        RedCard1,
        RedCard2,
        Square,
    }
}
