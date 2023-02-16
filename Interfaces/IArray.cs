using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Interfaces
{
    internal interface IArray<T>
    {
        public void Add(T item);
        public void Insert(T item, int index);
        public T Remove(int index);
    }
}
