using System.Diagnostics;

Console.Title = "MathGame";
var menuOptions = new Dictionary<int, string>
{
    {
        1, "Addition"
    },
    {
        2, "Subtraction"
    },
    {
        3, "Multiplication"
    },
    {
        4, "Division"
    },
    {
        5, "Game History"
    },
    {
        6, "Exit"
    }
};

var difficulties = new Dictionary<int, string>
{
    {
        1, "Easy"
    },
    {
        2, "Normal"
    },
    {
        3, "Hard"
    }
};

var gamesHistoryData = new List<PlayedGame>();
const int questionsCount = 10;

Console.WriteLine("Welcome to MathGame!");
MainMenu();

return;

void MainMenu()
{
    Console.WriteLine("What would you like to play?");
    WriteOptionsDictionary(menuOptions);
    var userSelection = GetUserInput();
    LoopWhileWrongInput(menuOptions, ref userSelection);

    if (menuOptions[userSelection] == "Game History")
    {
        Console.Clear();
        Console.WriteLine($"Played {gamesHistoryData.Count} games in total.");
        foreach (var gameHistoryData in gamesHistoryData)
        {
            Console.WriteLine(
                $"Played a game of {gameHistoryData.Type} that ended in {gameHistoryData.TimeInSeconds} seconds with {gameHistoryData.ScorePercentage}% question answered correctly.");
        }

        MainMenu();
    }

    if (menuOptions[userSelection] == "Exit")
    {
        Console.WriteLine("Bye!");
        ExitConsole();
    }

    var selectedGame = menuOptions[userSelection];

    LineBreak();

    Console.WriteLine("What difficulty would you like to play?");
    WriteOptionsDictionary(difficulties);
    var chosenDifficulty = GetUserInput();
    LoopWhileWrongInput(difficulties, ref chosenDifficulty);
    var selectedDifficulty = chosenDifficulty;

    LineBreak();
    Console.WriteLine($"Playing a game of {selectedGame} in {difficulties[chosenDifficulty]}");

    InitializeGame(selectedDifficulty, selectedGame);
}

void WriteOptionsDictionary(Dictionary<int, string> dictionary)
{
    foreach (var keyValuePair in dictionary)
    {
        Console.WriteLine($"\t{keyValuePair.Key} - {keyValuePair.Value}");
    }

    Console.WriteLine("Press the corresponding key to continue...");
}

int GetUserInput()
{
    return int.TryParse(Console.ReadKey().KeyChar.ToString(), out var choice) ? choice : 0;
}

void LoopWhileWrongInput(Dictionary<int, string> dictionary, ref int chosenOption)
{
    while (!dictionary.ContainsKey(chosenOption))
    {
        LineBreak();
        Console.WriteLine("Unrecognised selection. Please try again.");
        chosenOption = GetUserInput();
    }
}

void ExitConsole()
{
    Environment.Exit(0);
}

void LineBreak()
{
    Console.WriteLine();
}

void InitializeGame(int chosenDifficulty, string gameType)
{
    var upperLimit = (chosenDifficulty * chosenDifficulty) * 100;
    switch (gameType)
    {
        case "Addition":
            PlayMathGame(upperLimit, gameType, (a, b) => a + b);
            break;
        case "Subtraction":
            PlayMathGame(upperLimit, gameType, (a, b) => a - b);
            break;
        case "Multiplication":
            PlayMathGame(upperLimit, gameType, (a, b) => a * b);
            break;
        case "Division":
            PlayMathGame(upperLimit, gameType, (a, b) => a / b);
            break;
    }
}

void PlayMathGame(
    int upperLimit,
    string operationName,
    Func<int, int, int> operation)
{
    var firstNumber = GetRandomNumber(upperLimit);
    var secondNumber = GetRandomNumber(upperLimit);

    if (operationName == "Division")
        ValidateDivisionNumber(upperLimit, ref firstNumber, ref secondNumber);

    var count = 0;
    var timer = new Stopwatch();
    timer.Start();

    for (var i = 0; i < questionsCount; i++)
    {
        LineBreak();
        Console.WriteLine($"How much is {firstNumber} {GetOperatorSymbol(operationName)} {secondNumber}?");
        int.TryParse(Console.ReadLine(), out var userInput);

        if (userInput == operation(firstNumber, secondNumber))
        {
            count++;
        }

        firstNumber = GetRandomNumber(upperLimit);
        secondNumber = GetRandomNumber(upperLimit);

        if (operationName == "Division")
            ValidateDivisionNumber(upperLimit, ref firstNumber, ref secondNumber);
    }

    timer.Stop();

    var gameData = new PlayedGame
    {
        Type = operationName,
        ScorePercentage = (int)(count / (double)questionsCount * 100),
        TimeInSeconds = (int)timer.Elapsed.TotalSeconds,
    };

    gamesHistoryData.Add(gameData);
    MainMenu();
}

string GetOperatorSymbol(string operationName)
{
    switch (operationName)
    {
        case "Addition":
            return "+";
        case "Subtraction":
            return "-";
        case "Multiplication":
            return "*";
        case "Division":
            return "/";
        default:
            return "";
    }
}

void ValidateDivisionNumber(int upperLimit, ref int dividend, ref int divisor)
{
    while (divisor == 0 || dividend % divisor != 0)
    {
        dividend = GetRandomNumber(upperLimit);
        divisor = GetRandomNumber(upperLimit);
    }
}

int GetRandomNumber(int upperLimit)
{
    var random = new Random();
    return random.Next(1, upperLimit);
}

internal class PlayedGame
{
    public string Type { get; init; } = string.Empty;
    public int ScorePercentage { get; init; }
    public int TimeInSeconds { get; init; }
}