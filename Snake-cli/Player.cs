using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_cli
{
    internal class Player
    {
        int x = 0;
        int y = 0;
        int width = 0;
        int height = 0;
        int length = 5;

        readonly List<TerrainBlock> body = new();
        
        public TerrainBlock Head => body.Last();

        void MovePlayer(IList<TerrainBlock> area)
        {
            var blockToAdd = area.First(item => item.X_ID == x && item.Y_ID == y);
            blockToAdd.PaintAsPlayer();
            body.Add(blockToAdd);
            if (body.Count > length)
            {
                var blockToRemove = body.First();
                blockToRemove.PaintAsTerrain();
                body.Remove(blockToRemove);
            }
        }

        public void Init(IList<TerrainBlock> area)
        {
            for (int i = 0; i < length; i++)
            {
                body.Add(area[i]);
                body.Last().PaintAsPlayer();
            }
            y = length - 1;
        }

        public void AssignDimensions(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void MoveRight(IList<TerrainBlock> area)
        {
            x++;
            if (x == width) x = 0;
            MovePlayer(area);
        }

        public void MoveLeft(IList<TerrainBlock> area)
        {
            x--;
            if (x < 0) x = width - 1;
            MovePlayer(area);
        }

        public void MoveUp(IList<TerrainBlock> area)
        {
            y--;
            if (y < 0) y = height - 1;
            MovePlayer(area);
        }

        public void MoveDown(IList<TerrainBlock> area)
        {
            y++;
            if (y == height) y = 0;
            MovePlayer(area);
        }

        public void IncreseBody()
        {
            length++;
        }

        public bool ContainsBlock(int x_id, int y_id)
        {
            return body.FirstOrDefault(item => item.X_ID == x_id && item.Y_ID == y_id) != null;
        }

        public bool EatsHimself()
        {
            return body.Where(item => item.X_ID == Head.X_ID && item.Y_ID == Head.Y_ID).Count() > 1;
        }
    }
}
