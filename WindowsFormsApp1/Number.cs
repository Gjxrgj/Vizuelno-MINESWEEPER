using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsFormsApp1
{
    public class Number
    {
        public readonly int Size = 20;
        public int X { get; set; }
        public int Y { get; set; }
        public int Num { get; set; }
        public Color Color { get; set; }

        public Number(int x, int y, int number)
        {
            this.Num = number;
            X = x;
            Y = y;
        }
        public void Draw(Graphics g)
        {
            if (Num == 1)
                this.Color = Color.Blue;
            else if (Num == 2)
                this.Color = Color.Green;
            else if (Num == 3)
                this.Color = Color.Red;
            else if (Num == 4)
                this.Color = Color.DarkBlue;
            else if (Num == 5)
                this.Color = Color.DarkRed;
            else if (Num == 6)
                this.Color = Color.LightBlue;
            else if (Num == 7)
                this.Color = Color.Black;
            else
                this.Color = Color.Gray;

            if(this.Num != 0)
            {
                Font font = new Font("Arial", Size);
                SolidBrush brush = new SolidBrush(this.Color);

                string text = Num.ToString();
                SizeF size = g.MeasureString(text, font);

                g.DrawString(text, font, brush, X - size.Width / 2, Y - size.Height / 2);

                // Dispose of the Font and SolidBrush objects
                font.Dispose();
                brush.Dispose();
            }
        }

    }
}
