using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.EntityClasses;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.SpriteClasses;
using Microsoft.Xna.Framework;

namespace ArmorPotionFramework.Items
{
    public abstract class TempaQuip : Item
    {
        public TempaQuip(Texture2D icon, String name) : base(icon, name) { }
        public TempaQuip(Texture2D icon, String name, AnimatedSprite sprite) : base(icon, name, sprite) { }

        public abstract void OnEquip(Player equippedBy);
        public abstract void OnActivate(GameTime gameTime, Player activatedBy);
        public abstract void OnUnEquip(Player removedBy);

        public override void CollectedBy(Player player)
        {
            player.Inventory.TempaQuips.Add(this);
        }
    }
}
