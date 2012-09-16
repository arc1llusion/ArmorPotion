using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArmorPotionFramework.Items;
using ArmorPotionFramework.EntityClasses;

namespace ArmorPotionFramework.Inventory
{
    public class ItemCollection<T> where T : Items.Item
    {
        private int _max;
        private int _currentIndex;

        private readonly List<T> _items;

        public event EventHandler BeforeForward;

        public ItemCollection() : this(Int32.MaxValue)
        {
            _items = new List<T>();
        }

        public ItemCollection(int max)
        {
            _max = max;
            _currentIndex = 0;

        }

        internal List<T> Collection
        {
            get { return this._items; }
        }

        public T Current
        {
            get { return this._items[_currentIndex]; }
        }

        public void Add(T item)
        {
            if (_items.Count >= _max)
                throw new Exception("Cannot add any more to item collection");

            _items.Add(item);
        }

        internal T Forward(Player player)
        {
            if (BeforeForward != null)
                BeforeForward(this.Current, null);

            if (++_currentIndex >= _items.Count)
                _currentIndex = _items.Count - 1;

            return Current;
        }

        internal T Back()
        {
            if (--_currentIndex < 0)
                _currentIndex = 0;

            return Current;
        }
    }
}
