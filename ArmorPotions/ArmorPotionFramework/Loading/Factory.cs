using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.WorldClasses;
using Microsoft.Xna.Framework.Content;
using ArmorPotionFramework.Utility;
using System.Xml;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.SpriteClasses;

namespace ArmorPotionFramework.Loading
{
    public struct SpriteInfo
    {
        public String Name;
        public AnimatedSprite Sprite;
        public SpriteInfo(String name, AnimatedSprite sprite)
        {
            Name = name;
            Sprite = sprite;
        }
    }

    public abstract class Factory<T, K>
    {
        #region Fields

        private World _world;
        private ContentManager _content;
        private readonly String _basePath;

        private Dictionary<String, K> _objects;

        #endregion

        #region Constructors

        public Factory(World world, String basePath)
        {
            _world = world;
            _content = world.Game.Content;
            _basePath = basePath;

            _objects = new Dictionary<string, K>();
        }

        #endregion

        #region Properties

        public World World { get { return this._world; } }
        public ContentManager Content { get { return this._content; } }
        public String BasePath { get { return this._basePath; } }
        public Dictionary<String, K> Objects { get { return this._objects; } }

        #endregion

        #region Abstract methods

        public abstract T Create(String name);
        protected abstract K ReadObjectData(XmlElement element, out String name);

        #endregion

        #region Virtual Methods

        protected virtual void LoadData()
        {
            XmlSource xs = _content.Load<XmlSource>(_basePath + @"\" + typeof(K).Name);
            XmlDocument document = new XmlDocument();
            document.LoadXml(xs.XmlCode);

            foreach (XmlElement element in document.DocumentElement.GetElementsByTagName(typeof(T).Name))
            {
                String name;
                K data = ReadObjectData(element, out name);
                _objects.Add(name, data);
            }
        }

        protected virtual N CreateInstance<N>(Type type)
        {
            return (N)Activator.CreateInstance(type);
        }

        protected virtual N CreateInstance<N>(Type type, Object[] args)
        {
            return (N)Activator.CreateInstance(type, args);
        }

        protected virtual void SetProperties<N>(N obj, XmlNode node)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (node.Attributes[property.Name] != null)
                {
                    if (property.PropertyType == typeof(Texture2D))
                        property.SetValue(obj, Content.Load<Texture2D>(node.Attributes[property.Name].Value), null);
                    else if(property.PropertyType.IsEnum)
                        property.SetValue(obj, Enum.Parse(property.PropertyType, node.Attributes[property.Name].Value), null);
                    else
                        property.SetValue(obj, Convert.ChangeType(node.Attributes[property.Name].Value, property.PropertyType), null);
                }
            }
        }

        protected virtual void SetFields<N>(N obj, XmlNode node)
        {
            FieldInfo[] fields = obj.GetType().GetFields();

            foreach (FieldInfo field in fields)
            {
                if (node.Attributes[field.Name] != null)
                {
                    if (field.FieldType == typeof(Texture2D))
                        field.SetValue(obj, Content.Load<Texture2D>(node.Attributes[field.Name].Value));
                    else if (field.FieldType.IsEnum)
                        field.SetValue(obj, Enum.Parse(field.FieldType, node.Attributes[field.Name].Value));
                    else
                        field.SetValue(obj, Convert.ChangeType(node.Attributes[field.Name].Value, field.FieldType));
                }
            }
        }

        protected virtual SpriteInfo CreateSpriteSheet(XmlNode sprite)
        {
            Dictionary<AnimationKey, Animation> animations = new Dictionary<AnimationKey, Animation>();

            foreach (XmlNode node in sprite.SelectNodes("Animation"))
            {
                Animation animation = new Animation(
                    int.Parse(node.Attributes["FrameCount"].Value),
                    int.Parse(node.Attributes["Width"].Value),
                    int.Parse(node.Attributes["Height"].Value),
                    int.Parse(node.Attributes["xOffset"].Value),
                    int.Parse(node.Attributes["yOffset"].Value));

                animation.FramesPerSecond = int.Parse(node.Attributes["FramesPerSecond"].Value);

                if (node.Attributes["Loop"] != null)
                {
                    bool loop = true;
                    bool.TryParse(node.Attributes["Loop"].Value, out loop);
                    animation.Loop = loop;
                }

                animations.Add((AnimationKey)Enum.Parse(typeof(AnimationKey), node.Attributes["Key"].Value), animation);
            }

            AnimatedSprite newSprite = new AnimatedSprite(ReadTexture(sprite), animations);

            if (sprite.Attributes["IsAnimating"] != null)
                newSprite.IsAnimating = Boolean.Parse(sprite.Attributes["IsAnimating"].Value);

            return new SpriteInfo(sprite.Attributes["Name"].Value, newSprite);
        }

        protected Texture2D ReadTexture(XmlNode element)
        {
            return Content.Load<Texture2D>(BasePath + @"\" + element.Attributes["Texture"].Value);
        }

        #endregion
    }
}
