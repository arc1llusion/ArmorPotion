using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.EntityClasses.Components;
using ArmorPotionFramework.Projectiles;
using ArmorPotionFramework.SpriteClasses;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ArmorPotions.Components
{
    public class AoEDrop : IAIComponent
    {
        private int _lifetime;
        private AreaOfEffectProjectile _projectile;
        private bool _isSetUp = false;

        public AoEDrop()
        {
            _lifetime = 3000;
        }

        public void SetUp(ArmorPotionFramework.EntityClasses.Enemy enemy)
        {
            if (!_isSetUp)
            {
                Vector2 newPosition = new Vector2(enemy.Position.X - enemy.CurrentSprite.Width / 2, enemy.Position.Y - enemy.CurrentSprite.Height / 2);

                Animation animation = new Animation(1, 256, 256, 0, 0);
                AnimatedSprite sprite = new AnimatedSprite(enemy.World.Game.Content.Load<Texture2D>(@"Images\Enemy\LightBugAttack"), new Dictionary<AnimationKey, Animation> { { AnimationKey.Down, animation } });
                _projectile = new AreaOfEffectProjectile(enemy.World, null, newPosition, 3000);
                _projectile.AnimatedSprites.Add("Normal", sprite);
                enemy.World.Projectiles.Add(_projectile);
                _lifetime = 3000;
                _isSetUp = true;
            }
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime, ArmorPotionFramework.EntityClasses.Enemy enemy)
        {
            _lifetime -= gameTime.ElapsedGameTime.Milliseconds;
            if (_lifetime < 0)
            {
                _isSetUp = false;
                enemy.ActionComplete();
            }
        }
    }
}
