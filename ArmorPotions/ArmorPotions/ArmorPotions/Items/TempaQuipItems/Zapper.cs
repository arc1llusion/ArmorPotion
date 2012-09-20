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

namespace ArmorPotions.Items.TempaQuipItems
{
    public class Zapper : TempaQuip
    {
        public Zapper(Texture2D icon, String name, AnimatedSprite sprite)
            : base(icon, name, sprite)
        {
            this._allowMulti = false;
            this._hasProjectile = false;
        }

        public override void OnEquip(ArmorPotionFramework.EntityClasses.Player equippedBy)
        {
            equippedBy.Health.ChangeMaxRelative(200);
        }

        public override void OnActivate(GameTime gameTime, ArmorPotionFramework.EntityClasses.Player activatedBy)
        {
            if (!_hasProjectile)
            {
                AreaOfEffectProjectile projectile = new AreaOfEffectProjectile(activatedBy.World, this, ProjectileTarget.Enemy, ArmorPotionFramework.TileEngine.EventType.LightningEvent, false, CenterEntity(activatedBy), 3000);
                projectile.AnimatedSprites.Add("Normal", AnimatedSprite);

                activatedBy.World.Projectiles.Add(projectile);

                _hasProjectile = true;
            }
        }

        public override void OnUnEquip(ArmorPotionFramework.EntityClasses.Player removedBy)
        {
            removedBy.Health.ChangeMaxRelative(-200);
        }
    }
}
