using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.Game;
using ArmorPotionFramework.SpriteClasses;
using System.Runtime.CompilerServices;
using ArmorPotionFramework.WorldClasses;

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

        private int _xCollisionOffset;
        private int _yCollisionOffset;

        public Entity(World world)
        {
            _world = world;

            _xVelocityLimit = new Vector2(Int32.MinValue, Int32.MaxValue);
            _yVelocityLimit = new Vector2(Int32.MinValue, Int32.MaxValue);

            _animatedSprite = new Dictionary<String, AnimatedSprite>();
            _currentSpriteKey = String.Empty;

            _xCollisionOffset = 0;
            _yCollisionOffset = 0;
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

        public int XCollisionOffset
        {
            get { return this._xCollisionOffset; }
            set { this._xCollisionOffset = value; }
        }

        public int YCollisionOffset
        {
            get { return this._yCollisionOffset; }
            set { this._yCollisionOffset = value; }
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
                    (int)Math.Ceiling(_position.X) + _xCollisionOffset,
                    (int)Math.Ceiling(_position.Y) + _yCollisionOffset,
                    CurrentSprite.Width - _xCollisionOffset,
                    CurrentSprite.Height - _yCollisionOffset);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            CurrentSprite.Update(gameTime);
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
