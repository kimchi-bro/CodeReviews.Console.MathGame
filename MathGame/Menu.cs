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

        while (true)
        {
            var gameSelected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("What Game you would like to play?")
                .PageSize(10)
                .AddChoices([
                    "Random Operations",
                    "Addition",
                    "Substraction",
                    "Mulitiplication",
                    "Division",
                    "Change Difficulty",
                    "View previous Games",
                    "Quit the program"
                ]));

            if (gameSelected == "Random Operations")
            {
                Game.Run(GameType.Random, gameDifficulty);
                continue;
            }
            if (gameSelected == "Addition")
            {
                Game.Run(GameType.Addition, gameDifficulty);
                continue;
            }
            else if (gameSelected == "Substraction")
            {
                Game.Run(GameType.Subtraction, gameDifficulty);
                continue;
            }
            else if (gameSelected == "Mulitiplication")
            {
                Game.Run(GameType.Multiplication, gameDifficulty);
                continue;
            }
            else if (gameSelected == "Division")
            {
                Game.Run(GameType.Division, gameDifficulty);
                continue;
            }
            else if (gameSelected == "Change Difficulty")
            {
                gameDifficulty = SetGameDifficulty();
                continue;
            }
            else if (gameSelected == "View previous Games")
            {
                Helpers.ShowPlayedGames();
                continue;
            }
            else if (gameSelected == "Quit the program")
            {
                AnsiConsole.MarkupLine("[yellow]\nGoodbye![/]");
                Thread.Sleep(1000);
                return;
            }
        }
    }

    internal static GameDifficulty SetGameDifficulty()
    {
        Console.Clear();

        var gameDifficulty = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("Choose Game difficulty: ")
            .PageSize(10)
            .AddChoices([
                "Easy",
                "Normal",
                "Hard"
            ]));

        if (gameDifficulty == "Easy") return GameDifficulty.Easy;
        else if (gameDifficulty == "Normal") return GameDifficulty.Normal;
        else if (gameDifficulty == "Hard") return GameDifficulty.Hard;
        else return GameDifficulty.Easy;
    }
}