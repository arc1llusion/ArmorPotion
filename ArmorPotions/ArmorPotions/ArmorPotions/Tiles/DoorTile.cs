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
        bool _isOpened = false;
        Texture2D _doorTexture;
        Texture2D _doorOpenTexture;
        public DoorTile(TileType tileType, int tileID, Texture2D doorTexture, Texture2D doorOpenTexture)
            : base(tileType, tileID, doorTexture)
        {
            _doorTexture = doorTexture;
            _doorOpenTexture = doorOpenTexture;
        }




        public override void onEvent(EventType sendEvent)
        {
            if (sendEvent == EventType.DoorTrigger)
            {
                
                if (_isOpened)
                {
                    _isOpened = false;
                    Texture = _doorTexture;
                    TileType = TileType.NonPassable;
                }
                else
                {
                    _isOpened = true;
                    Texture = _doorOpenTexture;
                    TileType = TileType.Passable;
                }
            }
        }
    }
}
