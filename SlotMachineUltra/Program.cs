using System;
using System.Collections.Generic;

class SlotMachineGame
{
    private const int GRID_SIZE = 3;
    private const int STARTING_PLAYER_MONEY = 100;
    private static readonly Random random = new Random();

    private enum BetChoice
    {
        CenterHorizontalLine = 1,
        AllHorizontalLines,
        AllVerticalLines,
        BothDiagonals,
        AllLines
    }

    private static readonly Dictionary<int, int> PAYOUTS = new Dictionary<int, int>
    {
        {1, 2}, {2, 3}, {3, 4}, {4, 5}, {5, 10}
    };

    public static void Main()
    {
        int playerMoney = STARTING_PLAYER_MONEY;
        Console.WriteLine("Welcome to the Slot Machine Game!");

        while (playerMoney > 0)
        {
            Console.WriteLine($"Current Money: ${playerMoney}");
            BetChoice betChoice = GetPlayerChoice();
            int linesToBet = GetLinesToBet(betChoice);
            int maxBetPerLine = playerMoney / (linesToBet * 5);
            int wagerPerLine = GetWagerPerLine(maxBetPerLine);
            int totalWager = wagerPerLine * linesToBet;

            if (totalWager > playerMoney)
            {
                Console.WriteLine("You do not have enough money for that wager.");
                continue;
            }

            playerMoney -= totalWager;
            int[,] grid = GenerateSlotOutcomes();
            DisplayGrid(grid);
            int totalWinnings = CalculateWinnings(grid, betChoice, wagerPerLine);
            playerMoney += totalWinnings;

            DisplayRoundResult(totalWinnings, totalWager);
        }

        Console.WriteLine("Game Over! You've run out of money.");
    }

    private static BetChoice GetPlayerChoice()
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

    private static int GetWagerPerLine(int maxBetPerLine)
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

    private static int[,] GenerateSlotOutcomes()
    {
        int[,] grid = new int[GRID_SIZE, GRID_SIZE];
        for (int i = 0; i < GRID_SIZE; i++)
        {
            for (int j = 0; j < GRID_SIZE; j++)
            {
                grid[i, j] = random.Next(1, 6);
            }
        }
        return grid;
    }

    private static void DisplayGrid(int[,] grid)
    {
        Console.WriteLine("Slot Machine Outcome:");
        for (int i = 0; i < GRID_SIZE; i++)
        {
            for (int j = 0; j < GRID_SIZE; j++)
            {
                Console.Write($"{grid[i, j]}{(j < GRID_SIZE - 1 ? " | " : "")}");
            }
            Console.WriteLine(i < GRID_SIZE - 1 ? "\n---+---+---" : "\n");
        }
    }

    private static int CalculateWinnings(int[,] grid, BetChoice choice, int wagerPerLine)
    {
        int winnings = 0;
        // Calculation logic based on BetChoice
        return winnings;
    }

    private static void DisplayRoundResult(int winnings, int wager)
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

    private static int GetLinesToBet(BetChoice choice)
    {
        switch (choice)
        {
            case BetChoice.CenterHorizontalLine: return 1;
            case BetChoice.AllHorizontalLines:
            case BetChoice.AllVerticalLines: return GRID_SIZE;
            case BetChoice.BothDiagonals: return 2;
            case BetChoice.AllLines: return GRID_SIZE * 2 + 2;
            default: throw new ArgumentOutOfRangeException(nameof(choice), "Invalid betting choice");
        }
    }
}