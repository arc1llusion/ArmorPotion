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

        public void Update(GameTime gameTime, Enemy enemy)
        {
            if (currentWalkDistance <= 0)
            {
                int r = RandomGenerator.Random.Next(0, 4);
                currentWalkDistance = randomWalkDistance;

                switch (r)
                {
                    case 0:
                        enemy.Velocity = new Vector2(1, 0);
                        enemy.CurrentSprite.CurrentAnimation = AnimationKey.Right;
                        break;
                    case 1:
                        enemy.Velocity = new Vector2(-1, 0);
                        enemy.CurrentSprite.CurrentAnimation = AnimationKey.Left;
                        break;
                    case 2:
                        enemy.Velocity = new Vector2(0, 1);
                        enemy.CurrentSprite.CurrentAnimation = AnimationKey.Down;
                        break;
                    case 3:
                        enemy.Velocity = new Vector2(0, -1);
                        enemy.CurrentSprite.CurrentAnimation = AnimationKey.Up;
                        break;
                }
            }
            else
            {
                enemy.Position = enemy.Position + enemy.Velocity;
                currentWalkDistance -= Math.Abs((enemy.Velocity.X + enemy.Velocity.Y) / 2);

                if (enemy.HasPlayerInSight)
                    enemy.ActionComplete();
            }
        }

        public void SetUp(Enemy enemy)
        {
            throw new NotImplementedException();
        }
    }
}
