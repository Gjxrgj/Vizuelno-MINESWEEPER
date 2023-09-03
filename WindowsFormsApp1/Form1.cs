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
            CreateBoxes();
            Scene.CreateNumbers();
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

        private int GetClosestLowerValue(int value, int[] values)
        {

            int closestValue = values[0];

            foreach (int v in values)
            {
                if (v <= value)
                {
                    closestValue = v;
                }
                else
                {
                    break;
                }
            }

            return closestValue;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int[] xValues = { 70, 100, 130, 160, 190, 220, 250, 280, 310 };
            int[] yValues = { 30, 60, 90, 120, 150, 180, 210, 240, 270 };

            int closestX = GetClosestLowerValue(e.X, xValues);
            int closestY = GetClosestLowerValue(e.Y, yValues);

            if (e.Button == MouseButtons.Right)
            {
                if(Scene.Flags.Count < 10)
                {
                Scene.AddOrRemoveFlag(closestX, closestY, e.X, e.Y);
                }
                flagRemaininglbl.Text = "Flags remaining: " + (10 - Scene.Flags.Count);
                flagRemaininglbl.Refresh();
            }
            else
            {
                if (!Scene.AnyFlagClicked(e.X, e.Y))
                {
                    //Game lose
                    if (Scene.ClickBomb(e.X, e.Y))
                    {
                        Scene.ClickBox(e.X, e.Y);
                        Invalidate();
                        if (MessageBox.Show("You lost. Would you like to play again?", "Game Over", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            NewGame();
                            return;
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                    //Game won
                    else if (Scene.Gameover())
                    {
                        Scene.ClickBox(e.X, e.Y);
                        Invalidate();
                        if (MessageBox.Show("You won!!! Would you like to play again?", "Game over", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            NewGame();
                            return;
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                    Scene.ClickBox(e.X, e.Y);
                }
                Scene.RemoveFlagsOnClickedSpaces();
            }
            Invalidate();
        }
    }
}
