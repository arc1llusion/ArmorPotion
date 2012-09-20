using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.EntityClasses;
using Microsoft.Xna.Framework;

namespace ArmorPotionFramework.Game
{
    public class ArmorPotionsGame : Microsoft.Xna.Framework.Game
    {
        private SpriteBatch _spriteBatch;

        public SpriteBatch SpriteBatch
        {
            get
            {
                return _spriteBatch;
            }
            protected set
            {
                _spriteBatch = value;
            }
        }

        public Rectangle ScreenRectangle
        {
            get;
            set;
        }
    }
}
