using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Bomb
    {
        public readonly int Size = 30;
        public int X { get; set; }
        public int Y { get; set; }

        public Bomb(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void Draw(Graphics graphics)
        {
            Brush brush = new SolidBrush(Color.Black);
            graphics.FillEllipse(brush, X, Y, Size, Size);
            brush.Dispose();
        }
        public bool IsAdjacent(int x, int y)
        {
            // Check if the coordinates (x, y) are adjacent to the bomb's position
            if (Math.Abs(X - x) <= 30 && Math.Abs(Y - y) <= 30)
            {
                return true; // Coordinates are adjacent to the bomb
            }

            return false; // Coordinates are not adjacent to the bomb
        }
        public bool IsClicked(int x, int y)
        {
            if ((this.X < x && this.X + Size > x) && (this.Y < y && this.Y + Size > y))
            {
                return true;
            }
            return false;
        }
        public bool IsColliding(int x, int y)
        {
            // Check if the coordinates (x, y) are colliding with the bomb
            if (Math.Abs(X - x) < 30 && Math.Abs(Y - y) < 30)
            {
                return true; // Coordinates are colliding with the bomb
            }
            return false; // Coordinates are not colliding with the bomb
        }
    }
}
