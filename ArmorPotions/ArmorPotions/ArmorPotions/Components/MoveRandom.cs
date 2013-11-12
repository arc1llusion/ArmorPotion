using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.EntityClasses.Components;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.EntityClasses;
using ArmorPotionFramework.Utility;
using ArmorPotionFramework.SpriteClasses;

namespace ArmorPotions.Components
{
    public class MoveRandom : IAIComponent
    {
        private float randomWalkDistance = 50;
        private float currentWalkDistance = 0;
        private Vector2 velocity = Vector2.Zero;

        public void Update(GameTime gameTime, Enemy enemy)
        {
            if (currentWalkDistance <= 0)
            {
                int r = RandomGenerator.Random.Next(0, 4);
                currentWalkDistance = randomWalkDistance;

                Vector2 velocityAbsolute = new Vector2(Math.Abs(enemy.Velocity.X), Math.Abs(enemy.Velocity.Y));

                switch (r)
                {
                    case 0:
                        velocity = new Vector2(velocityAbsolute.X, 0);
                        enemy.CurrentSprite.CurrentAnimation = AnimationKey.Right;
                        break;
                    case 1:
                        velocity = new Vector2(-velocityAbsolute.X, 0);
                        enemy.CurrentSprite.CurrentAnimation = AnimationKey.Left;
                        break;
                    case 2:
                        velocity = new Vector2(0, velocityAbsolute.Y);
                        enemy.CurrentSprite.CurrentAnimation = AnimationKey.Down;
                        break;
                    case 3:
                        velocity = new Vector2(0, -velocityAbsolute.Y);
                        enemy.CurrentSprite.CurrentAnimation = AnimationKey.Up;
                        break;
                }
            }
            else
            {
                if (!enemy.HandleCollisions(velocity).IsColliding)
                {
                    enemy.Position = enemy.Position + velocity;
                    currentWalkDistance -= Math.Abs((velocity.X + velocity.Y) / 2);
                }
                else
                {
                    currentWalkDistance = 0;
                }

                if (enemy.HasPlayerInSight)
                    enemy.ActionComplete();
            }
        }

        public void SetUp(Enemy enemy)
        {
        }


        public IAIComponent Clone()
        {
            return new MoveRandom();
        }
    }
}
