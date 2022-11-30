using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Snake_cli
{
    internal class GameField
    {
        readonly Player player = new Player();
        readonly Random random = new Random();
        readonly List<TerrainBlock> blocks = new List<TerrainBlock>();

        public int Score { get; private set; } = 0;
        public int Width { get; set; } = 15;
        public int Height { get; set; } = 15;

        TerrainBlock? food;

        void CreateFood()
        {
            int newFoodX = 0;
            int newFoodY = 0;

            do
            {
                newFoodX = random.Next(0, Width - 1);
                newFoodY = random.Next(0, Height - 1);
            }
            while (player.ContainsBlock(newFoodX, newFoodY));

            food = blocks.First(item => item.X_ID == newFoodX && item.Y_ID == newFoodY);
            food.PaintAsFood();
        }

        void CreatePlayer()
        {
            player.Init(blocks);
            player.AssignDimensions(Width, Height);
        }

        void UpdateScore()
        {
            Score++;
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
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    var block = new TerrainBlock
                    {
                        X_ID = i,
                        Y_ID = j,
                    };
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

        public void Update()
        {
            Console.SetCursorPosition(0, 0);
            foreach (var block in blocks)
            {
                block.Draw();
            }
        }
    }
}
