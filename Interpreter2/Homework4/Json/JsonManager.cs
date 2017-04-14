using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Homework4
{
    public class JsonManager
    {
        private JsonSerializer serializer;

        public JsonManager()
        {
            serializer = new JsonSerializer();
        }

        public string SereilizeJson<T>(string filename, T a)
        {
            try
            {
                using (StreamWriter file = File.CreateText(filename))
                {
                    serializer.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                    serializer.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
                    serializer.Serialize(file, a);
                }
                return "Serializind data into JSON was successful";
            }
            catch (Exception ex)
            {
                return "Can't Serialize yor data into JSON";
                Console.WriteLine(ex.Message);
            }
        }

        public T DeserelizeJson<T>(string filename)
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
