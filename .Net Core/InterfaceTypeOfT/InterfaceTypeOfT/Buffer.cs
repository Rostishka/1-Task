using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace InterfaceTypeOfT
{
    class Buffer<T> : IBuffer<T>, IEnumerable<T>
    {
        Queue<T> _queue = new Queue<T>();

        public bool IsEmpty
        {
            get { return _queue.Count == 0; }
        }

        //public IEnumerable<TOutput> AsEnumarableOf<TOutput>()
        //{
            
        //}

        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        public T Read()
        {
            return _queue.Dequeue();
        }

        public void Write(T value)
        {
            _queue.Enqueue(value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
