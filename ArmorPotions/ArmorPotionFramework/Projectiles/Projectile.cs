using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.SpriteClasses;
using ArmorPotionFramework.EntityClasses;
using ArmorPotionFramework.WorldClasses;
using ArmorPotionFramework.Items;
using ArmorPotionFramework.Data;
using ArmorPotionFramework.TileEngine;

namespace ArmorPotionFramework.Projectiles
{
    public abstract class Projectile : Entity
    {
        protected bool _isAlive;
        protected Item _source;
        protected EventType _eventType;
        protected bool _triggerEvents;

        public Projectile(World world, Item source, EventType eventType, bool triggerEvents)
            : base(world)
        {
            _isAlive = true;
            _source = source;
            _eventType = eventType;
            _triggerEvents = triggerEvents;
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

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            CollisionData collisionData = this.HandleCollisions(this._velocity);
            if (collisionData.IsColliding)
            {
                OnCollide(collisionData.TileData);
                if (_triggerEvents)
                {
                    foreach (Tile tile in collisionData.TileData)
                    {
                        if (tile.TileID == 11 || tile.TileID == 12 || tile.TileID == 13)
                            tile.onEvent(_eventType);
                    }
                }
            }
        }

        public abstract void OnCollide(List<Tile> tileData);
    }
}
