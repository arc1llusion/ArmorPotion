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
        bool _isCooled;
        Texture2D _lavaTexture;
        Texture2D _cooledLavaTexture;
        public LavaTile(TileType tileType, int tileID, Texture2D lavaTexture, Texture2D cooledLavaTexture, bool isCooled)
            : base(tileType, tileID, lavaTexture)
        {
            _isCooled = isCooled;
            _lavaTexture = lavaTexture;
            _cooledLavaTexture = cooledLavaTexture;
            if (_isCooled)
            {
                Texture = _cooledLavaTexture;
                TileType = TileType.Passable;
            }
            else{
                Texture = _lavaTexture;
                TileType = TileType.Hole;
            }
        }

        public override void onEvent(EventType sendEvent)
        {

            if(sendEvent == EventType.IceEvent && !_isCooled){
                _isCooled = true;
                Texture = _cooledLavaTexture;
                TileType = TileType.Passable;
            }else if(sendEvent == EventType.FireEvent && _isCooled){
                _isCooled = false;
                Texture = _lavaTexture;
                TileType = TileType.Hole;
            }
            throw new NotImplementedException();
        }
    }
}
