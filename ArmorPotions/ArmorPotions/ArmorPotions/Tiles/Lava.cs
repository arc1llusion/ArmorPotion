using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.TileEngine;

namespace ArmorPotions.Tiles
{
    class Lava : Tile
    {
        public Lava()
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
