using Spectre.Console;
namespace MathGame;

internal class Game
{
    public DateTime Date { get; set; }
    public int Score { get; set; }
    public GameType Type { get; set; }
    public GameDifficulty Difficulty { get; set; }
    public TimeSpan TimeSpent { get; set; }

    public static void Run(GameType gameType, GameDifficulty gameDifficulty)
    {
        AnsiConsole.MarkupLine($"[yellow]{gameType} Game selected, difficulty level: {gameDifficulty}![/]\n");

        var score = 0;
        var startTime = DateTime.Now;
        bool isRandom = gameType == GameType.Random;

        for (int i = 0; i < 5; i++)
        {
            GameType currentGameType = isRandom ? (GameType)Random.Shared.Next(Enum.GetValues<GameType>().Length - 1) : gameType;

            var numbers = Numbers.Generate(currentGameType, gameDifficulty);

            Helpers.ShowQuestionMessage(currentGameType, numbers);

            if (Helpers.CheckPlayersAnswer(currentGameType, numbers)) score++;

            Console.WriteLine();
        }

        var endTime = DateTime.Now;
        TimeSpan timeSpent = endTime - startTime;

        var result = new Helpers.GameResult(gameType, gameDifficulty, score, timeSpent);
        Helpers.ShowGameResult(result);
    }
}
