namespace Parser.Interfaces
{
    public interface IUrl<in T>
    {
        string Base { get; }

        string Prefix { get; }

        string Get(T details);
    }
}
