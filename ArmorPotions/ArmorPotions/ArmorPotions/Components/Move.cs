using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArmorPotionFramework.EntityClasses;
using ArmorPotionFramework.EntityClasses.Components;

namespace ArmorPotions.Components
{
    public class Move : IAIComponent
    {
        private int max;
        private int current;

        private int life;

        public Move()
        {
            max = 500;
            current = max;
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime, Enemy enemy)
        {
            if (current > 0)
            {
                current -= gameTime.ElapsedGameTime.Milliseconds;
                life -= gameTime.ElapsedGameTime.Milliseconds;
                enemy.Position = enemy.Position + enemy.Velocity;

                if (life < 0)
                    enemy.ActionComplete();
            }
            else
            {
                current = max;
            }
        }

        public void SetUp(Enemy enemy)
        {
            life = 3000;
        }


        public IAIComponent Clone()
        {
            return new Move();
        }
    }
}
