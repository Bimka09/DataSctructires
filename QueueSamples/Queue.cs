using System;
using DataStructures.Sctructures;

namespace DataStructures.QueueSamples
{
    public class Queue<T>
    {
        private T[] _array;
        private int _head;
        private int _tail;
        private int _size;
        private int _growFactor = 2;
        private const int _MinimumGrow = 4;
        private static readonly T[] emptyArray = EmptyArray<T>.Value;

        public Queue()
        {
            _array = emptyArray;
        }
        public int Count
        {
            get { return _size; }
        }
        public void Enqueue(T value)
        {
            if (_size == _array.Length)
            {
                int newcapacity = (int)(((long)_array.Length * (long)_growFactor) / 10);
                if (newcapacity < _array.Length + _MinimumGrow)
                {
                    newcapacity = _array.Length + _MinimumGrow;
                }
                SetCapacity(newcapacity);
            }

            _array[_tail] = value;
            _tail = (_tail + 1) % _array.Length;
            _size++;
        }
        public T Dequeue()
        {
            if (Count == 0)
                throw new InvalidOperationException("InvalidOperation_EmptyQueue");

            T removed = _array[_head];
            _array[_head] = default;
            _head = (_head + 1) % _array.Length;
            _size--;
            return removed;
        }
        private void SetCapacity(int capacity)
        {
            T[] newarray = new T[capacity];
            if (_size > 0)
            {
                if (_head < _tail)
                {
                    Array.Copy(_array, _head, newarray, 0, _size);
                }
                else
                {
                    Array.Copy(_array, _head, newarray, 0, _array.Length - _head);
                    Array.Copy(_array, 0, newarray, _array.Length - _head, _tail);
                }
            }

            _array = newarray;
            _head = 0;
            _tail = _size == capacity ? 0 : _size;
        }
        public bool Contains(T value)
        {
            int index = _head;
            int count = _size;

            while (count-- > 0)
            {
                if (value == null)
                {
                    if (_array[index] == null)
                        return true;
                }
                else if (_array[index] != null && _array[index].Equals(value))
                {
                    return true;
                }
                index = (index + 1) % _array.Length;
            }

            return false;
        }
    }
}
