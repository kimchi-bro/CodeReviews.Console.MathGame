namespace MathGame;

internal class Numbers
{
    internal static List<int> Generate(GameType gameType, GameDifficulty gameDifficulty)
    {
        int minNumber = gameDifficulty switch
        {
            GameDifficulty.Easy => 0,
            GameDifficulty.Normal => 1,
            GameDifficulty.Hard => 2,
            _ => 0
        };

        int maxNumber = gameDifficulty switch
        {
            GameDifficulty.Easy => 11,
            GameDifficulty.Normal => 22,
            GameDifficulty.Hard => 33,
            _ => 0
        };

        int difficultyModifier = gameDifficulty switch
        {
            GameDifficulty.Easy => 1,
            GameDifficulty.Normal => 2,
            GameDifficulty.Hard => 3,
            _ => 0
        };

        return gameType switch
        {
            GameType.Addition => GetTwoRandomNumbers(minNumber, maxNumber * difficultyModifier),
            GameType.Subtraction => GetSubtractionNumbers(minNumber, maxNumber * difficultyModifier),
            GameType.Multiplication => GetTwoRandomNumbers(minNumber + 1, maxNumber / 2),
            GameType.Division => GetDivisionNumbers(minNumber + 1, maxNumber * difficultyModifier),
            _ => []
        };
    }

    internal static List<int> GetDivisionNumbers(int minNumber, int maxNumber)
    {
        var result = GetTwoRandomNumbers(minNumber, maxNumber);
        while (result[0] % result[1] != 0) result = GetTwoRandomNumbers(minNumber, maxNumber);
        return result;
    }

    internal static List<int> GetSubtractionNumbers(int minNumber, int maxNumber)
    {
        var result = GetTwoRandomNumbers(minNumber, maxNumber);
        while (result[0] - result[1] < 0) result = GetTwoRandomNumbers(minNumber, maxNumber);
        return result;
    }

    internal static List<int> GetTwoRandomNumbers(int minValue, int maxValue)
    {
        var numbers = new List<int>
        {
            Random.Shared.Next(minValue, maxValue + 1),
            Random.Shared.Next(minValue, maxValue + 1)
        };
        return numbers;
    }
}
