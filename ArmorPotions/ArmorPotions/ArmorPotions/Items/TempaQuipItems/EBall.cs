using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.Items;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.SpriteClasses;
using ArmorPotionFramework.Projectiles;
using Microsoft.Xna.Framework;

namespace ArmorPotions.Items.TempaQuipItems
{
    public class EBall : TempaQuip
    {
        private AnimatedSprite _secondaryProjectileSprite;
        private float _throwDistance;
        private float _spreadAngle;
        private float _revolutions;
        private float _projectilesPerIteration;
        private float _projectileDistance;

        public EBall(Texture2D icon, String name, AnimatedSprite sprite, int wait, AnimatedSprite secondaryProjectileSprite, float throwDistance, float projectileDistance, int spreadAngle, float revolutions, float projectilesPerIteration)
            : base(icon, name, sprite, wait)
        {
            _secondaryProjectileSprite = secondaryProjectileSprite;

            this._allowMulti = false;
            this._hasProjectile = false;

            _throwDistance = throwDistance;
            _projectileDistance = projectileDistance;
            _spreadAngle = MathHelper.ToRadians(spreadAngle);
            _revolutions = revolutions;
            _projectilesPerIteration = projectilesPerIteration;
        }

        public override void OnEquip(ArmorPotionFramework.EntityClasses.Player equippedBy)
        {
        }

        public override void OnActivate(Microsoft.Xna.Framework.GameTime gameTime, ArmorPotionFramework.EntityClasses.Player activatedBy)
        {
            if (!_hasProjectile)
            {
                _currentWaitTime = _maxWaitTime;

                ThrowProjectile projectile = new ThrowProjectile(
                    activatedBy.World, 
                    this, this.CenterEntity(activatedBy), 
                    _throwDistance, 
                    MathHelper.ToRadians((int)activatedBy.CurrentSprite.CurrentAnimation * 90), 
                    _projectileDistance, 
                    _spreadAngle, 
                    _revolutions, 
                    _projectilesPerIteration);

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
