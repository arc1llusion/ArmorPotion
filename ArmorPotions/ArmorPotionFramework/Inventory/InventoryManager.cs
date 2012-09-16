using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.Items;
using Microsoft.Xna.Framework;
using ArmorPotionFramework.EntityClasses;

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

        public InventoryManager()
        {
            _tempaQuips = new List<TempaQuip>();
            _instaQuips = new List<InstaQuip>();
            _permaQuips = new List<PermaQuip>();

            _tempaQuipIndex = 0;
            _instaQuipIndex = 0;
            _permaQuipIndex = 0;
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

        //public void ForwardTempaQuip(Player player)
        //{
        //    CurrentTempaQuip.OnUnEquip(player);
        //    ForwardIndex<TempaQuip>(ref _tempaQuipIndex, _tempaQuips);
        //    CurrentTempaQuip.OnEquip(player);
        //}

        //public void BackTempaQuip(Player player)
        //{
        //    CurrentTempaQuip.OnUnEquip(player);
        //    BackIndex<TempaQuip>(ref _tempaQuipIndex, _tempaQuips);
        //    CurrentTempaQuip.OnEquip(player);
        //}

        //public void ForwardInstaQuip()
        //{
        //    ForwardIndex<InstaQuip>(ref _instaQuipIndex, _instaQuips);
        //}

        //public void BackInstaQuip()
        //{
        //    BackIndex<InstaQuip>(ref _instaQuipIndex, _instaQuips);
        //}

        //public void ForwardPermaQuip()
        //{
        //    ForwardIndex<PermaQuip>(ref _permaQuipIndex, _permaQuips);
        //}

        //public void BackPermaQuip()
        //{
        //    BackIndex<PermaQuip>(ref _permaQuipIndex, _permaQuips);
        //}

        public void SelectRelativeTempaQuip(Player player, int deltaIndex)
        {
            if (CurrentTempaQuip != null)
                CurrentTempaQuip.OnUnEquip(player);

            _tempaQuipIndex = (int)MathHelper.Clamp(_tempaQuipIndex + deltaIndex, 0, _tempaQuips.Count - 1);

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

        public void ActivateTempaQuip(Player player)
        {
            CurrentTempaQuip.OnActivate(player);
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
    }
}
