

Console.Title = "MathGame";
var menuOptions = new Dictionary<string, string>
{
    {
        "1", "Addition"
    },
    {
        "2", "Subtraction"
    },
    {
        "3", "Multiplication"
    },
    {
        "4", "Division"
    },
    {
        "5", "Random"
    },
    {
        "6", "Game History"
    },
    {
        "7", "Exit"
    }
};

var difficulties = new Dictionary<string, string>
{
    {
        "1", "Easy"
    },
    {
        "2", "Normal"
    },
    {
        "3", "Hard"
    }
};

var gamesHistoryData = new PlayedGame[10];

Console.WriteLine("Welcome to MathGame!");
var (game, difficulty) = GameChoiceMenu();


return;

(string Game, string Difficulty ) GameChoiceMenu()
{
    (string Game, string Difficulty) result;
    Console.WriteLine("What would you like to play?");
    WriteOptionsDictionary(menuOptions);
    
    var userSelection = GetUserInput();

    while (!menuOptions.ContainsKey(userSelection))
    {
        LineBreak();
        Console.WriteLine("Unrecognised selection. Please try again.");
        userSelection = GetUserInput();
    }

    if (menuOptions[userSelection] == "Exit")
    {
        Console.WriteLine("Bye!");
        ExitConsole();
    }
    
    result.Game = menuOptions[userSelection];
    
    LineBreak();
    Console.WriteLine($"Playing a game of {result.Game}");
    Console.WriteLine("What difficulty would you like to play?");
    WriteOptionsDictionary(difficulties);
    
    var userDifficulty = GetUserInput();
    result.Difficulty = difficulties[userDifficulty];
    
    return result;
}

void LineBreak()
{
    Console.WriteLine();
}

void ExitConsole()
{
    Environment.Exit(0);
}

void WriteOptionsDictionary(Dictionary<string, string> dictionary)
{
    foreach (var keyValuePair in dictionary)
    {
        Console.WriteLine($"\t{keyValuePair.Key} - {keyValuePair.Value}");
    }
    Console.WriteLine("Press the corresponding key to continue...");
}

string GetUserInput()
{
    return Console.ReadKey().KeyChar.ToString();
}

public class PlayedGame
{
    public string GameName { get; set; } = string.Empty;
    public int ScorePercentage { get; set; }
    public int Time { get; set; }
}