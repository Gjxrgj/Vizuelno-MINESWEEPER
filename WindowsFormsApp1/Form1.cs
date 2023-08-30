using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Scene Scene;
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            NewGame();
        }
        public void NewGame()
        {
            Scene = new Scene();
            RandomizeBombs();
            Scene.CreateNumbers();
            CreateBoxes();
            timer1.Interval = 1000;
            timer1.Enabled = true;
            timer1.Start();
        }
        public void RandomizeBombs()
        {
            Random random = new Random();
            List<int> XList = new List<int>();
            List<int> YList = new List<int>();
            
            while(XList.Count < 10)
            {
                int randX = random.Next(0, 9);
                int randY = random.Next(1, 9);
                if (XList.Count == 0 && YList.Count == 0)
                {
                    XList.Add(randX * 30 + 60);
                    YList.Add(randY * 30);
                }
                else
                {
                    if (XList[XList.Count - 1] != randX * 30 + 60 || YList[YList.Count - 1] != randY * 30)
                    {
                        XList.Add(randX * 30 + 60);
                        YList.Add(randY * 30);
                    }
                }
            }
            for (int i = 0; i < XList.Count; i++)
            {
                Scene.AddBomb(XList[i], YList[i]);
            }
        }
        public void CreateBoxes()
        {
            int reset = 60;
            int x = reset;
            int y = 0;
            for (int i = 0; i < 81; i++)
            {
                if(i % 9 == 0)
                {
                    y += 30;
                    x = reset;
                }
                Scene.AddBox(x, y);
                x += 30;
            }
        }



        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Scene.DrawAll(e.Graphics);
            Invalidate();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Scene.ClickBox(e.X, e.Y);
            if (Scene.ClickBomb(e.X, e.Y))
            {
                if (MessageBox.Show("Would you like to play again?", "Game over", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    NewGame();
                }
                else
                {
                    Application.Exit();
                }
            }
            Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("Whatever");
        }
    }
}