using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.Items;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.SpriteClasses;
using ArmorPotionFramework.Projectiles;

namespace ArmorPotions.Items.TempaQuipItems
{
    public class EBall : TempaQuip
    {
        private AnimatedSprite _secondaryProjectileSprite;

        public EBall(Texture2D icon, String name, AnimatedSprite sprite, int wait, AnimatedSprite secondaryProjectileSprite)
            : base(icon, name, sprite, wait)
        {
            _secondaryProjectileSprite = secondaryProjectileSprite;

            this._allowMulti = false;
            this._hasProjectile = false;
        }

        public override void OnEquip(ArmorPotionFramework.EntityClasses.Player equippedBy)
        {
        }

        public override void OnActivate(Microsoft.Xna.Framework.GameTime gameTime, ArmorPotionFramework.EntityClasses.Player activatedBy)
        {
            if (!_hasProjectile)
            {
                _currentWaitTime = _maxWaitTime;

                ThrowProjectile projectile = new ThrowProjectile(activatedBy.World, this, this.CenterEntity(activatedBy), 100, 270, 75, Math.PI / 4, 2, 1);
                projectile.AnimatedSprites.Add("Normal", AnimatedSprite);
                projectile.AnimatedSprites.Add("Projectile", _secondaryProjectileSprite);

                activatedBy.World.Projectiles.Add(projectile);

                _hasProjectile = true;
            }
        }

        public override void OnUnEquip(ArmorPotionFramework.EntityClasses.Player removedBy)
        {
        }
    }
}
