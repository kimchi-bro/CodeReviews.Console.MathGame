using Spectre.Console;
namespace MathGame;

internal static class Menu
{
    internal static void ShowMainMenu()
    {
        var name = Helpers.GetPlayersName();
        var gameDifficulty = SetGameDifficulty();

        Console.Clear();
        AnsiConsole.MarkupLine($"Hello, [yellow]{name}[/]. It's {DateTime.Now.DayOfWeek}. This is your math's Game.\n");

        string[] menuChoices = [
            "Random Operations",
            "Addition",
            "Subtraction",
            "Multiplication",
            "Division",
            "Change Difficulty",
            "View previous Games",
            "Quit the program"];

        while (true)
        {
            string choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What Game you would like to play?")
                .PageSize(10)
                .AddChoices(menuChoices));

            switch (choice)
            {
                case "Random Operations":
                    Game.Run(GameType.Random, gameDifficulty);
                    break;
                case "Addition":
                    Game.Run(GameType.Addition, gameDifficulty);
                    break;
                case "Subtraction":
                    Game.Run(GameType.Subtraction, gameDifficulty);
                    break;
                case "Multiplication":
                    Game.Run(GameType.Multiplication, gameDifficulty);
                    break;
                case "Division":
                    Game.Run(GameType.Division, gameDifficulty);
                    break;
                case "Change Difficulty":
                    gameDifficulty = SetGameDifficulty();
                    break;
                case "View previous Games":
                    Helpers.ShowPlayedGames();
                    break;
                case "Quit the program":
                    AnsiConsole.MarkupLine("[yellow]\nGoodbye![/]");
                    Thread.Sleep(1000);
                    return;
            };
        }
    }

    internal static GameDifficulty SetGameDifficulty()
    {
        Console.Clear();

        var gameDifficulty = AnsiConsole.Prompt(
        new SelectionPrompt<GameDifficulty>()
            .Title("Choose Game difficulty: ")
            .PageSize(10)
            .AddChoices(Enum.GetValues<GameDifficulty>()));

        return gameDifficulty switch
        {
            GameDifficulty.Easy => GameDifficulty.Easy,
            GameDifficulty.Normal => GameDifficulty.Normal,
            GameDifficulty.Hard => GameDifficulty.Hard,
            _ => GameDifficulty.Easy,
        };
    }
}