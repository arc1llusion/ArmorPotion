using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.WorldClasses;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.Items;

namespace ArmorPotionFramework.Projectiles
{
    public class AreaOfEffectProjectile : Projectile
    {
        private Vector2 _destination;
        private int _lifetime;

        public AreaOfEffectProjectile(World world, Item source, Vector2 destination, int lifetime)
            : base(world, source)
        {
            _position = destination;
            _destination = destination;
            _lifetime = lifetime;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _lifetime -= gameTime.ElapsedGameTime.Milliseconds;
            if (_lifetime < 0)
                _isAlive = false;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            CurrentSprite.Draw(gameTime, spriteBatch, _destination - World.Camera.CameraOffset, World.Camera);
        }
    }
}
