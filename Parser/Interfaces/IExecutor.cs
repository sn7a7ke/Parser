namespace Parser.Interfaces
{
    public interface IExecutor<out TResult>
    {
        TResult Run(IUrl url);
    }
}