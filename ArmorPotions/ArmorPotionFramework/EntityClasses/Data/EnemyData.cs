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
        public int LeftCollisionOffset = 0;
        public int RightCollisionOffset = 0;
        public int TopCollisionOffset = 0;
        public int BottomCollisionOffset = 0;
        public IAIComponent IdleComponent = null;
        public IAIComponent DecisionComponent = null;
        public int Health = 0;
        public Dictionary<String, IAIComponent> ActionComponents = new Dictionary<string, IAIComponent>();
        public Dictionary<String, AnimatedSprite> Sprites = new Dictionary<string, AnimatedSprite>();
        public float SightRadius = 0;
        public String Color = "";

        public Dictionary<String, IAIComponent> DeepCopyActionComponents
        {
            get
            {
                return ActionComponents.ToDictionary(k => k.Key, v => v.Value.Clone());
            }
        }

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
