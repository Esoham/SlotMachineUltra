public static class SlotMachineUI
{
    public static BetChoice GetPlayerChoice()
    {
        while (true)
        {
            Console.WriteLine("Choose your bet:");
            foreach (BetChoice option in Enum.GetValues(typeof(BetChoice)))
            {
                Console.WriteLine($"{(int)option}. {option.ToString().Replace('_', ' ')}");
            }

            if (int.TryParse(Console.ReadLine(), out int selectedChoice) && Enum.IsDefined(typeof(BetChoice), selectedChoice))
            {
                return (BetChoice)selectedChoice;
            }

            Console.WriteLine("Invalid choice. Please try again.");
        }
    }

    public static int GetWagerPerLine(int maxBetPerLine)
    {
        while (true)
        {
            Console.Write($"Enter your wager per line (max ${maxBetPerLine}): ");
            if (int.TryParse(Console.ReadLine(), out int wagerPerLine) && wagerPerLine >= 1 && wagerPerLine <= maxBetPerLine)
            {
                return wagerPerLine;
            }

            Console.WriteLine("Invalid wager. Please try again.");
        }
    }

    public static void DisplayGrid(int[,] grid)
    {
        Console.WriteLine("Slot Machine Outcome:");
        for (int i = 0; i < Constants.GRID_SIZE; i++)
        {
            for (int j = 0; j < Constants.GRID_SIZE; j++)
            {
                Console.Write($"{grid[i, j]}{(j < Constants.GRID_SIZE - 1 ? " | " : "")}");
            }
            Console.WriteLine(i < Constants.GRID_SIZE - 1 ? "\n---+---+---" : "\n");
        }
    }

    public static void DisplayRoundResult(int winnings, int wager)
    {
        if (winnings > 0)
        {
            Console.WriteLine($"You won ${winnings}! Your total wager was ${wager}.");
        }
        else
        {
            Console.WriteLine($"You did not win anything. Your total wager was ${wager}.");
        }
        Console.WriteLine();
    }

    public static int GetLinesToBet(BetChoice choice)
    {
        switch (choice)
        {
            case BetChoice.CenterHorizontalLine: return 1;
            case BetChoice.AllHorizontalLines:
            case BetChoice.AllVerticalLines: return Constants.GRID_SIZE;
            case BetChoice.BothDiagonals: return 2;
            case BetChoice.AllLines: return Constants.GRID_SIZE * 2 + 2;
            default: throw new ArgumentOutOfRangeException(nameof(choice), "Invalid betting choice");
        }
    }
}
