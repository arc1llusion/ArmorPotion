using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.WorldClasses;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.Utility;
using ArmorPotionFramework.Items;

namespace ArmorPotionFramework.Projectiles
{
    public class LinearProjectile : Projectile
    {
        protected float _liveDistance;
        protected Vector2 _startingPosition;
        protected Vector2 _destination;
        protected double _angle;

        public LinearProjectile(World world, Item source, float liveDistance, Vector2 startingPosition, Vector2 destination)
            : base(world, source)
        {
            _liveDistance = liveDistance;
            _startingPosition = startingPosition;
            _destination = destination;
            _angle = GameMath.CalculateAngle(startingPosition, destination);

            Position = startingPosition;
            Velocity = new Vector2((float)Math.Cos(_angle) * 2, (float)Math.Sin(_angle) * 2);
        }

        public LinearProjectile(World world, Item source, float liveDistance, Vector2 startingPosition, double angle)
            : base(world, source)
        {
            _liveDistance = liveDistance;
            _startingPosition = startingPosition;
            _angle = angle;

            Position = startingPosition;
            Velocity = new Vector2((float)Math.Cos(_angle) * 2, (float)Math.Sin(_angle) * 2);
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
 
            double distance = GameMath.Distance(_startingPosition, _position);
            if (distance  > _liveDistance)
                this._isAlive = false;
        }

        public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            CurrentSprite.Draw(gameTime, spriteBatch, _position - World.Camera.CameraOffset, World.Camera);
        }
    }
}
