using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.WorldClasses;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.Utility;

namespace ArmorPotionFramework.Projectiles
{
    public class LinearProjectile : Projectile
    {
        private float _liveDistance;
        private Vector2 _startingPosition;
        private Vector2 _destination;
        private double _angle;

        public LinearProjectile(World world, float liveDistance, Vector2 startingPosition, Vector2 destination)
            : base(world)
        {
            _liveDistance = liveDistance;
            _startingPosition = startingPosition;
            _destination = destination;
            _angle = GameMath.CalculateAngle(startingPosition, destination);

            Position = startingPosition;
            Velocity = new Vector2((float)Math.Cos(_angle), (float)Math.Sin(_angle));
        }

        public float LiveDistance
        {
            get { return this._liveDistance; }
        }

        public Vector2 Destination
        {
            get { return this._destination; }
        }

        public Vector2 StartingPosition
        {
            get { return this._startingPosition; }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _position.X += _velocity.X;
            _position.Y += _velocity.Y;

            if (_position.X + _position.Y > _liveDistance)
                this._isAlive = false;
        }

        public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            CurrentSprite.Draw(gameTime, spriteBatch, _position, World.Camera);
        }
    }
}
