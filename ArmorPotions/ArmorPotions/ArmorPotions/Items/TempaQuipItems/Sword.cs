using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.Items;
using Microsoft.Xna.Framework.Graphics;

namespace ArmorPotions.Items.TempaQuipItems
{
    public class Sword : TempaQuip
    {
        public Sword(Texture2D icon, String name)
            : base(icon, name)
        {

        }

        public override void OnEquip(ArmorPotionFramework.EntityClasses.Player equippedBy)
        {
        }

        public override void OnActivate(Microsoft.Xna.Framework.GameTime gameTime, ArmorPotionFramework.EntityClasses.Player activatedBy)
        {
        }

        public override void OnUnEquip(ArmorPotionFramework.EntityClasses.Player removedBy)
        {
        }
    }
}
