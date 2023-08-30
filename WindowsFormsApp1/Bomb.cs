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
            x -= 10;
            y -= 15;
            if(x - 30 == X && y - 30 == Y)
                return true;
            if (x == X & y - 30 == Y)
                return true;
            if (x + 30 == X & y - 30 == Y)
                return true;
            if (x - 30 == X & y == Y)
                return true;
            if (x + 30 == X & y == Y)
                return true;
            if (x - 30 == X & y + 30 == Y)
                return true;
            if (y + 30 == Y & x == X)
                return true;
            if (x + 30 == X & y + 30 == Y)
                return true;
            return false;
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
            x -= 10;
            y -= 15;
            Console.WriteLine("X coordinate: " + x + " - Y coordinate: " + y);
            Console.WriteLine("X coordinate BOMB: " + X + " - Y coordinate BOMB: " + Y);
            // Check if the coordinates (x, y) are colliding with the bomb
            if (x == X && y == Y)
            {
                Console.WriteLine("TUKA");
                Console.WriteLine("X coordinate: " + (x - 10) + " - Y coordinate: " + (y - 15));
                Console.WriteLine("X coordinate BOMB: " + X + " - Y coordinate BOMB: " + Y);
                return true;    
            }
            return false;
        }
    }
}
