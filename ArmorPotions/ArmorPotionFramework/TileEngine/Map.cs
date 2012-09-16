using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmorPotionFramework.TileEngine
{
    public class Map
    {

        Tile[,] tileMapBotom = new Tile[255, 255];
        Tile[,] tileMapTop = new Tile[255, 255];

        public Map(Tile[,] mapTop, Tile[,] mapBottom)
        {
            tileMapTop = mapTop;
            tileMapBotom = mapBottom;
        }
        public void Update()
        {
        }

        public void Draw()
        {
        }
    }
}
