using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.EntityClasses.Components;
using ArmorPotionFramework.Projectiles;
using ArmorPotionFramework.SpriteClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.EntityClasses;

namespace ArmorPotions.Components
{
    public class AoEDrop : IAIComponent
    {
        private int _lifetime;
        private int _waitTime;
        private AreaOfEffectProjectile _projectile;
        private bool _isSetUp = false;

        private int _defaultLifetime;
        private int _defaultWaitTime;

        private Texture2D _texture;

        public AoEDrop()
        {
            _defaultLifetime = 3000;
            _defaultWaitTime = 1000;
        }

        public void SetUp(ArmorPotionFramework.EntityClasses.Enemy enemy)
        {
            _lifetime = _defaultLifetime;
            _waitTime = _defaultWaitTime;
        }

        public int LifeTime
        {
            set { this._defaultLifetime = value; }
        }

        public int Wait
        {
            set { this._defaultWaitTime = value; }
        }

        public Texture2D AttackTexture
        {
            set { this._texture = value; }
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime, ArmorPotionFramework.EntityClasses.Enemy enemy)
        {
            bool canAttack = WaitTimer(gameTime);
            if (canAttack && !_isSetUp)
            {
                Vector2 newPosition = new Vector2(enemy.Position.X - enemy.CurrentSprite.Width / 2, enemy.Position.Y - enemy.CurrentSprite.Height / 2);

                Animation animation = new Animation(1, 256, 256, 0, 0);
                AnimatedSprite sprite = new AnimatedSprite(_texture, new Dictionary<AnimationKey, Animation> { { AnimationKey.Down, animation } });
                _projectile = new AreaOfEffectProjectile(enemy.World, null, newPosition, _defaultLifetime);
                _projectile.AnimatedSprites.Add("Normal", sprite);
                enemy.World.Projectiles.Add(_projectile);
                _lifetime = _defaultLifetime;
                _isSetUp = true;
            }
            else if(canAttack && _isSetUp)
            {
                _lifetime -= gameTime.ElapsedGameTime.Milliseconds;
                if (_lifetime < 0)
                {
                    _isSetUp = false;
                    enemy.ActionComplete();
                }
            }
        }

        private bool WaitTimer(GameTime gameTime)
        {
            if (_waitTime < 0)
                return true;

            _waitTime -= gameTime.ElapsedGameTime.Milliseconds;
            return false;
        }
    }
}
