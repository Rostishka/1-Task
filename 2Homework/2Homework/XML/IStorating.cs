using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2Homework
{
    interface IStorating<T>
    {
        string FileName { get; }

        void SaveEntity(T entity);
        void DeleteEntity(T entity);
        void EditEntity(T entity);
        T LoadEntity();
    }
}
