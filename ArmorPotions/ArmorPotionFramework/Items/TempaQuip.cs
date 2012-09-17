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
        protected int _maxWaitTime;
        protected int _currentWaitTime;

        public TempaQuip(Texture2D icon, String name) : base(icon, name) { }
        public TempaQuip(Texture2D icon, String name, AnimatedSprite sprite) : base(icon, name, sprite) { }

        public TempaQuip(Texture2D icon, String name, int maxWait)
            : base(icon, name)
        {
            _maxWaitTime = maxWait;
            _currentWaitTime = maxWait;
        }

        public TempaQuip(Texture2D icon, String name, AnimatedSprite sprite, int maxWait)
            : base(icon, name, sprite)
        {
            _maxWaitTime = maxWait;
            _currentWaitTime = maxWait;
        }

        public Vector2 CenterEntity(Entity entity)
        {
            float x = (entity.Position.X + entity.CurrentSprite.Width / 2) -(AnimatedSprite.Width / 2);
            float y = (entity.Position.Y + entity.CurrentSprite.Height / 2) - (AnimatedSprite.Height / 2);

            return new Vector2(x, y);
        }

        public abstract void OnEquip(Player equippedBy);
        public abstract void OnActivate(GameTime gameTime, Player activatedBy);
        public abstract void OnUnEquip(Player removedBy);

        public override void CollectedBy(Player player)
        {
            player.Inventory.TempaQuips.Add(this);
        }
    }
}
