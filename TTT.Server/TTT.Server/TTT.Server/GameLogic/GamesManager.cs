namespace TTT.Server.GameLogic;

public class GamesManager
{
    private readonly List<Game> games = new();

    public Guid RegisterGame(string oUser, string xUser)
    {
        var game = new Game(oUser, xUser);
        games.Add(game);
        return game.Id;
    }

    public Game FindGame(string userName)
    {
        return games.FirstOrDefault(x => x.OUser == userName || x.XUser == userName);
    }

    public Game CloseGame(string userName)
    {
        var game = FindGame(userName);
        if (game == null)
        {
            return null;
        }

        games.Remove(game);
        return game;
    }

    public bool GameExists(string userName)
    {
        return games.Any(x => x.OUser == userName || x.XUser == userName);
    }

    public int GetGameCount() => games.Count;
}