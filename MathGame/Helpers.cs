using Spectre.Console;
namespace MathGame;

internal class Helpers
{
    internal static List<Game> _games = [];

    internal static string GetPlayersName()
    {
        AnsiConsole.Markup("[bold yellow]Enter your name: [/]");
        return GetLettersInput();
    }

    internal static void ShowQuestionMessage(GameType gameType, List<int> numbers)
    {
        string operation = gameType switch
        {
            GameType.Addition => "+",
            GameType.Subtraction => "-",
            GameType.Multiplication => "*",
            GameType.Division => "/",
            _ => ""
        };

        AnsiConsole.Markup($"[bold yellow]{numbers[0]} {operation} {numbers[1]} = [/]");
    }

    internal static void ShowPlayedGames()
    {
        Console.Clear();

        if (_games.Count > 0)
        {
            AnsiConsole.MarkupLine("[bold yellow]Games played:\n[/]");

            var table = new Table();

            table.AddColumn("Date");
            table.AddColumn(new TableColumn("Game Type"));
            table.AddColumn(new TableColumn("Difficulty"));
            table.AddColumn(new TableColumn("Score"));
            table.AddColumn(new TableColumn("Time Spent"));

            foreach (var game in _games)
            {
                table.AddRow(
                    $"{game.Date:d}",
                    $"{game.Type}",
                    $"{game.Difficulty}",
                    $"{game.Score}/5",
                    $"{game.TimeSpent.TotalSeconds:0} sec.");
            }

            AnsiConsole.Write(table);
        }
        else AnsiConsole.MarkupLine("[red3_1]You haven't played any game yet![/]");

        PressAnyKeyToExitToMenu();
    }

    internal static bool CheckPlayersAnswer(GameType gameType, List<int> numbers)
    {
        var answer = GetDigitsInput();

        var result = gameType switch
        {
            GameType.Addition => numbers[0] + numbers[1],
            GameType.Subtraction => numbers[0] - numbers[1],
            GameType.Multiplication => numbers[0] * numbers[1],
            GameType.Division => numbers[0] / numbers[1],
            _ => 0
        };

        if (result == answer)
        {
            AnsiConsole.MarkupLine("[bold seagreen1]Your answer is correct![/]");
            return true;
        }
        else
        {
            AnsiConsole.MarkupLine($"[red3_1]Wrong answer, it is {result}.[/]");
            return false;
        }
    }

    public record struct GameResult(GameType GameType, GameDifficulty GameDifficulty, int GameScore, TimeSpan TimeSpent);

    internal static void ShowGameResult(GameResult result)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn($"You have {result.GameScore} correct answers out of 5 questions, time spent: {result.TimeSpent.TotalSeconds:0} seconds!");
        AnsiConsole.Write(table);

        _games.Add(new Game
        {
            Date = DateTime.Now,
            Type = result.GameType,
            Score = result.GameScore,
            Difficulty = result.GameDifficulty,
            TimeSpent = result.TimeSpent
        });

        PressAnyKeyToExitToMenu();
    }

    internal static void PressAnyKeyToExitToMenu()
    {
        AnsiConsole.Status()
            .Start("[yellow]Press any key to exit to Game menu: [/]", ctx =>
            {
                ctx.Spinner(Spinner.Known.Default);
                ctx.SpinnerStyle(Style.Parse("yellow"));
                Thread.Sleep(500);
                Console.ReadKey(true);
            });

        Console.Clear();
    }

    public static int GetDigitsInput()
    {
        string? input = Console.ReadLine() ?? "";
        while (true)
        {
            if (!string.IsNullOrEmpty(input) && int.TryParse(input.Trim(), out int result)) return result;
            else
            {
                AnsiConsole.Markup("[red]Invalid input, only digits allowed. Retry: [/]");
                input = Console.ReadLine() ?? "";
            }
        }
    }

    public static string GetLettersInput()
    {
        string? input = Console.ReadLine() ?? "";
        while (true)
        {
            if (!string.IsNullOrEmpty(input) && input.Trim().All(char.IsLetter)) return input.Trim();
            else
            {
                AnsiConsole.Markup("[red]Invalid input, only leters allowed. Retry: [/]");
                input = Console.ReadLine() ?? "";
            }
        }
    }
}
