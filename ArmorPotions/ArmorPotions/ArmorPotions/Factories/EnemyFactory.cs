using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.Xml;
using ArmorPotionFramework.Utility;
using ArmorPotionFramework.EntityClasses.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.SpriteClasses;
using ArmorPotionFramework.EntityClasses;
using ArmorPotions;
using ArmorPotionFramework.EntityClasses.Data;
using ArmorPotionFramework.WorldClasses;

namespace ArmorPotions.Factories
{
    struct SpriteInfo
    {
        public String Name;
        public AnimatedSprite Sprite;
        public SpriteInfo(String name, AnimatedSprite sprite)
        {
            Name = name;
            Sprite = sprite;
        }
    }

    public class EnemyFactory : ArmorPotionFramework.Loading.Factory<Enemy, EnemyData>
    {
        public EnemyFactory(World world, String basePath) : base(world, basePath)
        {
            LoadData();
        }

        public override Enemy Create(String name)
        {
            return new Enemy(World, Objects[name]);
        }

        protected override EnemyData ReadObjectData(XmlElement element, out String name)
        {
            EnemyData data = new EnemyData();

            data.Name = element.Attributes["Name"].Value;
            
            data.Velocity = ReadVector2(element.SelectSingleNode("Velocity"));

            SpriteInfo sprite = CreateSpriteSheet(element.SelectSingleNode("SpriteSheets"));
            data.Sprites.Add(sprite.Name, sprite.Sprite);
            data.Texture = sprite.Sprite.Texture;

            data.IdleComponent = GetAIComponent(element.SelectSingleNode("IdleComponent"));//CreateInstance<IAIComponent>((GetAttributeType(element, "IdleComponent")));
            data.DecisionComponent = GetAIComponent(element.SelectSingleNode("DecisionComponent")); //CreateInstance<IAIComponent>((GetAttributeType(element, "DecisionComponent")));

            foreach (XmlNode node in element.SelectSingleNode("ActionComponents").SelectNodes("ActionComponent"))
            {
                data.ActionComponents.Add(node.Attributes["Name"].Value, GetAIComponent(node));
            }

            name = data.Name;
            return data;
        }

        private Type GetAttributeType(XmlNode element, String name)
        {
            return Type.GetType(element.SelectSingleNode(name).Attributes["Type"].Value);
        }

        private Type GetAttributeType(String name)
        {
            return Type.GetType(name);
        }

        private Vector2 ReadVector2(XmlNode element)
        {
            return new Vector2(float.Parse(element.Attributes["X"].Value), float.Parse(element.Attributes["Y"].Value));
        }

        private Texture2D ReadTexture(XmlNode element)
        {
            return Content.Load<Texture2D>(BasePath + @"\" + element.Attributes["Texture"].Value);
        }

        private SpriteInfo CreateSpriteSheet(XmlNode element)
        {
            Dictionary<AnimationKey, Animation> animations = new Dictionary<AnimationKey, Animation>();
            XmlNode sprite = element.SelectSingleNode("Sprite");

            foreach (XmlNode node in sprite.SelectNodes("Animation"))
            {
                Animation animation = new Animation(
                    int.Parse(node.Attributes["FrameCount"].Value),
                    int.Parse(node.Attributes["Width"].Value),
                    int.Parse(node.Attributes["Height"].Value),
                    int.Parse(node.Attributes["xOffset"].Value),
                    int.Parse(node.Attributes["yOffset"].Value));

                animation.FramesPerSecond = int.Parse(node.Attributes["FramesPerSecond"].Value);

                animations.Add((AnimationKey)Enum.Parse(typeof(AnimationKey), node.Attributes["Key"].Value), animation);
            }

            return new SpriteInfo(sprite.Attributes["Name"].Value, new AnimatedSprite(ReadTexture(sprite), animations));
        }

        private IAIComponent GetAIComponent(XmlNode node)
        {
            IAIComponent component = CreateInstance<IAIComponent>(GetAttributeType(node.Attributes["Type"].Value));
            SetProperties<IAIComponent>(component, node);

            return component;
        }
    }
}
