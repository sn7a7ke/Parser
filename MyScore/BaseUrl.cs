using Parser.Interfaces;

namespace MyScore
{
    public abstract class BaseUrl<T> : IUrl<T>
    {
        protected BaseUrl()
        {
            Template = $"{Base}{Prefix}";
        }

        public string Template { get; private set; }

        public string Base { get; private set; } = "https://www.myscore.com.ua/";

        public abstract string Prefix { get; protected set; }

        public abstract string Get(T details);
    }
}
