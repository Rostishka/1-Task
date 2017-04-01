using System;

namespace Lazar
{
    abstract class BaseManager<T> : IStoragingManager<T>
    {
        protected BaseManager(string fileName)
        {
            FileName = fileName;
        }

        public string FileName { get; }

        public abstract void SaveEntity(T entity);
        public abstract T LoadEntity();
        public abstract void UpdateEntity(T entity);
        public abstract void DeleteEntity(T entity);

    }
}