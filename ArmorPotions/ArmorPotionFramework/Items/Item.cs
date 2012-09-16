using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.EntityClasses;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.SpriteClasses;

namespace ArmorPotionFramework.Items
{
    public class ItemData
    {
        public Texture2D Icon;
        public String Name;
        public Type Type;

        public ItemData()
        {
        }

        public ItemData(Texture2D icon, String name,Type type)
        {
            this.Icon = icon;
            this.Name = name;
            this.Type = type;
        }
    }

    public abstract class Item
    {
        protected Texture2D _icon;
        protected String _name;
        protected List<Modifier> _modifiers;
        protected Vector2 _position;
        protected AnimatedSprite _sprite;
        
        protected bool _allowMulti;
        protected bool _hasProjectile;

        public Item(Texture2D icon, String name)
        {
            this._icon = icon;
            this._modifiers = new List<Modifier>();
            this._name = name;
        }

        public Item(Texture2D icon, String name, AnimatedSprite sprite)
            : this(icon, name)
        {
            _sprite = sprite;
        }

        public Item(ItemData data)
        {
            this._name = data.Name;
            this._icon = data.Icon;
        }

        public Texture2D ItemIcon
        {
            get
            {
                return this._icon;
            }
        }

        public AnimatedSprite AnimatedSprite
        {
            get
            {
                return this._sprite;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this._position;
            }

            set
            {
                this._position = value;
            }
        }

        public List<Modifier> Modifiers
        {
            get
            {
                return this._modifiers;
            }
        }

        public bool HasProjectile
        {
            get { return this._hasProjectile; }
            set { this._hasProjectile = value; }
        }

        public bool AllowMulti
        {
            get { return this._allowMulti; }
        }

        public abstract void CollectedBy(Player player);

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void DrawIcon(SpriteBatch spriteBatch)
        {
            if (_icon != null) spriteBatch.Draw(_icon, _position, Color.White);
        }
    }
}
