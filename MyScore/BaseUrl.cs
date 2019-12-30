using Parser.Interfaces;
using System.Linq;

namespace MyScore
{
    public class BaseUrl : IUrl
    {
        public virtual string Base { get; protected set; } = "https://www.myscore.com.ua/";

        public string Get()
        {
            var cc = Chunks();
            var chunks = cc.TakeWhile(c => !string.IsNullOrEmpty(c));
            var prefix = string.Join("/", chunks);
            return $"{Base}{prefix}";
        }

        public virtual string[] Chunks() => new string[] { };
    }
}
