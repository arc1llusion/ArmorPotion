using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.SpriteClasses;
using System;

namespace ArmorPotionFramework.TileEngine
{
    public struct TileInfo
    {
        public Rectangle Bounds;
        public Tile Tile;

        public TileInfo(Rectangle bounds, Tile tile)
        {
            Bounds = bounds;
            Tile = tile;
        }
    }

    public abstract class Tile
    {
        #region Static fields

        public static int Width = 128;
        public static int Height = 128;

        #endregion

        #region private/protected fields

        private int _tileID;
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

        public int TileID
        {
            get { return _tileID; }
            set { _tileID = value; }
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

        public Tile(TileType type, int tileID, Texture2D texture)
        {
            _tileType = type;
            _texture = texture;
            _tileName = String.Empty;
            _tileID = tileID;
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
