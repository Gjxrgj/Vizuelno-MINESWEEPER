using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Box
    {
        public const int Size = 30;
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }
        public int Number { get; set; }
        public bool clicked { get; set; }

        public Box(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
            this.Color = Color.Gray;
            this.clicked = false;
        }

        public void Draw(Graphics graphics)
        {
            Brush brush = new SolidBrush(this.Color);
            Pen pen = new Pen(Color.Black);

            if (clicked)
            {

                Color fullyTransparentColor = Color.FromArgb(0, 0, 0, 0);

                graphics.FillRectangle(new SolidBrush(fullyTransparentColor), this.X + 5, this.Y + 5, Size - 10, Size - 10);
            }

            else
            {
                graphics.FillRectangle(brush, this.X, this.Y, Size, Size);
                graphics.DrawRectangle(pen, this.X, this.Y, Size, Size);
            }

            brush.Dispose();
            pen.Dispose();
        }
        public bool IsClicked(int x, int y)
        {
            if ((this.X < x && this.X + Size > x) && (this.Y < y && this.Y + Size > y))
            {
                return true;
            }
            return false;
        }
    }
}
