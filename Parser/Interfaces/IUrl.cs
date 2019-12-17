namespace Parser.Interfaces
{
    public interface IUrl
    {
        string Base { get; }

        string Prefix { get; }

        string Get();
    }
}
