using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.Items;
using ArmorPotionFramework.Loading;
using ArmorPotionFramework.WorldClasses;
using Microsoft.Xna.Framework.Graphics;
using System.Xml;

namespace ArmorPotions.Factories
{
    public class ItemFactory : Factory<Item, ItemData>
    {
        public Dictionary<String, ItemData> items;
        public ItemFactory(World world, String basePath) : base(world, basePath)
        {
            LoadData();
        }

        public override Item Create(string name)
        {
            ItemData data = Objects[name];
            List<Object> args = new List<Object>{ data.Icon, data.Name };
            args.AddRange(data.Args);

            return CreateInstance<Item>(data.Type, args.ToArray());
        }

        protected override ItemData ReadObjectData(XmlElement element, out string name)
        {
            ItemData data = new ItemData();

            data.Name = element.Attributes["Name"].Value;
            data.Icon = Content.Load<Texture2D>(element.Attributes["Texture"].Value);
            data.Type = Type.GetType(element.Attributes["Type"].Value);
            
            List<Object> args = new List<Object>();

            XmlNode spriteNode = element.SelectSingleNode("SpriteSheet");
            if (spriteNode != null)
            {
                data.Sprite = CreateSpriteSheet(spriteNode).Sprite;
                args.Add(data.Sprite);
            }

            foreach (XmlNode node in element.SelectSingleNode("ConstructorArgs").SelectNodes("Argument"))
            {
                if (node.Attributes["Type"].Value == "Sprite")
                    args.Add(CreateSpriteSheet(node).Sprite);
                else
                {
                    args.Add(Convert.ChangeType(node.Attributes["Value"].Value, Type.GetType(node.Attributes["Type"].Value)));
                }
            }

            name = data.Name;
            data.Args = args;

            return data;
        }
    }
}
