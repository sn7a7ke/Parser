using Parser.Interfaces;

namespace Parser
{
    public abstract class AExecutor<TDetails, TResult> : IExecutor<TDetails, TResult>
    {
        protected IParser<TResult> _parser;
        protected ILoader<TDetails> _loader;
        
        public virtual TResult Run(TDetails details)
        {
            var page = _loader.GetPage(details);
            var results = _parser.Parse(page);
            return results;
        }
    }
}
