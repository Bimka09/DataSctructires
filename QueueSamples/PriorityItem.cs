using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.QueueSamples
{
    public class PriorityItem<T>
    {
        private T _data;

        private PriorityItem<T> _sequentialPrev;
        private PriorityItem<T> _sequentialNext;

        private PriorityChain<T> _chain;
        private PriorityItem<T> _priorityPrev;
        private PriorityItem<T> _priorityNext;
        public PriorityItem(T data)
        {
            _data = data;
        }

        public T Data { get { return _data; } }
        public bool IsQueued { get { return _chain != null; } }

        public PriorityItem<T> SequentialPrev { get { return _sequentialPrev; } set { _sequentialPrev = value; } }
        public PriorityItem<T> SequentialNext { get { return _sequentialNext; } set { _sequentialNext = value; } }

        public PriorityChain<T> Chain { get { return _chain; } set { _chain = value; } }
        public PriorityItem<T> PriorityPrev { get { return _priorityPrev; } set { _priorityPrev = value; } }
        public PriorityItem<T> PriorityNext { get { return _priorityNext; } set { _priorityNext = value; } }

    }
}
