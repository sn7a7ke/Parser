using Parser.Interfaces;

namespace MyScore
{
    public class BaseUrl : IUrl
    {
        public virtual string Base { get; protected set; } = "https://www.myscore.com.ua/";

        public virtual string Prefix { get; protected set; } = "";

        public string Get()
        {
            var url = string.Format(Compile(), Organize());
            return url;
        }

        protected virtual string Compile()
        {
            return $"{Base}{Prefix}";
        }

        protected virtual string[] Organize()
        {
            return new string[] { };
        }
    }
}
