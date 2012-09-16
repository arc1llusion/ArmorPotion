using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.Items;
using ArmorPotionFramework.Loading;
using ArmorPotionFramework.WorldClasses;
using Microsoft.Xna.Framework.Graphics;

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
            Object[] args = { data.Icon, data.Name };
            return CreateInstance<Item>(data.Type, args);
        }

        protected override ItemData ReadObjectData(System.Xml.XmlElement element, out string name)
        {
            ItemData data = new ItemData();

            data.Name = element.Attributes["Name"].Value;
            data.Icon = Content.Load<Texture2D>(element.Attributes["Texture"].Value);
            data.Type = Type.GetType(element.Attributes["Type"].Value);

            name = data.Name;

            return data;
        }
    }
}
