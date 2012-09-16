using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.TileEngine;

namespace ArmorPotions.Tiles
{
    class FloorTile : Tile
    {
        public FloorTile()
            : base(null)
        {

        }

        public override void onEvent(EventType sendEvent)
        {
            throw new NotImplementedException();
        }
    }
}
