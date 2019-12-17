using MyScore.Models.Football;
using Parser;
using Parser.Interfaces;

namespace MyScore.Pack.GamePack
{
    public class GameExecutor : Executor<Game>
    {
        public GameExecutor(ILoader loader) : base(loader, new GameParser())
        {
        }
    }
}
