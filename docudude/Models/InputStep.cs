using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace docudude.Models
{
    public class InputStep
    {
        public string Type { get; set; }
        public JToken Data { get; set; }

        public T GetData<T>()
        {
            return Data.ToObject<T>();
        }

        public object GetData(Type T)
        {
            return Data.ToObject(T);
        }
    }
}
