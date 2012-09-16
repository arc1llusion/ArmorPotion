using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.CameraSystem;

namespace ArmorPotionFramework.SpriteClasses
{
    public class AnimatedSprite
    {
        #region Field Region

        private Dictionary<AnimationKey, Animation> _animations;
        private AnimationKey _currentAnimation;
        private bool _isAnimating;

        private Texture2D _texture;
        private Color _tintColor;
        private Vector2 _velocity;
        private float _speed = 2.0f;

        #endregion

        #region Property Region

        public AnimationKey CurrentAnimation
        {
            get { return _currentAnimation; }
            set { _currentAnimation = value; }
        }

        public bool IsAnimating
        {
            get { return _isAnimating; }
            set { _isAnimating = value; }
        }

        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        public int Width
        {
            get { return _animations[_currentAnimation].FrameWidth; }
        }

        public int Height
        {
            get { return _animations[_currentAnimation].FrameHeight; }
        }

        public float Speed
        {
            get { return _speed; }
            set { _speed = MathHelper.Clamp(_speed, 1.0f, 16.0f); }
        }

        public Color TintColor
        {
            get { return _tintColor; }
            set { _tintColor = value; }
        }

        public Vector2 Velocity
        {
            get { return _velocity; }
            set
            {
                _velocity = value;
                if (_velocity != Vector2.Zero)
                    _velocity.Normalize();
            }
        }

        #endregion

        #region Constructor Region

        public AnimatedSprite(Texture2D sprite, Dictionary<AnimationKey, Animation> animation)
        {
            _texture = sprite;
            _animations = new Dictionary<AnimationKey, Animation>();

            foreach (AnimationKey key in animation.Keys)
                _animations.Add(key, (Animation)animation[key].Clone());

            _tintColor = Color.White;
        }

        public AnimatedSprite(Texture2D sprite, Dictionary<AnimationKey, Animation> animation, Color spriteTint)
            : this(sprite, animation)
        {
            _tintColor = spriteTint;
        }

        #endregion

        #region Method Region

        public void Update(GameTime gameTime)
        {
            if (_isAnimating)
                _animations[_currentAnimation].Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Camera camera)
        {
            Rectangle currentRect = _animations[_currentAnimation].CurrentFrameRect;
            Rectangle tempRect = new Rectangle(
                currentRect.X + (int)camera.CameraX, 
                currentRect.Y + (int)camera.CameraY,
                currentRect.Width,
                currentRect.Height);
   
            spriteBatch.Draw(
                _texture,
                new Vector2(position.X*camera.Scale, position.Y*camera.Scale),
                currentRect,
                _tintColor,
                0,
                Vector2.Zero,
                camera.Scale,
                SpriteEffects.None,
                0);
        }

        public void Reset()
        {
            _animations[_currentAnimation].Reset();
        }

        public AnimatedSprite Clone()
        {
            Dictionary<AnimationKey, Animation> animations = new Dictionary<AnimationKey, Animation>();

            foreach (KeyValuePair<AnimationKey, Animation> animation in _animations)
            {
                animations.Add(animation.Key, (Animation)animation.Value.Clone());
            }

            AnimatedSprite sprite = new AnimatedSprite(_texture, animations);
            sprite.Velocity = _velocity;
            sprite.TintColor = _tintColor;

            return sprite;
        }

        #endregion
    }
}
