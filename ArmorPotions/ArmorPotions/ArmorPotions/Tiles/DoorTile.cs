using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.TileEngine;
using Microsoft.Xna.Framework.Graphics;

namespace ArmorPotions.Tiles
{
    class DoorTile : Tile
    {
        public DoorTile(TileType tileType, Texture2D texture) : base(null)
        {

        }
        bool _isOpened;




        public override void onEvent(EventType sendEvent)
        {
            if (sendEvent == EventType.DoorTrigger)
            {
                _isOpened = !_isOpened;
            }
            throw new NotImplementedException();
        }
    }
}
