using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MazeGenerator
{
    public partial class MainForm : Form
    {
        private const int CellSize = 20;
        private int mazeWidth = 15;
        private int mazeHeight = 15;
        private bool[,] maze;
        private Point playerPosition;
        private Point endPosition;
        private bool gameWon = false;

        public MainForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.KeyDown += MainForm_KeyDown;

            GenerateMaze();

            ClientSize = new Size(
                mazeWidth * CellSize + 20,
                mazeHeight * CellSize + 100
            );
        }

        private void MazeSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (mazeSizeComboBox.SelectedIndex)
            {
                case 0: mazeWidth = mazeHeight = 11; break;
                case 1: mazeWidth = mazeHeight = 15; break;
                case 2: mazeWidth = mazeHeight = 21; break;
                case 3: mazeWidth = mazeHeight = 31; break;
            }

            ClientSize = new Size(
                mazeWidth * CellSize + 20,
                mazeHeight * CellSize + 100
            );

            GenerateMaze();
            Invalidate();
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            GenerateMaze();
            Invalidate();
        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int yOffset = 80;

            using (Brush wallBrush = new SolidBrush(Color.Black))
            {
                for (int y = 0; y < mazeHeight; y++)
                {
                    for (int x = 0; x < mazeWidth; x++)
                    {
                        if (maze[y, x])
                        {
                            g.FillRectangle(wallBrush,
                                10 + x * CellSize,
                                yOffset + y * CellSize,
                                CellSize, CellSize);
                        }
                    }
                }
            }

            using (Brush playerBrush = new SolidBrush(Color.Blue))
            {
                g.FillRectangle(playerBrush,
                    10 + playerPosition.X * CellSize,
                    yOffset + playerPosition.Y * CellSize,
                    CellSize, CellSize);
            }

            using (Brush exitBrush = new SolidBrush(Color.Red))
            {
                g.FillRectangle(exitBrush,
                    10 + endPosition.X * CellSize,
                    yOffset + endPosition.Y * CellSize,
                    CellSize, CellSize);
            }

            using (Pen gridPen = new Pen(Color.LightGray))
            {
                for (int y = 0; y <= mazeHeight; y++)
                {
                    g.DrawLine(gridPen,
                        10, yOffset + y * CellSize,
                        10 + mazeWidth * CellSize, yOffset + y * CellSize);
                }

                for (int x = 0; x <= mazeWidth; x++)
                {
                    g.DrawLine(gridPen,
                        10 + x * CellSize, yOffset,
                        10 + x * CellSize, yOffset + mazeHeight * CellSize);
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameWon)
                return;

            int newX = playerPosition.X;
            int newY = playerPosition.Y;

            switch (e.KeyCode)
            {
                case Keys.Up: newY--; break;
                case Keys.Down: newY++; break;
                case Keys.Left: newX--; break;
                case Keys.Right: newX++; break;
                default: return;
            }

            if (newX >= 0 && newX < mazeWidth &&
                newY >= 0 && newY < mazeHeight &&
                !maze[newY, newX])
            {
                playerPosition = new Point(newX, newY);

                if (playerPosition.X == endPosition.X && playerPosition.Y == endPosition.Y)
                {
                    gameWon = true;
                    statusLabel.Text = "Congratulations! You won!";
                }

                Invalidate();
            }
        }

        private void GenerateMaze()
        {
            maze = new bool[mazeHeight, mazeWidth];

            for (int y = 0; y < mazeHeight; y++)
            {
                for (int x = 0; x < mazeWidth; x++)
                {
                    maze[y, x] = true;
                }
            }

            Random random = new Random();
            Stack<Point> stack = new Stack<Point>();

            Point start = new Point(1, 1);
            maze[start.Y, start.X] = false;
            stack.Push(start);

            while (stack.Count > 0)
            {
                Point current = stack.Peek();
                List<Point> unvisitedNeighbors = GetUnvisitedNeighbors(current);

                if (unvisitedNeighbors.Count > 0)
                {
                    Point next = unvisitedNeighbors[random.Next(unvisitedNeighbors.Count)];
                    int wallX = current.X + (next.X - current.X) / 2;
                    int wallY = current.Y + (next.Y - current.Y) / 2;
                    maze[wallY, wallX] = false;
                    maze[next.Y, next.X] = false;
                    stack.Push(next);
                }
                else
                {
                    stack.Pop();
                }
            }

            playerPosition = new Point(1, 1);
            endPosition = new Point(mazeWidth - 2, mazeHeight - 2);
            maze[endPosition.Y, endPosition.X] = false;

            gameWon = false;
            statusLabel.Text = "Navigate to the red square to win!";
        }

        private List<Point> GetUnvisitedNeighbors(Point p)
        {
            List<Point> neighbors = new List<Point>();
            Point[] directions = {
                new Point(0, -2), new Point(2, 0),
                new Point(0, 2), new Point(-2, 0)
            };

            foreach (Point dir in directions)
            {
                int newX = p.X + dir.X;
                int newY = p.Y + dir.Y;

                if (newX > 0 && newX < mazeWidth - 1 &&
                    newY > 0 && newY < mazeHeight - 1 &&
                    maze[newY, newX])
                {
                    neighbors.Add(new Point(newX, newY));
                }
            }

            return neighbors;
        }
    }
}
