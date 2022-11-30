using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class TerrainBlock
    {
        readonly static Color _DEFAULT_COLOR = Color.Transparent;
        readonly static Color _PLAYER_COLOR = Color.Yellow;
        readonly static Color _FOOD_COLOR = Color.Red;

        public int X_ID { get; set; }
        public int Y_ID { get; set; }
        public Point Position { get; set; }
        public int EdgeSize { get; set; } = 25;

        Color _backgroundColor;
        public Color BackgroundColor 
        {
            get => _backgroundColor; 
            set
            {
                _backgroundColor = value;
                Pen = new Pen(new SolidBrush(BackgroundColor), 3);
            }
        }

        public Pen Pen { get; set; }

        public TerrainBlock()
        {
            BackgroundColor = _DEFAULT_COLOR;
        }

        public void PaintAsPlayer()
        {
            BackgroundColor = _PLAYER_COLOR;
        }

        public void PaintAsTerrain()
        {
            BackgroundColor = _DEFAULT_COLOR;
        }

        public void PaintAsFood()
        {
            BackgroundColor = _FOOD_COLOR;
        }

        public void Draw(Graphics g, bool isRound = true)
        {
            var rect = new Rectangle(Position.X, Position.Y, EdgeSize, EdgeSize);
            if (isRound) g.FillEllipse(Pen.Brush, rect);
            else g.FillRectangle(Pen.Brush, rect);
        }
    }
}
