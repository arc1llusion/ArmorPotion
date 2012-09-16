using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.SpriteClasses;
using ArmorPotionFramework.EntityClasses;
using ArmorPotionFramework.WorldClasses;
using ArmorPotionFramework.Items;

namespace ArmorPotionFramework.Projectiles
{
    public abstract class Projectile : Entity
    {
        protected bool _isAlive;
        protected Item _source;

        public Projectile(World world, Item source)
            : base(world)
        {
            _isAlive = true;
            _source = source;
        }

        public bool IsAlive
        {
            get { return this._isAlive; }
            protected set { this._isAlive = value; }
        }

        public Item Source
        {
            get { return this._source; }
        }
    }
}
