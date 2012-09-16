using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.SpriteClasses;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.EntityClasses.Components;
using Microsoft.Xna.Framework.Graphics;

namespace ArmorPotionFramework.EntityClasses.Data
{
    public class EnemyData
    {
        public String Name = String.Empty;
        public Texture2D Texture = null;
        public Vector2 Velocity = new Vector2();
        public IAIComponent IdleComponent = null;
        public IAIComponent DecisionComponent = null;
        public Dictionary<String, IAIComponent> ActionComponents = new Dictionary<string, IAIComponent>();
        public Dictionary<String, AnimatedSprite> Sprites = new Dictionary<string, AnimatedSprite>();

        public Dictionary<String, AnimatedSprite> DeepCopySprites
        {
            get
            {
                Dictionary<String, AnimatedSprite> temp = new Dictionary<string, AnimatedSprite>();

                foreach (KeyValuePair<String, AnimatedSprite> pair in Sprites)
                {
                    temp.Add(pair.Key, pair.Value.Clone());
                }

                return temp;
            }
        }
    }
}
