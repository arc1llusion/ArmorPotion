using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.TileEngine;

namespace ArmorPotions.Tiles
{
    class WaterTile : Tile
    {
        bool _isDeep;
        bool _isFrozen;

        public WaterTile() : base(null)
        {
        }

        public override void onEvent(EventType sendEvent)
        {
            throw new NotImplementedException();
        }


    }
}
