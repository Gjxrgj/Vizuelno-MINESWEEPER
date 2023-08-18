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

        public Number(int x, int y, int number)
        {
            this.Num = number;
            X = x;
            Y = y;
        }
        public void Draw(Graphics g)
        {
            Font font = new Font("Arial", Size);
            SolidBrush brush = new SolidBrush(Color.Red);

            string text = Num.ToString();
            SizeF size = g.MeasureString(text, font);

            g.DrawString(text, font, brush, X - size.Width / 2, Y - size.Height / 2);

            // Dispose of the Font and SolidBrush objects
            font.Dispose();
            brush.Dispose();
        }

    }
}
