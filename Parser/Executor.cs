﻿using Parser.Interfaces;

namespace Parser
{
    public class Executor<TResult> : IExecutor<TResult>
    {
        protected ILoader _loader;
        protected IParser<TResult> _parser;

        public Executor(ILoader loader, IParser<TResult> parser)
        {
            _loader = loader;
            _parser = parser;
        }

        public virtual TResult Run(IUrl url)
        {
            var page = _loader.GetPage(url);
            var results = _parser.Parse(page);
            return results;
        }
    }
}