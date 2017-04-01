using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Homework3
{
    class JsonManager<T>
    {
        public static string SereilizeJson(string filename, T a)
        {
            try
            {
                using (StreamWriter file = File.CreateText(filename))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                    serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                    serializer.Serialize(file, a);
                }
                return "Serializind data into JSON was successful";
            }
            catch (Exception)
            {
                return "Can't Serialize yor data into JSON";
            }
        }

        public static object DeserelizeJson(string filename)
        {
            using (StreamReader file = File.OpenText(filename))
            {
                JsonSerializer serializer = new JsonSerializer();
                T obj = (T)serializer.Deserialize(file, typeof(T));
                return obj;
            }
        }
    }
}
