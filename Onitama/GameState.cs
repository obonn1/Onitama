using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    internal class GameState
    {
        public Square[,] Grid { get; set; } = new Square[5, 5];
        public GameState()
        {
            for (int i = 0; i < Grid.GetLength(0); i++)
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (i == 0) Grid[i, j] = new Square(HasPiece.Blue);
                    else if (i == 4) Grid[i, j] = new Square(HasPiece.Red);
                    else Grid[i, j] = new Square();
                };
            Grid[0, 2].IsMaster = true;
            Grid[4, 2].IsMaster = true;
        }
    }
}
