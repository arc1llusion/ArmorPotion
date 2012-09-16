using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.Items;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.Characteristics;

namespace ArmorPotions.Items
{
    public class Potion : InstaQuip
    {
        public Potion(Texture2D icon, String name)
            : base(icon, name)
        {
        }

        public override void ConsumedBy(ArmorPotionFramework.EntityClasses.Player player)
        {
            player.Health.Damage(IncrementalValue.Quarter, 1);
        }
    }
}
