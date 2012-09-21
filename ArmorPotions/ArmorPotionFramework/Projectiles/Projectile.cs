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
using ArmorPotionFramework.Utility;

namespace ArmorPotionFramework.Projectiles
{
    public enum ProjectileTarget { Player, Enemy };
    public abstract class Projectile : Entity
    {
        protected Item _source;
        protected EventType _eventType;
        protected bool _triggerEvents;
        protected ProjectileTarget _target;
        protected ushort _damageAmount;

        private bool _damagedOnce;

        public Projectile(World world, Item source, ProjectileTarget target, EventType eventType, bool triggerEvents)
            : base(world)
        {
            _source = source;
            _eventType = eventType;
            _triggerEvents = triggerEvents;
            _damagedOnce = false;
            _target = target;
            _damageAmount = 5;
        }

        public Item Source
        {
            get { return this._source; }
        }

        public ushort DamageAmount
        {
            get { return this._damageAmount; }
            set { this._damageAmount = value; }
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);

            /* Tile Collision */
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

            if (!_damagedOnce)
            {
                /* Enemy Collision */
                if (this._target == ProjectileTarget.Enemy)
                {
                    IEnumerable<Enemy> enemies = World.CurrentDungeon.Enemies.Where(enemy => GameMath.Distance(_position, enemy.Position) < 200);
                    foreach (Enemy enemy in enemies)
                    {
                        if (enemy.BoundingRectangle.Intersects(this.BoundingRectangle))
                        {
                            enemy.Health.Damage(_damageAmount);
                            _damagedOnce = true;
                        }
                    }
                }
                else if (this._target == ProjectileTarget.Player)
                {
                    if (World.Player.BoundingRectangle.Intersects(this.BoundingRectangle))
                    {
                        World.Player.Damage(_damageAmount);
                        _damagedOnce = true;
                    }
                }
            }
        }

        public abstract void OnCollide(List<Tile> tileData);
    }
}
