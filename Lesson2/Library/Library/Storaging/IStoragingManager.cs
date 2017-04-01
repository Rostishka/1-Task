using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazar
{
    interface IStoragingManager<T>
    {
        string FileName { get; }

        void SaveEntity(T entity);
        T LoadEntity();
        void UpdateEntity(T entity);
        void DeleteEntity(T entity);

    }
}
