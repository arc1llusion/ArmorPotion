using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.SpriteClasses;
using ArmorPotionFramework.WorldClasses;
using ArmorPotionFramework.TileEngine;
using ArmorPotionFramework.Data;

namespace ArmorPotionFramework.EntityClasses
{
    public abstract class Entity
    {
        private World _world;

        protected Vector2 _position;
        protected Vector2 _velocity;

        protected Vector2 _xVelocityLimit;
        protected Vector2 _yVelocityLimit;
        private Texture2D _entityTexture;

        private Dictionary<String, AnimatedSprite> _animatedSprite;
        private String _currentSpriteKey;

        private int _leftCollisionOffset;
        private int _rightCollisionOffset;
        private int _topCollisionOffset;
        private int _bottomCollisionOffset;


        public Entity(World world)
        {
            _world = world;

            _xVelocityLimit = new Vector2(Int32.MinValue, Int32.MaxValue);
            _yVelocityLimit = new Vector2(Int32.MinValue, Int32.MaxValue);

            _animatedSprite = new Dictionary<String, AnimatedSprite>();
            _currentSpriteKey = String.Empty;

            _leftCollisionOffset = 0;
            _rightCollisionOffset = 0;
            _topCollisionOffset = 0;
            _bottomCollisionOffset = 0;
        }

        public World World
        {
            get { return this._world; }
            internal set { this._world = World; }
        }

        public Vector2 Position
        {
            get { return this._position; }
            set { this._position = value; }
        }

        public Vector2 PositionOffset
        {
            get { return (new Vector2(_position.X, _position.Y) - World.Camera.CameraOffset); }
        }

        /// <summary>
        /// Sets the collision offset from the left of the entity
        /// </summary>
        public int LeftCollisionOffset
        {
            get { return this._leftCollisionOffset; }
            set { this._leftCollisionOffset = value; }
        }

        /// <summary>
        /// Sets the collision offset from the left of the entity
        /// If there is a left collision, this offset is automatically taken into account when using the BoundingRectangle property
        /// </summary>
        public int RightCollisionOffset
        {
            get { return this._rightCollisionOffset; }
            set { this._rightCollisionOffset = value; }
        }

        /// <summary>
        /// Sets the collision offset from the top of the entity
        /// </summary>
        public int TopCollisionOffset
        {
            get { return this._topCollisionOffset; }
            set { this._topCollisionOffset = value; }
        }

        /// <summary>
        /// Sets the collision offset from the Bottom of the entity
        /// If there is a top collision, this offset is automatically taken into account when using the BoundingRectangle property
        /// </summary>
        public int BottomCollisionOffset
        {
            get { return this._bottomCollisionOffset; }
            set { this._bottomCollisionOffset = value; }
        }

        /// <summary>
        /// A property that sets both the left and right collision offset if they are the same
        /// </summary>
        public int XCollisionOffset
        {
            set { this._leftCollisionOffset = value; this._rightCollisionOffset = value; }
        }

        /// <summary>
        /// A property that sets both the top and bottom collision offset if they are the same
        /// </summary>
        public int YCollisionOffset
        {
            set { this._topCollisionOffset = value; this._bottomCollisionOffset = value; }
        }

        public Vector2 Velocity
        {
            get { return this._velocity; }
            set
            {
                this._velocity.X = MathHelper.Clamp(value.X, _xVelocityLimit.X, _xVelocityLimit.Y);
                this._velocity.Y = MathHelper.Clamp(value.Y, _yVelocityLimit.X, _xVelocityLimit.Y);
            }
        }

        public virtual Texture2D Texture
        {
            get { return this._entityTexture; }
            set { this._entityTexture = value; }
        }

        public Dictionary<String, AnimatedSprite> AnimatedSprites
        {
            get { return this._animatedSprite; }

            protected internal set { this._animatedSprite = value; }
        }

        public String CurrentSpriteKey
        {
            get
            {
                if (_currentSpriteKey == String.Empty && _animatedSprite.Count != 0)
                    _currentSpriteKey = _animatedSprite.First().Key;

                return this._currentSpriteKey;
            }
            set
            {
                if (!_animatedSprite.ContainsKey(value))
                    throw new KeyNotFoundException("The key " + value + " was not found in the Sprite Set");

                this._currentSpriteKey = value;
            }
        }

        public AnimatedSprite CurrentSprite
        {
            get { return this._animatedSprite[CurrentSpriteKey]; }
        }

        public Rectangle BoundingRectangle
        {
            get
            {
                return new Rectangle(
                    (int)Math.Ceiling(_position.X) + _leftCollisionOffset,
                    (int)Math.Ceiling(_position.Y) + _topCollisionOffset,
                    CurrentSprite.Width - (_rightCollisionOffset + _leftCollisionOffset),
                    CurrentSprite.Height - (_bottomCollisionOffset + _topCollisionOffset));
            }
        }

        public Rectangle VisualBoundingRectangle
        {
            get
            {
                Vector2 cameraOffset = World.Camera.CameraOffset;

                return new Rectangle(
                    (int)_position.X - (int)cameraOffset.X + _leftCollisionOffset,
                    (int)_position.Y - (int)cameraOffset.Y + _topCollisionOffset,
                    CurrentSprite.Width - (_rightCollisionOffset + _leftCollisionOffset),
                    CurrentSprite.Height - (_bottomCollisionOffset + _topCollisionOffset));
            }
        }

        public CollisionData HandleCollisions(Vector2 velocity)
        {
            Rectangle bounds = BoundingRectangle;

            int leftTile = (int)Math.Floor(((float)bounds.Left + velocity.X) / Tile.Width);
            int rightTile = (int)Math.Ceiling((((float)bounds.Right + velocity.X) / Tile.Width)) - 1;
            int topTile = (int)Math.Floor(((float)bounds.Top + velocity.Y) / Tile.Height);
            int bottomTile = (int)Math.Ceiling((((float)bounds.Bottom + velocity.Y) / Tile.Height)) - 1;

            Map map = World.CurrentDungeon;

            bool xAxisCollision = false;
            bool yAxisCollision = false;

            for (int y = topTile; y <= bottomTile; y++)
            {
                for (int x = leftTile; x <= rightTile; x++)
                {
                    TileInfo? tile = map.GetTile(1, x, y);
                    if (tile.HasValue && tile.Value.Tile.TileType != TileType.Passable)
                    {
                        Rectangle tileRect = tile.Value.Bounds;

                        Rectangle xAxisRectangle = new Rectangle(
                                                            bounds.X + (int)velocity.X,
                                                            bounds.Y,
                                                            bounds.Width,
                                                            bounds.Height);

                        Rectangle yAxisRectangle = new Rectangle(
                                                            bounds.X,
                                                            bounds.Y + (int)velocity.Y,
                                                            bounds.Width,
                                                            bounds.Height);

                        xAxisCollision = xAxisRectangle.Intersects(tileRect);
                        yAxisCollision = yAxisRectangle.Intersects(tileRect);
                    }
                }
            }

            return new CollisionData(xAxisCollision & yAxisCollision, xAxisCollision, yAxisCollision);
        }

        public virtual void Update(GameTime gameTime)
        {
            CurrentSprite.Update(gameTime);
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
