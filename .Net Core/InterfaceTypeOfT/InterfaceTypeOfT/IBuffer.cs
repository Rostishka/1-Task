using System.Collections.Generic;
using System.Text;

namespace InterfaceTypeOfT
{
    interface IBuffer<T> : IEnumerable<T>
    {
        bool IsEmpty { get; }
        T Read();
        void Write(T value);

       // IEnumerable<TOutput> AsEnumarableOf<TOutput>();
    }
}