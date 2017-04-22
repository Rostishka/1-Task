using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceTypeOfT
{

    class CircularBuffer<T> : Buffer<T>
    {
        public T[] buffer;
        private int tail;
        private int head;

        public CircularBuffer() : this(capacity: 3)
        {

        }

        public CircularBuffer(int capacity)
        {
            buffer = new T[capacity + 1];
            tail = 0;
            head = 0;
        }

        public int Capacity
        {
           get { return buffer.Length; }
        }

        public bool IsFool
        {
            get { return (tail + 1) % buffer.Length == head; }
        }
    }
}
