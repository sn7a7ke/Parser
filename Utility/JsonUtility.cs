using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Utility
{
    public static class JsonUtility
    {
        public static IEnumerable<T> FromJson<T>(this IEnumerable<string> data)
        {
            return data.Select(d => JsonConvert.DeserializeObject<T>(d));
        }

        public static IEnumerable<string> ToJson<T>(this IEnumerable<T> data)
        {
            return data.Select(d => JsonConvert.SerializeObject(d));
        }
    }
}
