using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.Items;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.EntityClasses;
using ArmorPotionFramework.Projectiles;
using ArmorPotionFramework.Input;
using ArmorPotionFramework.SpriteClasses;
using Microsoft.Xna.Framework;

namespace ArmorPotions.Items.TempaQuipItems
{
    public class Gun : TempaQuip
    {
        LinearProjectile _projectile;

        public Gun(Texture2D icon, String name, AnimatedSprite sprite)
            : base(icon, name, sprite, 400)
        {
            this._allowMulti = true;
            this._hasProjectile = false;
        }

        public override void OnEquip(Player equippedBy)
        {
        }

        public override void OnActivate(GameTime gameTime, Player activatedBy)
        {
            _currentWaitTime -= gameTime.ElapsedGameTime.Milliseconds;
            if (_currentWaitTime < 0)
            {
                _currentWaitTime = _maxWaitTime;

                _projectile = new LinearProjectile(activatedBy.World, this, 75, CenterEntity(activatedBy), MathHelper.ToRadians((int)activatedBy.CurrentSprite.CurrentAnimation * 90));
                _projectile.AnimatedSprites.Add("Normal", AnimatedSprite);

                activatedBy.World.Projectiles.Add(_projectile);
            }
        }

        public override void OnUnEquip(Player removedBy)
        {
        }
    }
}
