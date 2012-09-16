using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.TileEngine;

namespace ArmorPotions.Tiles
{
    class LavaTile : Tile
    {
        public LavaTile()
            : base(null)
        {
        }
        bool _isCooled;

        public override void onEvent(EventType sendEvent)
        {
            throw new NotImplementedException();
        }
    }
}
