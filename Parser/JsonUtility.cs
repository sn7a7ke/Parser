using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Parser
{
    public static class JsonUtility
    {
        public static IEnumerable<T> FromJson<T>(this IEnumerable<string> data)
        {
            return data.Select(l => JsonConvert.DeserializeObject<T>(l));
        }

        public static IEnumerable<string> ToJson<T>(this IEnumerable<T> data)
        {
            return data.Select(l => JsonConvert.SerializeObject(l));
        }
    }
}
