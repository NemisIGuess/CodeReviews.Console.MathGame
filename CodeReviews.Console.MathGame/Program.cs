

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
        5, "Random"
    },
    {
        6, "Game History"
    },
    {
        7, "Exit"
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

Console.WriteLine("Welcome to MathGame!");
var (game, difficulty) = GameChoiceMenu();
InitializeGame(difficulty, game);

return;

(string Game, int Difficulty ) GameChoiceMenu()
{
    (string Game, int Difficulty) userChoice;
    
    Console.WriteLine("What would you like to play?");
    WriteOptionsDictionary(menuOptions);
    var userSelection = GetUserInput();
    LoopWhileWrongInput(menuOptions, ref userSelection);

    if (menuOptions[userSelection] == "Exit")
    {
        Console.WriteLine("Bye!");
        ExitConsole();
    }
    
    userChoice.Game = menuOptions[userSelection];
    
    LineBreak();
    
    Console.WriteLine($"Playing a game of {userChoice.Game}");
    Console.WriteLine("What difficulty would you like to play?");
    WriteOptionsDictionary(difficulties);
    var chosenDifficulty = GetUserInput();
    LoopWhileWrongInput(difficulties, ref chosenDifficulty);
    
    userChoice.Difficulty = chosenDifficulty;
    
    return userChoice;
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
            AdditionGame(upperLimit);
            break;
        case "Subtraction":
            SubtractionGame(upperLimit);
            break;
        case "Multiplication":
            MultiplicationGame(upperLimit);
            break;
        case "Division":
            DivisionGame(upperLimit);
            break;
        case "Random":
            RandomGame(upperLimit);
            break;
    }
        
}

void AdditionGame(int upperLimit)
{
    var firstNumber = GetRandomNumber(upperLimit);
    var secondNumber = GetRandomNumber(upperLimit);
    int count = 0;

    var timer = new Stopwatch();
    for (var i = 0; i < 5; i++)
    {
        LineBreak();
        Console.WriteLine($"How much is {firstNumber} + {secondNumber}?");
        int.TryParse(Console.ReadLine(), out var userInput);
        
        if (userInput == firstNumber +  secondNumber)
        {
            count++;
        }
        firstNumber = GetRandomNumber(upperLimit);
        secondNumber = GetRandomNumber(upperLimit);
    }

    var data = new PlayedGame
    {
        Type = "Addition",
        ScorePercentage = (count / 10) * 100, // esto esta mal
        Time = TimeSpan.Parse(timer.ElapsedMilliseconds.ToString()), // esto tambien
    };

    Console.WriteLine($"Game finished in {data.Time.ToString()}, {data.ScorePercentage}"); // esto no mostrarlo aqui
    gamesHistoryData.Add(data);

}

void SubtractionGame(int upperLimit)
{
    
}

void MultiplicationGame(int upperLimit)
{
    
}

void DivisionGame(int upperLimit)
{
    
}

void RandomGame(int upperLimit)
{
    var random = new Random();
    var randomChoice = random.Next(4);
    switch (randomChoice)
    {
        case 0:
            AdditionGame(upperLimit);
            break;
        case 1:
            SubtractionGame(upperLimit);
            break;
        case 2:
            MultiplicationGame(upperLimit);
            break;
        case 3:
            DivisionGame(upperLimit);
            break;
    }
}

int GetRandomNumber(int upperLimit)
{
    var random = new Random();
    return random.Next(1, upperLimit);
}





public class PlayedGame
{
    public string Type { get; set; } = string.Empty;
    public int ScorePercentage { get; set; }
    public TimeSpan Time { get; set; }
}