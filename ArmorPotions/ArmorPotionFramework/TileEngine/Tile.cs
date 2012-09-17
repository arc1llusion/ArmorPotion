using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.SpriteClasses;

namespace ArmorPotionFramework.TileEngine
{
    public abstract class Tile
    {
        String tileName = "";
        Vector2 _location;
        Vector2 _tileSize;
        Texture2D _texture;
        Animation standingAnimation;
        TileType _tileType;
        public bool _isHidden;
        Rectangle _tileRect;

        public Tile(TileType type, Texture2D texture)
        {
            _tileType = type;
            _texture = texture;
        }
        public Tile(Animation animation)
        {
        }
        public abstract void onEvent(EventType sendEvent);
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }
        public Vector2 Position
        {
            get { return _location; }
            set { _location = value; }
        }
    }
}
