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
        public List<Flag> Flags{ get; set; }

        public Scene()
        {
            Bombs = new List<Bomb>();
            Boxes = new List<Box>();
            Numbers = new List<Number>();
            Flags = new List<Flag>();
        }
        public void DrawAll(Graphics graphics)
        {
            DrawBackground(graphics);
            foreach (Number number in Numbers)
            {
                number.Draw(graphics);
            }
            foreach (Box box in Boxes)
            {
                box.Draw(graphics);
            }
            foreach (Bomb bomb in Bombs)
            {
                bomb.Draw(graphics);
            }
            foreach (Flag flag in Flags)
            {
                flag.Draw(graphics);
            }
            
        }
        public void DrawBackground(Graphics g)
        { 
            // Define the starting coordinates and box dimensions
            int startX = 60;
            int startY = 30;
            int boxSize = 30;
            int rowCount = 9;
            int colCount = 9;

            // Define the outline color
            Pen outlinePen = Pens.Black;

            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < colCount; col++)
                {
                    // Calculate the coordinates of the current box
                    int x = startX + col * boxSize;
                    int y = startY + row * boxSize;

                    // Create a rectangle for the current box
                    Rectangle boxRect = new Rectangle(x, y, boxSize, boxSize);

                    // Fill the box with light gray color
                    g.FillRectangle(Brushes.LightGray, boxRect);

                    // Draw the outline for the box
                    g.DrawRectangle(outlinePen, boxRect);
                }
            }
        }
        public bool Gameover()
        {
            int UnclickedBoxes = 0;
            foreach(Box box in Boxes)
            {
                if (!box.clicked)
                {
                    UnclickedBoxes++;
                }
            }
            if (UnclickedBoxes == 10)
                return true;
            return false;
        }
        public void RemoveFlagsOnClickedSpaces()
        {
            List<Flag> flagsToRemove = new List<Flag>();

            foreach (Flag flag in Flags)
            {
                foreach (Box box in Boxes)
                {
                    if (box.clicked)
                    {
                        flagsToRemove.Add(flag);
                        break; // No need to check this flag against other boxes
                    }
                }
            }

            // Remove the flags
            /*foreach (Flag flagToRemove in flagsToRemove)
            {
                Flags.Remove(flagToRemove);
            }*/
        }
        public void AddOrRemoveFlag(int closestX, int closestY, int actualX, int actualY)
        {
            bool addFlag = true;

            Box clickedBox = Boxes.FirstOrDefault(box => box.IsClicked(actualX, actualY));
            if (clickedBox != null && clickedBox.clicked)
            {
                addFlag = false;
            }

            if (addFlag)
            {

                bool flagExists = Flags.Any(flag => flag.IsClicked(actualX, actualY));

                if (!flagExists)
                {
                    Flags.Add(new Flag(closestX, closestY));
                }
            }
        }
        public bool AnyFlagClicked(int x, int y)
        {
            foreach(Flag flag in Flags)
            {
                if (flag.IsClicked(x, y))
                    return true;
            }
            return false;
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
            Box clickedBox = Boxes.FirstOrDefault(box => box.IsClicked(x, y));

            if (clickedBox != null && !clickedBox.clicked)
            {
                if (ClickBomb(x, y))
                {

                    foreach (Bomb bomb in Bombs)
                    {
                        bomb.Covered = false;
                    }
                }
                else if (clickedBox.Number == 0)
                {
                    HashSet<Box> boxesToClick = new HashSet<Box>();

                    RecursiveClickZeros(clickedBox, boxesToClick);

                    foreach (Box boxToClick in boxesToClick)
                    {
                        boxToClick.clicked = true;
                        boxToClick.Color = Color.White;
                    }
                }
                else
                {
                    clickedBox.clicked = true;
                    clickedBox.Color = Color.White;
                }
            }
        }
        private void RecursiveClickZeros(Box box, HashSet<Box> clickedBoxes)
        {
            clickedBoxes.Add(box);

            foreach (Box adjacentBox in GetAdjacentBoxes(box.X, box.Y))
            {
                if (!clickedBoxes.Contains(adjacentBox))
                {
                    if (adjacentBox.Number == 0)
                    {
                        // If adjacent box is empty, recursively click it
                        RecursiveClickZeros(adjacentBox, clickedBoxes);
                    }
                    else if (adjacentBox.Number > 0)
                    {
                        // If adjacent box has a number, click it and stop recursion
                        adjacentBox.clicked = true;
                    }
                }
            }
        }



        private List<Box> GetAdjacentBoxes(int x, int y)
        {
            List<Box> adjacentBoxes = new List<Box>();

            if (IsWithinGridBounds(x, y - 30))
            {
                Box aboveBox = GetBoxAtCoordinates(x, y - 30);
                adjacentBoxes.Add(aboveBox);
            }

            if (IsWithinGridBounds(x, y + 30))
            {
                Box belowBox = GetBoxAtCoordinates(x, y + 30);
                adjacentBoxes.Add(belowBox);
            }

            if (IsWithinGridBounds(x - 30, y))
            {
                Box leftBox = GetBoxAtCoordinates(x - 30, y);
                adjacentBoxes.Add(leftBox);
            }

            if (IsWithinGridBounds(x + 30, y))
            {
                Box rightBox = GetBoxAtCoordinates(x + 30, y);
                adjacentBoxes.Add(rightBox);
            }

            return adjacentBoxes;
        }
        private bool IsWithinGridBounds(int x, int y)
        {
            return x >= 60 && x <= 300 && y >= 30 && y <= 270;
        }
        private Box GetBoxAtCoordinates(int boxX, int boxY)
        {
            return Boxes.FirstOrDefault(box => box.X == boxX && box.Y == boxY);
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
            int xBomb = 30;
            int yBomb = 0;
            for (int i = 0; i < 81; i++)
            {
                x += 30;
                xBomb += 30;

                if ((i + 1) % 9 == 0)
                {
                    x = xMargin;
                    xBomb = 60;
                }
                int num = 0;
                if (i % 9 == 0)
                {
                    y += 30;
                    yBomb += 30;
                }

                foreach (Bomb bomb in Bombs)
                {
                    if (bomb.IsAdjacent(x, y))
                    {
                        num++;
                    }
                }

                bool AddNumber = true;
                Console.WriteLine("X coordinate::" + x);

                foreach (Bomb bomb in Bombs)
                {
                    if (bomb.IsColliding(x, y))
                    {

                        AddNumber = false;
                        break;
                    }
                }
                if (AddNumber)
                {
                    Numbers.Add(new Number(x, y, num));
                    foreach (Box box in Boxes)
                    {
                        if (box.X == xBomb && box.Y == yBomb)
                        {
                            box.Number = num;
                        }
                    }
                }
            }
        }
    }
}
