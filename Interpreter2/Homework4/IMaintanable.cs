using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework4
{
    interface IMaintanable<T>
    {
        void AddUser(T obj);
        T GetUser(string key);
        void DeleteUser(string key);
        void UpdateUser(T obj);
    }
}
