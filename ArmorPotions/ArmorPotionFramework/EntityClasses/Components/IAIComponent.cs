using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.EntityClasses;

namespace ArmorPotionFramework.EntityClasses.Components
{
    public interface IAIComponent
    {
        void SetUp(Enemy enemy);
        void Update(GameTime gameTime, Enemy enemy);
    }
}
