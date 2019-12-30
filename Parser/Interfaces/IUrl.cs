namespace Parser.Interfaces
{
    public interface IUrl
    {
        string Base { get; }

        string Get();

        string[] Chunks();
    }
}
