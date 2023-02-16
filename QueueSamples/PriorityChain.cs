using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.QueueSamples
{
    public class PriorityChain<T>
    {
        private PriorityItem<T> _head;
        private PriorityItem<T> _tail;
        private DispatcherPriority _priority;
        private int _count;
        public PriorityChain(DispatcherPriority priority) 
        {
            _priority = priority;
        }

        public DispatcherPriority Priority { get { return _priority; } set { _priority = value; } }
        public int Count { get { return _count; } set { _count = value; } }
        public PriorityItem<T> Head { get { return _head; } set { _head = value; } }
        public PriorityItem<T> Tail { get { return _tail; } set { _tail = value; } }
    }
}
