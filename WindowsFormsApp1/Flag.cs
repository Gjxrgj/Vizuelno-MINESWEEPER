using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Flag
    {
        public readonly int Size = 8;
        public int X { get; set; }
        public int Y { get; set; }

        public Flag(int x, int y)
        {
            X = x;
            Y = y;
        }
        public void Draw(Graphics graphics)
        {
            // Draw red triangle (flipped horizontally)
            Point[] trianglePoints = new Point[]
            {
        new Point(X, Y),
        new Point(X, Y + Size * 2),
        new Point(X + Size * 2, Y + Size)
            };

            Brush triangleBrush = new SolidBrush(Color.Red);
            graphics.FillPolygon(triangleBrush, trianglePoints);
            triangleBrush.Dispose();

            // Draw black line (vertical, starting from the top left corner of the triangle)
            Pen linePen = new Pen(Color.Black);
            linePen.Width = 3;  // Adjust line thickness
            int lineStartX = X;  // Start from the left side of the triangle
            int lineStartY = Y;  // Start from the top of the triangle
            int lineEndX = X;  // Stay on the left side of the triangle
            int lineEndY = Y + Size * 3;  // Extend the line further

            graphics.DrawLine(linePen, lineStartX, lineStartY, lineEndX, lineEndY);
            linePen.Dispose();
        }
        public bool IsClicked(int x, int y)
        {
            if ((this.X < x && this.X + 30 > x) && (this.Y < y && this.Y + 30 > y))
            {
                return true;
            }
            return false;
        }

    }
}
