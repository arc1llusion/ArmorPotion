using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.WorldClasses;
using ArmorPotionFramework.Items;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.Utility;

namespace ArmorPotionFramework.Projectiles
{
    public class ConeProjectile : LinearProjectile
    {

        public ConeProjectile(World world, Item source, float liveDistance, Vector2 startingPosition, Vector2 destination, double angleSpread)
            : base(world, source, liveDistance, startingPosition, destination)
        {
            this._destination = destination;

            double spread = RandomGenerator.Random.NextDouble() * (angleSpread);
            Velocity = new Vector2((float)Math.Cos(_angle ) * 2 + (float)spread, (float)Math.Sin(_angle) * 2 + (float)spread);
        }

        public ConeProjectile(World world, Item source, float liveDistance, Vector2 startingPosition, double angle, double angleSpread)
            : base(world, source, liveDistance, startingPosition, angle)
        {

            double spread = RandomGenerator.Random.NextDouble() * (angleSpread);
            Velocity = new Vector2((float)Math.Cos(_angle) * 2 + (float)spread, (float)Math.Sin(_angle) * 2 + (float)spread);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}