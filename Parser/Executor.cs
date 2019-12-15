using System;
using System.Collections.Generic;

namespace Parser
{
    public class Executor<TDetails, TResult>
    {
        private readonly IParser<TResult> _parser;
        private readonly ILoader<TDetails> _loader;

        public Executor(IParser<TResult> parser, ILoader<TDetails> loader)
        {
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            _loader = loader ?? throw new ArgumentNullException(nameof(loader));
        }

        public event Action<object, TResult> ReceivedChunk;
        public event Action<object> Done;

        public void Run(List<TDetails> detailsList)
        {
            var _detailsList = detailsList ?? throw new ArgumentNullException(nameof(detailsList));

            foreach (var details in _detailsList)
            {
                var results = Run(details);
                ReceivedChunk?.Invoke(this, results);
            }
            Done?.Invoke(this);
        }
        public TResult Run(TDetails details)
        {
            var page = _loader.GetPage(details);
            var results = _parser.Parse(page);
            return results;
        }
    }
}
