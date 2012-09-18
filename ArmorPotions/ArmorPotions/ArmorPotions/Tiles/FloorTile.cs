using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.TileEngine;
using Microsoft.Xna.Framework.Graphics;

namespace ArmorPotions.Tiles
{
    class FloorTile : Tile
    {
        public FloorTile(TileType tileType, int tileID, Texture2D texture)
            : base(tileType, tileID, texture)
        {

        }

        public override void onEvent(EventType sendEvent)
        {
            throw new NotImplementedException();
        }
    }
}
