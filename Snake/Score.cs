using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    internal class Score
    {
        public delegate void ScoreUpdatedEventHandler(object sender, EventArgs e);
        public event ScoreUpdatedEventHandler ScoreUpdated;
        public Font Font { get; set; } = new Font("Comic Sans MS", 25, FontStyle.Bold, GraphicsUnit.Pixel);
        public Brush Brush { get; set; } = new SolidBrush(Color.Yellow);

        public int Value { get; private set; }
        
        public void Update()
        {
            Value++;
            ScoreUpdated.Invoke(this, new EventArgs());
        }

        public void Draw(Graphics g, PointF pos)
        {
            g.DrawString($"Score: {Value}", Font, Brush, pos);
        }
    }
}
