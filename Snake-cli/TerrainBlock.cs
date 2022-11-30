using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_cli
{
    internal class TerrainBlock
    {
        readonly static ConsoleColor _DEFAULT_COLOR = ConsoleColor.Blue;
        readonly static ConsoleColor _PLAYER_COLOR = ConsoleColor.Green;
        readonly static ConsoleColor _FOOD_COLOR = ConsoleColor.Red;

        public int X_ID { get; set; }
        public int Y_ID { get; set; }
        public Point Position { get; set; }

        BlockAffiliation affiliation;

        public TerrainBlock()
        {
            affiliation = BlockAffiliation.Terrain;
        }

        public void PaintAsPlayer()
        {
            affiliation = BlockAffiliation.Player;
        }

        public void PaintAsTerrain()
        {
            affiliation = BlockAffiliation.Terrain;
        }

        public void PaintAsFood()
        {
            affiliation = BlockAffiliation.Food;
        }

        public void Draw()
        {
            Console.SetCursorPosition(X_ID, Y_ID);
            switch (affiliation)
            {
                case BlockAffiliation.Player: Console.BackgroundColor = _PLAYER_COLOR; break;
                case BlockAffiliation.Food: Console.BackgroundColor = _FOOD_COLOR; break;
                case BlockAffiliation.Terrain: Console.BackgroundColor = _DEFAULT_COLOR; break;
            }
            Console.Write(" ");
        }
    }
}
