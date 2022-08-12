using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onitama
{
    public abstract class DrawTools
    {
        public static SolidBrush BlackBrush { get;} = new(Color.Black);
        public static SolidBrush RedBrush { get;} = new(Color.Red);
        public static SolidBrush BlueBrush { get;} = new(Color.Blue);
        public static SolidBrush DarkRedBrush { get;} = new(Color.DarkRed);
        public static SolidBrush DarkBlueBrush { get;} = new(Color.DarkBlue);
        public static SolidBrush WhiteBrush { get; } = new(Color.White);
        public static SolidBrush MoccasinBrush { get; } = new(Color.Moccasin);
        public static Pen BlackPen { get; } = new(BlackBrush, 0.05f);
    }
}
