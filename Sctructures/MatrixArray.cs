using DataStructures.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Sctructures
{
    public class MatrixArray<T> : IArray<T>
    {
        private List<T[]> arrayX;
        public int size { get => arrayX.Count * _capasity; }
        public bool IsEmpty { get => this.size == 0; }
        private int _capasity { get; }
        private int _emptySpaces;

        public T this[int index]
        {
            get => arrayX[index / _capasity][index % _capasity];
            set => arrayX[index / _capasity][index % _capasity] = value;
        }

        public MatrixArray(int capasity)
        {
            _capasity = capasity;
            _emptySpaces = capasity;
            arrayX = new List<T[]>
            {
                new T[capasity]
            };
        }

        public void Add(T value)
        {
            if (_emptySpaces == 0)
            {
                ResizePlus();
            }
            arrayX[arrayX.Count - 1][_capasity - _emptySpaces] = value;
            _emptySpaces--;
        }

        private void ResizePlus()
        {
            arrayX.Add(new T[_capasity]);
            _emptySpaces = _capasity;
        }

        public T Remove(int index)
        {
            if (IsEmpty)
            {
                throw new IndexOutOfRangeException("Array is empty");
            }
            var element = arrayX[index / _capasity][index % _capasity];
            return element;
        }

        public T Get(int index)
        {
            return arrayX[index / _capasity][index];
        }

        public void Insert(T item, int index)
        {
            ShiftResizeAdd(index);
            arrayX[index / _capasity][index % _capasity] = item;
            _emptySpaces--;
        }

        private void ShiftResizeAdd(int index)
        {
            arrayX.Add(new T[_capasity]);
            var rowIndex = index / _capasity;
            for (int i = arrayX.Count - 1; i > rowIndex; i--)
            {
                var tempArrayI = new T[_capasity];
                arrayX[i].CopyTo(tempArrayI, 0);

                for (int j = 0; j < _capasity; j++)
                {
                    if (j == 0)
                    {
                        tempArrayI[j] = arrayX[i - 1][_capasity - 1];
                    }
                    else
                    {
                        tempArrayI[j] = arrayX[i][j - 1];
                    }
                }
                arrayX[i] = tempArrayI;
            }
            var tempArray = new T[_capasity];
            arrayX[rowIndex].CopyTo(tempArray, 0);
            var colIndex = index % _capasity;
            for (int j = _capasity - 1; j > colIndex; j--)
            {
                tempArray[j] = arrayX[rowIndex][j - 1];
            }
            arrayX[rowIndex] = tempArray;
            _emptySpaces += _capasity;
        }
    }
}
