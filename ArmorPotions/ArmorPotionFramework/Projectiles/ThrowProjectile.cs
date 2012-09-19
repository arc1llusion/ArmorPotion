﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.WorldClasses;
using ArmorPotionFramework.Items;
using ArmorPotionFramework.Utility;

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

        #endregion

        public ThrowProjectile(World world, Item source, Vector2 startingPostion, float throwDistance, float beginningAngle, float projectileDistance, double projectileSpread, float revolutions, float projectilesPerIteration)
            : base(world, source)
        {
            _position = startingPostion;

            _startingPosition = startingPostion;
            _throwDistance = throwDistance;
            _beginningAngle = MathHelper.ToRadians(beginningAngle);
            _angle = _beginningAngle;
            _projectileDistance = projectileDistance;
            _projectileSpread = projectileSpread;
            _angleCycle = revolutions * 2 * Math.PI;
            _deltaAngle = projectilesPerIteration / _angleCycle;

            _thrown = false;
            Velocity = new Vector2((float)Math.Cos(beginningAngle) * 2, (float)Math.Sin(beginningAngle) * 2);
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
                    _angle += _deltaAngle;
                    ConeProjectile projectile = new ConeProjectile(World, null, _projectileDistance, _position, _angle, _projectileSpread);
                    projectile.AnimatedSprites.Add("Normal", AnimatedSprites["Projectile"]);

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
    }
}