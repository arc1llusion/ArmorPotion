using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.TileEngine;
using Microsoft.Xna.Framework.Graphics;

namespace ArmorPotions.Tiles
{
    class LavaTile : Tile
    {
        public LavaTile(TileType tileType, int tileID, Texture2D lavaTexture, Texture2D cooledLavaTexture, bool isCooled)
            : base(tileType, tileID, lavaTexture)
        {
        }
        bool _isCooled;

        public override void onEvent(EventType sendEvent)
        {
            throw new NotImplementedException();
        }
    }
}
