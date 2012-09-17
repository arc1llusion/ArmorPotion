using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.TileEngine;
using Microsoft.Xna.Framework.Graphics;

namespace ArmorPotions.Tiles
{
    class WallTile : Tile
    {
        public WallTile(TileType tileType, Texture2D texture) : base(null)
        {
        }

        public override void onEvent(EventType sendEvent)
        {
            throw new NotImplementedException();
        }
    }
}
