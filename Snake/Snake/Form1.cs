using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        private int dirX, dirY;
        private int width = 900;
        private int height = 800;
        private int sizeOfSides = 40;
        private int score = 0;
        private int rI, rJ;
        private PictureBox fruit;
        private PictureBox[] snake = new PictureBox[400];
        private Label labelScore;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Змейка";
            this.Width = width;
            this.Height = height;
            dirX = 1;
            dirY = 0;
            labelScore = new Label();
            labelScore.Text = "Счет: 0";
            labelScore.Location = new Point(810, 10);
            this.Controls.Add(labelScore);
            snake[0] = new PictureBox();
            snake[0].Location = new Point(201, 201);
            snake[0].Size = new Size(sizeOfSides-1, sizeOfSides-1);
            snake[0].BackColor = Color.Green;
            this.Controls.Add(snake[0]);
            fruit = new PictureBox();
            fruit.BackColor = Color.Red;
            fruit.Size = new Size(sizeOfSides, sizeOfSides);
            GenerateMap();
            GenerateFruit();
            timer.Tick += new EventHandler(Update);
            timer.Interval = 200;
            timer.Start();
            this.KeyDown += new KeyEventHandler(OKP);
        }

        private void GenerateFruit()
        {
            Random r = new Random();
            rI = r.Next(0, height-sizeOfSides);
            int tempI = rI % sizeOfSides;
            rI -= tempI;
            rJ = r.Next(0, height - sizeOfSides);
            int tempJ = rJ % sizeOfSides;
            rJ -= tempJ;
            rI++;
            rJ++;
            fruit.Location = new Point(rI, rJ);
            this.Controls.Add(fruit);
        }

        private void CheckBorders()
        {
            if (snake[0].Location.X < 0)
            {
                for(int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);            
                }
                score = 0;
                labelScore.Text = "Счет: " + score;
                dirX = 1;
            }
            if (snake[0].Location.X > height)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                labelScore.Text = "Счет: " + score;
                dirX = -1;
            }
            if (snake[0].Location.Y < 0)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                labelScore.Text = "Счет: " + score;
                dirY = 1;
            }
            if (snake[0].Location.Y > height)
            {
                for (int i = 1; i <= score; i++)
                {
                    this.Controls.Remove(snake[i]);
                }
                score = 0;
                labelScore.Text = "Счет: " + score;
                dirY = -1;
            }
        }

        private void EatItself()
        {
            for(int i=1;i< score; i++)
            {
                if(snake[0].Location == snake[i].Location)
                {
                    for (int j = i; j <= score; j++)
                        this.Controls.Remove(snake[j]);
                    score = score - (score - i + 1);
                    labelScore.Text = "Счет: " + score;
                }
            }
        }

        private void EatFruit()
        {
            if(snake[0].Location.X == rI && snake[0].Location.Y == rJ)
            {
                labelScore.Text = "Счет: " + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score - 1].Location.X + 40 * dirX, snake[score - 1].Location.Y - 40 * dirY);
                snake[score].Size = new Size(sizeOfSides-1, sizeOfSides-1);
                snake[score].BackColor = Color.Green;
                this.Controls.Add(snake[score]);
                GenerateFruit();
            }
        }

        private void GenerateMap()
        {
            for(int i = 0; i < width / sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, sizeOfSides * i);
                pic.Size = new Size(width - 100, 1);
                this.Controls.Add(pic);
            }
            for (int i = 0; i <= height / sizeOfSides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point( sizeOfSides * i,0);
                pic.Size = new Size(1,width);
                this.Controls.Add(pic);
            }
        }

        private void MoveSnake()
        {
            for(int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake[0].Location = new Point(snake[0].Location.X + dirX * (sizeOfSides), snake[0].Location.Y + dirY * (sizeOfSides));
            EatItself();
        }

        private void Update(Object myObject,EventArgs eventsArgs)
        {
            CheckBorders();
            EatFruit();
            MoveSnake();
        }

        private void OKP(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    dirX = 1;
                    dirY = 0;
                    break;
                case "Left":
                    dirX = -1;
                    dirY = 0;
                    break;
                case "Up":
                    dirY = -1;
                    dirX = 0;
                    break;
                case "Down":
                    dirY = 1;
                    dirX = 0;
                    break;
            }
        }
    }
}
