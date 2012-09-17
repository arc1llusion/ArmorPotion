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
            equippedBy.Health.AddToMax(ArmorPotionFramework.Characteristics.IncrementalValue.Full, 2);
            equippedBy.Health.Heal(ArmorPotionFramework.Characteristics.IncrementalValue.Full, 2);
        }

        public override void OnActivate(GameTime gameTime, ArmorPotionFramework.EntityClasses.Player activatedBy)
        {
            if (!_hasProjectile)
            {
                Vector2 newPosition = new Vector2(activatedBy.Position.X - activatedBy.CurrentSprite.Width / 2, activatedBy.Position.Y - activatedBy.CurrentSprite.Height / 2);

                AreaOfEffectProjectile projectile = new AreaOfEffectProjectile(activatedBy.World, this, newPosition, 3000);
                projectile.AnimatedSprites.Add("Normal", AnimatedSprite);

                activatedBy.World.Projectiles.Add(projectile);

                _hasProjectile = true;
            }
        }

        public override void OnUnEquip(ArmorPotionFramework.EntityClasses.Player removedBy)
        {
            removedBy.Health.RemoveFromMax(ArmorPotionFramework.Characteristics.IncrementalValue.Full, 2);
        }
    }
}
