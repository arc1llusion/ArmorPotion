using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.SpriteClasses;

namespace ArmorPotionFramework.TileEngine
{
    public struct TileInfo
    {
        public Rectangle Bounds;
        public TileType TileType;

        public TileInfo(Rectangle bounds, TileType type)
        {
            Bounds = bounds;
            TileType = type;
        }
    }

    public abstract class Tile
    {
        #region Static fields

        public static int Width = 192;
        public static int Height = 192;

        #endregion

        #region private/protected fields

        private String _tileName;
        private Vector2 _location;
        private Vector2 _tileSize;
        private Texture2D _texture;
        private Animation standingAnimation;
        private TileType _tileType;
        protected bool _isHidden;
        private Rectangle _tileRect;

        #endregion

        #region Properties

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

        public TileType TileType
        {
            get { return _tileType; }
            set { this._tileType = value; }
        }

        #endregion

        #region Constructors

        public Tile(TileType type, Texture2D texture)
        {
            _tileType = type;
            _texture = texture;
            _tileName = String.Empty;
        }
        public Tile(Animation animation)
        {
        }

        #endregion

        #region Abstract Methods 

        public abstract void onEvent(EventType sendEvent);

        #endregion
    }
}
