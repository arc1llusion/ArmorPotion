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
using ArmorPotionFramework.Loading;
using System.Reflection;

namespace ArmorPotions.Factories
{
    public class EnemyFactory : ArmorPotionFramework.Loading.Factory<Enemy, EnemyData>
    {
        public EnemyFactory(World world, String basePath) : base(world, basePath)
        {
            LoadData();
        }

        public override Enemy Create(String name)
        {
            EnemyData enemyData = Objects[name];
            Enemy enemy = new Enemy(World, enemyData);
            enemy.Name = name;

            return enemy;
        }

        protected override EnemyData ReadObjectData(XmlElement element, out String name)
        {
            EnemyData data = new EnemyData();

            data.Name = element.Attributes["Name"].Value;
            
            data.Velocity = ReadVector2(element.SelectSingleNode("Velocity"));

            foreach (XmlNode node in element.SelectSingleNode("SpriteSheets").SelectNodes("Sprite"))
            {
                SpriteInfo sprite = CreateSpriteSheet(node);
                data.Sprites.Add(sprite.Name, sprite.Sprite);
                data.Texture = sprite.Sprite.Texture;
            }

            XmlNode collisionNode = element.SelectSingleNode("CollisionOffset");

            if (collisionNode != null)
            {
                data.LeftCollisionOffset = int.Parse(collisionNode.Attributes["Left"].Value);
                data.RightCollisionOffset = int.Parse(collisionNode.Attributes["Right"].Value);
                data.TopCollisionOffset = int.Parse(collisionNode.Attributes["Top"].Value);
                data.BottomCollisionOffset = int.Parse(collisionNode.Attributes["Bottom"].Value);
            }

            data.IdleComponent = GetAIComponent(element.SelectSingleNode("IdleComponent"));
            data.DecisionComponent = GetAIComponent(element.SelectSingleNode("DecisionComponent"));

            foreach (XmlNode node in element.SelectSingleNode("ActionComponents").SelectNodes("ActionComponent"))
            {
                IAIComponent ActionComponent = GetAIComponent(node);

                PropertyInfo[] properties = ActionComponent.GetType().GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    foreach (XmlNode propNode in node.SelectNodes("Properties"))
                    {
                        if (propNode.Attributes["PropertyName"].Value == property.Name && propNode.Attributes["Type"].Value == "SpriteSheet")
                            property.SetValue(ActionComponent, CreateSpriteSheet(propNode).Sprite, null);
                    }
                }

                data.ActionComponents.Add(node.Attributes["Name"].Value, ActionComponent);
            }

            SetFields<EnemyData>(data, element.SelectSingleNode("Fields"));

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

        private IAIComponent GetAIComponent(XmlNode node)
        {
            IAIComponent component = CreateInstance<IAIComponent>(GetAttributeType(node.Attributes["Type"].Value));
            SetProperties<IAIComponent>(component, node);

            return component;
        }
    }
}
