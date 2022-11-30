using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    internal class GameField
    {
        readonly int width = 15;
        readonly int height = 15;
        readonly Player player = new Player();
        readonly Random random = new Random();
        readonly List<TerrainBlock> blocks = new List<TerrainBlock>();

        public Score Score { get; private set; } = new Score();
        public int Width => width * blocks.First().EdgeSize;
        public int Height => height * blocks.First().EdgeSize;

        TerrainBlock food;

        void CreateFood()
        {
            int newFoodX = 0;
            int newFoodY = 0;

            do
            {
                newFoodX = random.Next(0, width - 1);
                newFoodY = random.Next(0, height - 1);
            }
            while (player.ContainsBlock(newFoodX, newFoodY));

            food = blocks.First(item => item.X_ID == newFoodX && item.Y_ID == newFoodY);
            food.PaintAsFood();
        }

        void CreatePlayer()
        {
            player.Init(blocks);
            player.AssignDimensions(width, height);
        }

        void UpdateScore()
        {
            Score.Update();
        }

        void CheckCollision()
        {
            if (player != null && food != null)
            {
                if (player.Head.X_ID == food.X_ID && player.Head.Y_ID == food.Y_ID)
                {
                    player.IncreseBody();
                    CreateFood();
                    UpdateScore();
                }
            }
        }

        public void MoveRight()
        {
            player.MoveRight(blocks);
            CheckCollision();
        }

        public void MoveLeft()
        {
            player.MoveLeft(blocks);
            CheckCollision();
        }

        public void MoveUp()
        {
            player.MoveUp(blocks);
            CheckCollision();
        }

        public void MoveDown()
        {
            player.MoveDown(blocks);
            CheckCollision();
        }

        public void Init()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    var block = new TerrainBlock
                    {
                        X_ID = i,
                        Y_ID = j,
                    };
                    block.Position = new Point(i * block.EdgeSize, j * block.EdgeSize);
                    blocks.Add(block);
                }
            }
            CreatePlayer();
            CreateFood();
        }

        public bool GameOver()
        {
            return player.EatsHimself();
        }

        public void Update(Graphics g)
        {
            g.Clear(Color.Black);
            foreach (var block in blocks)
            {
                block.Draw(g, false);
            }
        }
    }
}
