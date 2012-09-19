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
using ArmorPotionFramework.TileEngine;

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

        private AnimatedSprite _sprite;
        private EventType _eventType;

        public AoEDrop()
        {
            _defaultLifetime = 3000;
            _defaultWaitTime = 1000;
        }

        public void SetUp(ArmorPotionFramework.EntityClasses.Enemy enemy)
        {
            _lifetime = _defaultLifetime;
            _waitTime = _defaultWaitTime;
            AnimationKey currentAnimation = enemy.CurrentSprite.CurrentAnimation;
            enemy.CurrentSpriteKey = "Charging";
            enemy.CurrentSprite.IsAnimating = true;
            enemy.CurrentSprite.CurrentAnimation = currentAnimation;
        }

        public int LifeTime
        {
            set { this._defaultLifetime = value; }
        }

        public int Wait
        {
            set { this._defaultWaitTime = value; }
        }

        public AnimatedSprite AttackTexture
        {
            set { this._sprite = value; }
        }

        public EventType EventType
        {
            set { this._eventType = value; }
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime, ArmorPotionFramework.EntityClasses.Enemy enemy)
        {
            bool canAttack = WaitTimer(gameTime);
            if (canAttack && !_isSetUp)
            {
                Vector2 newPosition = new Vector2(enemy.Position.X - enemy.CurrentSprite.Width / 2, enemy.Position.Y - enemy.CurrentSprite.Height / 2);

                Animation animation = new Animation(1, 256, 256, 0, 0);
                _projectile = new AreaOfEffectProjectile(enemy.World, null, EventType.LightningEvent, false, newPosition, _defaultLifetime);
                _projectile.AnimatedSprites.Add("Normal", _sprite.Clone());
                enemy.World.Projectiles.Add(_projectile);
                _lifetime = _defaultLifetime;
                _isSetUp = true;
                enemy.CurrentSpriteKey = "Walking";
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
