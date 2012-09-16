using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.EntityClasses;
using Microsoft.Xna.Framework.Graphics;

namespace ArmorPotionFramework.Items
{
    public abstract class InstaQuip : Item
    {
        public InstaQuip(Texture2D icon, String name) : base(icon, name) { }

        public abstract void ConsumedBy(Player player);

        public override void CollectedBy(Player player)
        {
            player.Inventory.InstaQuips.Add(this);
        }
    }
}
