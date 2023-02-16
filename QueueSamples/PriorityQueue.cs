using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DataStructures.QueueSamples
{
    internal class PriorityQueue<T>
    {
        private SortedList<int, PriorityChain<T>> _priorityChains;
        private Stack<PriorityChain<T>> _cacheReusableChains;

        private PriorityItem<T> _head;
        private PriorityItem<T> _tail;
        private int _count;
        public PriorityQueue()
        {
            _priorityChains = new SortedList<int, PriorityChain<T>>(); 
            _cacheReusableChains = new Stack<PriorityChain<T>>(10);

            _head = _tail = null;
            _count = 0;
        }
        public PriorityItem<T> Enqueue(DispatcherPriority priority, T data)
        {
            PriorityChain<T> chain = GetChain(priority);

            PriorityItem<T> priorityItem = new PriorityItem<T>(data);

            //InsertItemInSequentialChain(priorityItem, _tail);

            InsertItemInPriorityChain(priorityItem, chain, chain.Tail);

            return priorityItem;
        }
        private PriorityChain<T> GetChain(DispatcherPriority priority) 
        {
            PriorityChain<T> chain = null;

            int count = _priorityChains.Count;
            if (count > 0)
            {
                if (priority == (DispatcherPriority)_priorityChains.Keys[0])
                {
                    chain = _priorityChains.Values[0];
                }
                else if (priority == (DispatcherPriority)_priorityChains.Keys[count - 1])
                {
                    chain = _priorityChains.Values[count - 1];
                }
                else if ((priority > (DispatcherPriority)_priorityChains.Keys[0]) &&
                        (priority < (DispatcherPriority)_priorityChains.Keys[count - 1]))
                {
                    _priorityChains.TryGetValue((int)priority, out chain);
                }
            }

            if (chain == null)
            {
                if (_cacheReusableChains.Count > 0)
                {
                    chain = _cacheReusableChains.Pop();
                    chain.Priority = priority;
                }
                else
                {
                    chain = new PriorityChain<T>(priority);
                }

                _priorityChains.Add((int)priority, chain);
            }

            return chain;
        }
        private void InsertItemInSequentialChain(PriorityItem<T> item, PriorityItem<T> after)
        {
            
            if (after == null)
            {
                if (_head != null)
                {
                    _head.SequentialPrev = item;
                    item.SequentialNext = _head;
                    _head = item;
                }
                else
                {
                    _head = _tail = item;
                }
            }
            else
            {
                item.SequentialPrev = after;

                if (after.SequentialNext != null)
                {
                    item.SequentialNext = after.SequentialNext;
                    after.SequentialNext.SequentialPrev = item;
                    after.SequentialNext = item;
                }
                else
                {
                    after.SequentialNext = item;
                    _tail = item;
                }
            }

            _count++;
        }
        private void InsertItemInPriorityChain(PriorityItem<T> item, PriorityChain<T> chain, PriorityItem<T> after)
        {
            item.Chain = chain;

            if (after == null)
            {

                if (chain.Head != null)
                {
                    chain.Head.PriorityPrev = item;
                    item.PriorityNext = chain.Head;
                    chain.Head = item;
                }
                else
                {
                    chain.Head = chain.Tail = item;
                }
            }
            else
            {
                item.PriorityPrev = after;

                if (after.PriorityNext != null)
                {
                    item.PriorityNext = after.PriorityNext;
                    after.PriorityNext.PriorityPrev = item;
                    after.PriorityNext = item;
                }
                else
                {
                    after.PriorityNext = item;
                    chain.Tail = item;
                }
            }

            chain.Count++;
        }

        public T Dequeue()
        {
            int count = _priorityChains.Count;
            if (count > 0)
            {
                PriorityChain<T> chain = _priorityChains.Values[count - 1];

                PriorityItem<T> item = chain.Head;

                RemoveItem(item);

                return item.Data;
            }
            else
            {
                throw new InvalidOperationException();

            }
        }
        public void RemoveItem(PriorityItem<T> item)
        {
            PriorityChain<T> chain = item.Chain;

            RemoveItemFromPriorityChain(item);

            //RemoveItemFromSequentialChain(item);
        }
        private void RemoveItemFromPriorityChain(PriorityItem<T> item)
        {

            if (item.PriorityPrev != null)
            {
                item.PriorityPrev.PriorityNext = item.PriorityNext;
            }
            else
            {
                item.Chain.Head = item.PriorityNext;
            }

            if (item.PriorityNext != null)
            {
                item.PriorityNext.PriorityPrev = item.PriorityPrev;
            }
            else
            {
                item.Chain.Tail = item.PriorityPrev;
            }

            item.PriorityPrev = item.PriorityNext = null;
            item.Chain.Count--;
            if (item.Chain.Count == 0)
            {
                if (item.Chain.Priority == (DispatcherPriority)_priorityChains.Keys[_priorityChains.Count - 1])
                {
                    _priorityChains.RemoveAt(_priorityChains.Count - 1);
                }
                else
                {
                    _priorityChains.Remove((int)item.Chain.Priority);
                }

                if (_cacheReusableChains.Count < 10)
                {
                    _cacheReusableChains.Push(item.Chain);
                }
            }

            item.Chain = null;
        }
        private void RemoveItemFromSequentialChain(PriorityItem<T> item)
        {

            if (item.SequentialPrev != null)
            {
                item.SequentialPrev.SequentialNext = item.SequentialNext;
            }
            else
            {
                _head = item.SequentialNext;
            }

            if (item.SequentialNext != null)
            {
                item.SequentialNext.SequentialPrev = item.SequentialPrev;
            }
            else
            {
                _tail = item.SequentialPrev;
            }

            item.SequentialPrev = item.SequentialNext = null;
            _count--;
        }
    }
}
