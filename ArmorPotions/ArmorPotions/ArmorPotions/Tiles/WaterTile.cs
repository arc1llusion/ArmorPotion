using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.TileEngine;
using Microsoft.Xna.Framework.Graphics;

namespace ArmorPotions.Tiles
{
    class WaterTile : Tile
    {
        bool _isDeep;
        bool _isFrozen;

        public WaterTile(TileType tileType, Texture2D texture) : base(null)
        {
        }

        public override void onEvent(EventType sendEvent)
        {
            if (sendEvent == EventType.IceEvent && !_isFrozen)
            {
                _isFrozen = true;
            }else if(sendEvent == EventType.FireEvent && _isFrozen){
                _isFrozen = false;
            }

            throw new NotImplementedException();
        }

        public void Draw(){
        }


    }
}
