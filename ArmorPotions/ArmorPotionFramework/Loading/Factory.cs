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

namespace ArmorPotionFramework.Loading
{
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
                    else
                        property.SetValue(obj, Convert.ChangeType(node.Attributes[property.Name].Value, property.PropertyType), null);
                }
            }
        }

        #endregion
    }
}
