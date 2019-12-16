using MyScore.Models.Football;
using Parser;

namespace MyScore.Pack.GamePack
{
    public class GameExecutor : AExecutor<GameDetails, Game>
    {
        public GameExecutor()
        {
            _parser = new GameDetailsParser();
            var url = new GameUrl();
            _loader = new SeleniumLoader<GameDetails>(url);
        }
    }
}
