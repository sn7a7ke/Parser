namespace Parser.Interfaces
{
    public interface IExecutor<in TDetails, out TResult>
    {
        TResult Run(TDetails details);
    }
}