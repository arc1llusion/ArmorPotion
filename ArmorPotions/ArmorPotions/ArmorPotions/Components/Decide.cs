using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.EntityClasses;
using ArmorPotionFramework.EntityClasses.Components;
using ArmorPotionFramework.Utility;

namespace ArmorPotions.Components
{
    public class Decide : IAIComponent
    {
        public Decide() { }

        public void Update(GameTime gameTime, Enemy enemy)
        {
            int selection = RandomGenerator.Random.Next(0, enemy.ActionComponents.Count);
            IAIComponent component = enemy.ActionComponents.Values.ElementAt(selection);

            component.SetUp(enemy);
            
            enemy.ActiveComponent = component.Update;
        }

        public void SetUp(Enemy enemy)
        {
            throw new NotImplementedException();
        }
    }
}
