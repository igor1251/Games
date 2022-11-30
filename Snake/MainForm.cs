using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    public partial class MainForm : Form
    {
        const int _DEFAULT_TIMER_TICK_INTERVAL = 150;

        GameField gameField = new GameField();
        bool gameOver => gameField.GameOver();
        Way way = Way.Down;

        public MainForm()
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            BackColor = Color.Blue;

            scoreLabel.ForeColor = Color.Yellow;
            scoreValueLabel.ForeColor = Color.Yellow;

            scene.Paint += Scene_Paint;
            worldTimer.Tick += worldTimer_Tick;
            PreviewKeyDown += MainForm_PreviewKeyDown;
            gameField.Score.ScoreUpdated += Score_ScoreUpdated;

            gameField.Init();
            
            scene.Width = gameField.Width;
            scene.Height = gameField.Height;

            Width = gameField.Width + 190;
            Height = gameField.Height + 45;
            
            scene.Update();

            worldTimer.Interval = _DEFAULT_TIMER_TICK_INTERVAL;
            worldTimer.Start();
        }

        private void Score_ScoreUpdated(object sender, EventArgs e)
        {
            scoreValueLabel.Text = gameField.Score.Value.ToString();
        }

        void MovePlayer()
        {
            switch (way)
            {
                case Way.Up:
                    gameField.MoveUp();
                    break;
                case Way.Down:
                    gameField.MoveDown();
                    break;
                case Way.Left:
                    gameField.MoveLeft();
                    break;
                case Way.Right:
                    gameField.MoveRight();
                    break;
            }
        }

        void MainForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.W:
                case Keys.Up:
                    way = Way.Up;
                    break;
                case Keys.S:
                case Keys.Down:
                    way = Way.Down;
                    break;
                case Keys.A:
                case Keys.Left:
                    way = Way.Left;
                    break;
                case Keys.D:
                case Keys.Right:
                    way = Way.Right;
                    break;
            }
        }

        void Scene_Paint(object sender, PaintEventArgs e)
        {
            gameField.Update(e.Graphics);
        }

        void worldTimer_Tick(object sender, EventArgs e)
        {
            MovePlayer();
            scene.Invalidate();

            if (gameOver)
            {
                worldTimer.Stop();
                MessageBox.Show("Game over");
                Close();
            }
        }
    }
}
