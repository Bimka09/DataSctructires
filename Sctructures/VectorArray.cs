using DataStructures.Interfaces;
using System;

namespace DataStructures.Sctructures
{
    public class VectorArray<T> : IArray<T>
    {
        private T[] _items;
        private int _size;
        private int _vector;

        public VectorArray(int vector)
        {
            _vector = vector;
            _items = new T[_vector];
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
            int newCapacity = _items.Length + _vector;
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
