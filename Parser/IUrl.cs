using System.Collections.Generic;

namespace Parser
{
    public interface IUrl<T>
    {
        string Base { get; }

        string Prefix { get; }

        string Get(T details);
    }
}
