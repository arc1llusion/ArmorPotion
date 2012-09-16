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
        Vector2 _renderLocation;
        Vector2 _tileSize;
        Texture2D _texture;
        Animation standingAnimation;
        TileType _tileType;
        public bool _isHidden;
        Rectangle _tileRect;

        public Tile(TileType type, Texture2D texture, Vector2 location, Vector2 size)
        {
            _tileType = type;
            _texture = texture;
            _tileSize = size;
            _renderLocation = location; 
        }
        public Tile(Animation animation)
        {
        }

        public abstract void onEvent(EventType sendEvent);

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(_texture, _renderLocation, null, Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 1);

        }


    }
}
