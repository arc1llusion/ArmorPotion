﻿using System;
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
            : base(icon, name, sprite)
        {
        }

        public override void OnEquip(Player equippedBy)
        {
        }

        public override void OnActivate(Player activatedBy)
        {
            Vector2 newPosition = new Vector2(activatedBy.Position.X - activatedBy.CurrentSprite.Width / 2, activatedBy.Position.Y - activatedBy.CurrentSprite.Height / 2);

            _projectile = new LinearProjectile(activatedBy.World, 5, newPosition, InputHandler.CurrentMousePosition);
            _projectile.AnimatedSprites.Add("Normal", AnimatedSprite);

            activatedBy.World.Projectiles.Add(_projectile);
        }

        public override void OnUnEquip(Player removedBy)
        {
        }
    }
}
