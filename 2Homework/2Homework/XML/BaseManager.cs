using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2Homework
{
    public abstract class BaseManager<T> : IStorating<T>
    {
        public string FileName { get; }

        protected BaseManager(string _fileName)
        {
            FileName = _fileName;
        }

        public abstract void DeleteEntity(T entity);
        public abstract void EditEntity(T entity);
        public abstract T LoadEntity();
        public abstract void SaveEntity(T entity);
    }
}
