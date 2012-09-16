using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ArmorPotionFramework.TileEngine
{
    public class TileSheet
    {
        Texture2D _tileSheet;
        Texture2D[] _tiles;

        public TileSheet(Texture2D setTileSheet)
        {
            _tileSheet = setTileSheet;

        }
        public TileSheet(String fileLocation)
        {

        }

        public Texture2D[] breakTileSheet(int tileWidth, int tileHeight)
        {
            for (int i = 0; i < (int)(_tileSheet.Width / tileWidth); i++)
            {
                for (int c = 0; c < (int)(_tileSheet.Height / tileHeight); c++)
                {

                }
            }
            return null;
        }
    }
}
