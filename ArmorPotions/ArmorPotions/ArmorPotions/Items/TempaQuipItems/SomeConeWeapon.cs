using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.Items;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.SpriteClasses;
using ArmorPotionFramework.Projectiles;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.Input;
using ArmorPotionFramework.TileEngine;

namespace ArmorPotions.Items.TempaQuipItems
{
    public class SomeConeWeapon : TempaQuip
    {
        public SomeConeWeapon(Texture2D icon, String name, AnimatedSprite sprite)
            : base(icon, name, sprite, 20)
        {
            this._allowMulti = false;
            this._hasProjectile = false;
        }

        public override void OnEquip(ArmorPotionFramework.EntityClasses.Player equippedBy)
        {
        }

        public override void OnActivate(GameTime gameTime, ArmorPotionFramework.EntityClasses.Player activatedBy)
        {
            _currentWaitTime -= gameTime.ElapsedGameTime.Milliseconds;
            if (_currentWaitTime < 0)
            {
                _currentWaitTime = _maxWaitTime;

                ConeProjectile projectile = new ConeProjectile(activatedBy.World, this, ProjectileTarget.Enemy, EventType.FireEvent, false, 75, CenterEntity(activatedBy), MathHelper.ToRadians((int)activatedBy.CurrentSprite.CurrentAnimation * 90), Math.PI / 4);
                projectile.AnimatedSprites.Add("Normal", AnimatedSprite);

                activatedBy.World.Projectiles.Add(projectile);
            }
        }

        public override void OnUnEquip(ArmorPotionFramework.EntityClasses.Player removedBy)
        {

        }
    }
}
