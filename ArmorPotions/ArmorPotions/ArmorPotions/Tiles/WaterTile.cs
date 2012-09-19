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
        Texture2D _waterTexture;
        Texture2D _deepWaterTexture;
        Texture2D _iceTexture;

        public WaterTile(TileType tileType, int tileID, Texture2D waterTexture, Texture2D iceTexture, Texture2D deepWaterTexture, bool isDeep, bool isFrozen)
            : base(tileType, tileID, waterTexture)
        {
            _isDeep = isDeep;
            _isFrozen = isFrozen;
            if (_isFrozen)
            {
                Texture = iceTexture;
                TileType = TileType.Passable;
            }
            else if (_isDeep)
            {
                Texture = deepWaterTexture;
                TileType = TileType.Hole;
            }
            else
            {
                Texture = waterTexture;
                TileType = TileType.Passable;
            }

        }

        public override void onEvent(EventType sendEvent)
        {
            if (sendEvent == EventType.IceEvent && !_isFrozen)
            {
                _isFrozen = true;
                Texture = _iceTexture;
                TileType = TileType.Passable;
            }
            else if (sendEvent == EventType.FireEvent && _isFrozen)
            {
                _isFrozen = false;
                if (_isDeep)
                {
                    Texture = _deepWaterTexture;
                    TileType = TileType.Hole;
                }
                else
                {
                    Texture = _waterTexture;
                    TileType = TileType.Passable;
                }
            }
            else
            {
                Texture = _waterTexture;
            }

            throw new NotImplementedException();
        }


    }
}
