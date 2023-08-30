using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class Scene
    {
        public List<Bomb> Bombs { get; set; }
        public List<Box> Boxes{ get; set; }
        public List<Number> Numbers{ get; set; }

        public Scene()
        {
            Bombs = new List<Bomb>();
            Boxes = new List<Box>();
            Numbers = new List<Number>();
        }
        public void DrawAll(Graphics graphics)
        {
            foreach (Box box in Boxes)
            {
                box.Draw(graphics);
            }
            foreach (Bomb bomb in Bombs)
            {
                bomb.Draw(graphics);
            }
            foreach (Number number in Numbers)
            {
                number.Draw(graphics);
            }
        }
        public void AddBomb(int x, int y)
        {
            Bombs.Add(new Bomb(x, y));
        }
        public void AddBox(int x, int y)
        {
            Boxes.Add(new Box(x, y));
        }
        public void AddNumber(int x, int y, int number)
        {
            Numbers.Add(new Number(x, y, number));
        }
        public void ClickBox(int x, int y)
        {
            for(int i = 0; i < Boxes.Count; i++)
            {
                if(Boxes[i].IsClicked(x, y))
                {
                    Boxes.RemoveAt(i);
                    i--;
                }
            }
        }
        public bool ClickBomb(int x, int y)
        {
            for (int i = 0; i < Bombs.Count; i++)
            {
                if (Bombs[i].IsClicked(x, y))
                {
                    return true;
                }
            }
            return false;
        }
        public void CreateNumbers()
        {
            int xMargin = 70;
            int x = xMargin;
            int y = 15;
            int j = 0;
            for (int i = 0; i < 81; i++)
            {
                j++;
                if (j > 9)
                {
                    j = 0;
                }
                x += j * 30;
                //Reset for X coordinate
                if (x > 330)
                {
                    x = xMargin;
                }
                int num = 0;
                if(i % 9 == 0)
                {
                    y += 30;
                }
                // Find if the number should be increased
                foreach(Bomb bomb in Bombs)
                {
                    if (bomb.IsAdjacent(x, y))
                    {
                        num++;
                    }
                }
                bool AddNumber = true;
                Console.WriteLine("X coordinate::" + x);
                //Find if the number being added is on top of a bomb
                foreach (Bomb bomb in Bombs)
                {
                    if(bomb.IsColliding(x, y))
                    {
                       
                        AddNumber = false;
                        break;
                    }
                }
                if (AddNumber)
                {
                    Numbers.Add(new Number(x, y, num));
                }
            }
        }
    }
}
