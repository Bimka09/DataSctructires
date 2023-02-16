using DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Sctructures
{
    public class FactorArray<T> : IArray<T>
    {
        private T[] _items;
        private int _size;
        private int _factor;
        private const int _defaultCapacity = 1;
        private static readonly T[] emptyArray = EmptyArray<T>.Value;

        public FactorArray(int factor)
        {
            _factor = factor;
            _items = emptyArray;
        }
        public int Capacity
        {
            get
            {
                return _items.Length;
            }
            set
            {
                T[] newItems = new T[value];
                if (_size > 0)
                {
                    Array.Copy(_items, 0, newItems, 0, _size);
                }
                _items = newItems;
            }
        }
        public int Count { get => _size; }
        public void Add(T value)
        {
            if (_size == _items.Length) EnsureCapacity(_size + 1);
            _items[_size] = value;
            _size++;
        }
        public void Insert(T value, int index)
        {
            if (index > _size) throw new IndexOutOfRangeException("Given index lager then range of array");

            if (index == _size)
            {
                Add(value);
                return;
            }

            if (_size == _items.Length) EnsureCapacity(_size + 1);
            T[] newItems = new T[Capacity];
            Array.Copy(_items, 0, newItems, 0, index);
            newItems[index] = value;
            Array.Copy(_items, index, newItems, index + 1, _size - index);
            _items = newItems;
            _size++;
        }
        private void EnsureCapacity(int min)
        {
            int newCapacity = _items.Length == 0 ? _defaultCapacity : _items.Length * _factor;
            Capacity = newCapacity;
        }
        public T Remove(int index)
        {
            if (index < 0 || index >= _size) throw new ArgumentOutOfRangeException();
            var deletedElement = _items[index];
            _size--;
            if (index < _size)
            {
                Array.Copy(_items, index + 1, _items, index, _size - index);
            }
            _items[_size] = default(T);
            return deletedElement;
        }
    }
}
