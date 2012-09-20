using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.WorldClasses;
using ArmorPotionFramework.Items;
using ArmorPotionFramework.Utility;
using ArmorPotionFramework.TileEngine;

namespace ArmorPotionFramework.Projectiles
{
    public class ThrowProjectile : Projectile
    {
        #region Throw projectile specific fields

        private readonly double _angleCycle;
        private readonly double _deltaAngle;

        private Vector2 _startingPosition;
        private float _throwDistance;
        private float _beginningAngle;
        private double _angle;
        private bool _thrown;

        #endregion

        #region Throw projectile spawned projectile fields

        private double _projectileSpread;
        private float _projectileDistance;
        private bool _triggerSecondaryProjectileEvents;

        #endregion

        public ThrowProjectile(World world, Item source, ProjectileTarget target, EventType eventType, bool triggerEvents, Vector2 startingPostion, float throwDistance, float beginningAngle, float projectileDistance, double projectileSpread, float revolutions, float projectilesPerIteration, bool triggerSecondaryProjectileEvents)
            : base(world, source, target, eventType, triggerEvents)
        {
            _position = startingPostion;

            _startingPosition = startingPostion;
            _throwDistance = throwDistance;
            _beginningAngle = MathHelper.ToRadians(beginningAngle);
            _angle = _beginningAngle;
            _projectileDistance = projectileDistance;
            _projectileSpread = projectileSpread;
            _triggerSecondaryProjectileEvents = triggerSecondaryProjectileEvents;
            _angleCycle = revolutions * 2 * Math.PI;
            _deltaAngle = projectilesPerIteration / _angleCycle;

            _thrown = false;
            Velocity = new Vector2((float)Math.Cos(beginningAngle) * 2, (float)Math.Sin(beginningAngle) * 2);
            _triggerEvents = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (!_thrown)
            {
                double distance = GameMath.Distance(_startingPosition, _position);
                _position += Velocity;
                if (distance > _throwDistance)
                    _thrown = true;
            }
            else
            {
                if (_angle < _beginningAngle + _angleCycle)
                {
                    double newAngle = _angle += _deltaAngle;

                    if(_eventType == EventType.IceEvent)
                        newAngle = RandomGenerator.Random.NextDouble() * Math.PI * 2;

                    ConeProjectile projectile = new ConeProjectile(World, null, this._target, _eventType, _triggerSecondaryProjectileEvents, _projectileDistance, _position, newAngle, _projectileSpread);
                    projectile.DamageAmount = this.DamageAmount;

                    projectile.AnimatedSprites.Add("Normal", AnimatedSprites["Projectile"].Clone());

                    World.ProjectilesToAdd.Add(projectile);
                }
                else
                {
                    this._isAlive = false;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentSprite.Draw(gameTime, spriteBatch, _position - World.Camera.CameraOffset, World.Camera);
        }

        public override void OnCollide(List<TileEngine.Tile> tileData)
        {
            _thrown = true;
        }
    }
}
