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
    public class SomeConeWeapon : TempaQuip
    {
        public SomeConeWeapon(Texture2D icon, String name, AnimatedSprite sprite)
            : base(icon, name, sprite)
        {
            this._allowMulti = false;
            this._hasProjectile = false;
        }

        public override void OnEquip(ArmorPotionFramework.EntityClasses.Player equippedBy)
        {
        }

        public override void OnActivate(ArmorPotionFramework.EntityClasses.Player activatedBy)
        {
            Vector2 newPosition = new Vector2(activatedBy.Position.X - activatedBy.CurrentSprite.Width / 2, activatedBy.Position.Y - activatedBy.CurrentSprite.Height / 2);

            ConeProjectile projectile = new ConeProjectile(activatedBy.World, this, 75, newPosition, InputHandler.CurrentMousePosition, Math.PI);
            projectile.AnimatedSprites.Add("Normal", AnimatedSprite);

            activatedBy.World.Projectiles.Add(projectile);
        }

        public override void OnUnEquip(ArmorPotionFramework.EntityClasses.Player removedBy)
        {

        }
    }
}
