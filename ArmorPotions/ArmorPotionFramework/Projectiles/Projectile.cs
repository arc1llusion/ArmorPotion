using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.SpriteClasses;
using ArmorPotionFramework.EntityClasses;
using ArmorPotionFramework.WorldClasses;

namespace ArmorPotionFramework.Projectiles
{
    public abstract class Projectile : Entity
    {
        protected bool _isAlive;

        public Projectile(World world)
            : base(world)
        {
            _isAlive = true;
        }

        public bool IsAlive
        {
            get { return this._isAlive; }
            protected set { this._isAlive = value; }
        }
    }
}
