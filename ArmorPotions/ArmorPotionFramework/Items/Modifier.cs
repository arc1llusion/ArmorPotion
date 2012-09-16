using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArmorPotionFramework.Items
{
    public enum StatType
    {
        Health,
        Shield,

        Attack,
        Defense,
        Speed
    }

    public struct Modifier
    {
        public StatType ModifiedStat;
        public float Amount;
    }
}
