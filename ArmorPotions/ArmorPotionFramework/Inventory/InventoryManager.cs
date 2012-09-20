using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.Items;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.EntityClasses;
using Microsoft.Xna.Framework.Graphics;
using ArmorPotionFramework.Utility;

namespace ArmorPotionFramework.Inventory
{
    public class InventoryManager
    {
        private readonly List<TempaQuip> _tempaQuips;
        private readonly List<InstaQuip> _instaQuips;
        private readonly List<PermaQuip> _permaQuips;

        private int _tempaQuipIndex;
        private int _instaQuipIndex;
        private int _permaQuipIndex;

        private Vector2 _position;

        public InventoryManager(Vector2 position)
        {
            _tempaQuips = new List<TempaQuip>();
            _instaQuips = new List<InstaQuip>();
            _permaQuips = new List<PermaQuip>();

            _tempaQuipIndex = 0;
            _instaQuipIndex = 0;
            _permaQuipIndex = 0;

            _position = position;
        }

        public List<TempaQuip> TempaQuips
        {
            get { return this._tempaQuips; }
        }

        public List<InstaQuip> InstaQuips
        {
            get { return this._instaQuips; }
        }

        public List<PermaQuip> PermaQuips
        {
            get { return this._permaQuips; }
        }

        public TempaQuip CurrentTempaQuip
        {
            get
            {
                if (_tempaQuips.Count == 0 || (_tempaQuipIndex < 0 || _tempaQuipIndex >= _tempaQuips.Count))
                    return null;

                return this._tempaQuips[_tempaQuipIndex];
            }
        }

        public InstaQuip CurrentInstaQuip
        {
            get
            {
                if (_instaQuips.Count == 0 || (_instaQuipIndex < 0 || _instaQuipIndex >= _instaQuips.Count))
                    return null;

                return this._instaQuips[_instaQuipIndex];
            }
        }

        public PermaQuip CurrentPermaQuip
        {
            get
            {
                if (_permaQuips.Count == 0 || (_permaQuipIndex < 0 || _permaQuipIndex >= _permaQuips.Count))
                    return null;

                return this._permaQuips[_permaQuipIndex];
            }
        }

        public void SelectRelativeTempaQuip(Player player, int deltaIndex)
        {
            if (CurrentTempaQuip != null)
                CurrentTempaQuip.OnUnEquip(player);

            _tempaQuipIndex = (int)MathHelper.Clamp(_tempaQuipIndex + deltaIndex, 1, _tempaQuips.Count - 1);

            CurrentTempaQuip.OnEquip(player);
        }

        public void SelectRelativeInstaQuip(int deltaIndex)
        {
            _instaQuipIndex = (int)MathHelper.Clamp(_instaQuipIndex + deltaIndex, 0, _instaQuips.Count - 1);
        }

        public void SelectRelativePermaQuip(int deltaIndex)
        {
            _permaQuipIndex = (int)MathHelper.Clamp(_permaQuipIndex + deltaIndex, 0, _permaQuips.Count - 1);
        }

        public void ConsumeInstaQuip(Player player)
        {
            CurrentInstaQuip.ConsumedBy(player);
            InstaQuips.Remove(CurrentInstaQuip);
        }

        public void ActivatePermaQuip(Player player)
        {
            CurrentPermaQuip.ActivatedBy(player);
            PermaQuips.Remove(CurrentPermaQuip);
        }

        public void ActivateTempaQuip(GameTime gameTime, Player player)
        {
            CurrentTempaQuip.OnActivate(gameTime, player);
        }

        private void ForwardIndex<T>(ref int index, List<T> list)
        {
            if (++index >= list.Count)
                index = list.Count - 1;
        }

        private void BackIndex<T>(ref int index, List<T> list)
        {
            if (--index < 0)
                index = 0;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, float bottomWindowBound)
        {
            float x = _position.X;
            float y = _position.Y;

            for (int i = 0; i < _tempaQuips.Count; i++)
            {
                _tempaQuips[i].DrawIcon(gameTime, spriteBatch, x, y);

                if (i == _tempaQuipIndex)
                    RectangleExtensions.DrawRectangleBorder(new Rectangle((int)Math.Floor(x), (int)Math.Floor(y), 68, 68), spriteBatch, 4, Color.Blue);

                x += 70;
            }
        }
    }
}
