using System;
using System.Collections.Generic;

class SlotMachineGame
{
    private const int GRID_SIZE = 3;
    private const int MIN_SYMBOL_VALUE = 1;
    private const int MAX_SYMBOL_VALUE = 5;
    private static readonly Random RANDOM = new Random();
    private const int STARTING_PLAYER_MONEY = 100;
    private const int MAX_BET_MULTIPLIER = 5;

    // Constants for the number of lines bet per mode
    private const int LINES_CENTER_HORIZONTAL = 1;
    private const int LINES_HORIZONTAL = GRID_SIZE;
    private const int LINES_VERTICAL = GRID_SIZE;
    private const int LINES_DIAGONALS = 2;
    private const int LINES_ALL = GRID_SIZE * 2 + 2; // Grid size related calculations

    private enum BetChoice
    {
        CenterHorizontalLine = 1,
        AllHorizontalLines,
        AllVerticalLines,
        BothDiagonals,
        AllLines
    }

    private static void Main()
    {
        Console.WriteLine("Welcome to the Slot Machine Game!");
        int playerMoney = STARTING_PLAYER_MONEY;

        while (playerMoney > 0)
        {
            ShowPlayerMoney(playerMoney);
            BetChoice choice = GetPlayerChoice();
            int linesToBet = GetLinesToBet(choice);
            int maxBetPerLine = playerMoney / (linesToBet * MAX_BET_MULTIPLIER);
            int wagerPerLine = GetWagerPerLine(maxBetPerLine);
            int totalWager = wagerPerLine * linesToBet;

            if (totalWager > playerMoney)
            {
                Console.WriteLine("You do not have enough money for that wager.");
                continue;
            }

            playerMoney -= totalWager;
            string[,] grid = GenerateSlotOutcomes();
            DisplayGrid(grid);
            int totalWinnings = CalculateWinnings(grid, choice, wagerPerLine);
            playerMoney += totalWinnings;
            ShowRoundResult(totalWinnings, totalWager);
        }

        Console.WriteLine("Game Over! You've run out of money.");
    }

    private static void ShowPlayerMoney(int money)
    {
        Console.WriteLine($"Current Money: ${money}");
    }

    private static BetChoice GetPlayerChoice()
    {
        Console.WriteLine("Choose your bet:");
        foreach (BetChoice choice in Enum.GetValues(typeof(BetChoice)))
        {
            Console.WriteLine($"{(int)choice}. {choice} ({GetLinesToBet(choice)} lines)");
        }

        int choiceInput;
        while (!int.TryParse(Console.ReadLine(), out choiceInput) || !Enum.IsDefined(typeof(BetChoice), choiceInput))
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }

        return (BetChoice)choiceInput;
    }

    private static int GetLinesToBet(BetChoice choice)
    {
        switch (choice)
        {
            case BetChoice.CenterHorizontalLine:
                return LINES_CENTER_HORIZONTAL;
            case BetChoice.AllHorizontalLines:
                return LINES_HORIZONTAL;
            case BetChoice.AllVerticalLines:
                return LINES_VERTICAL;
            case BetChoice.BothDiagonals:
                return LINES_DIAGONALS;
            case BetChoice.AllLines:
                return LINES_ALL;
            default:
                return 0;
        }
    }

    private static int GetWagerPerLine(int maxBetPerLine)
    {
        Console.Write($"Enter your wager per line (max ${maxBetPerLine}): ");
        int wagerPerLine;
        while (!int.TryParse(Console.ReadLine(), out wagerPerLine) || wagerPerLine < 1 || wagerPerLine > maxBetPerLine)
        {
            Console.WriteLine("Invalid wager. Please try again.");
        }
        return wagerPerLine;
    }

    private static string[,] GenerateSlotOutcomes()
    {
        string[,] grid = new string[GRID_SIZE, GRID_SIZE];
        for (int i = 0; i < GRID_SIZE; i++)
        {
            for (int j = 0; j < GRID_SIZE; j++)
            {
                grid[i, j] = RANDOM.Next(MIN_SYMBOL_VALUE, MAX_SYMBOL_VALUE + 1).ToString();
            }
        }
        return grid;
    }

    private static void DisplayGrid(string[,] grid)
    {
        Console.WriteLine("Slot Machine Outcome:");
        for (int i = 0; i < GRID_SIZE; i++)
        {
            Console.WriteLine(string.Join(" | ", grid[i, 0], grid[i, 1], grid[i, 2]));
            if (i < GRID_SIZE - 1)
                Console.WriteLine("---+---+---");
        }
        Console.WriteLine();
    }

    private static int CalculateWinnings(string[,] grid, BetChoice choice, int wagerPerLine)
    {
        int winnings = 0;
        var payouts = new Dictionary<string, int>
        {
            {"1", 2}, {"2", 3}, {"3", 4}, {"4", 5}, {"5", 10}
        };

        switch (choice)
        {
            case BetChoice.CenterHorizontalLine:
                winnings += CheckLine(grid, 1, wagerPerLine, payouts);
                break;
            case BetChoice.AllHorizontalLines:
                for (int i = 0; i < GRID_SIZE; i++)
                    winnings += CheckLine(grid, i, wagerPerLine, payouts);
                break;
            case BetChoice.AllVerticalLines:
                for (int j = 0; j < GRID_SIZE; j++)
                    winnings += CheckColumn(grid, j, wagerPerLine, payouts);
                break;
            case BetChoice.BothDiagonals:
                winnings += CheckDiagonal(grid, true, wagerPerLine, payouts);
                winnings += CheckDiagonal(grid, false, wagerPerLine, payouts);
                break;
            case BetChoice.AllLines:
                for (int i = 0; i < GRID_SIZE; i++)
                {
                    winnings += CheckLine(grid, i, wagerPerLine, payouts);
                    winnings += CheckColumn(grid, i, wagerPerLine, payouts);
                }
                winnings += CheckDiagonal(grid, true, wagerPerLine, payouts);
                winnings += CheckDiagonal(grid, false, wagerPerLine, payouts);
                break;
        }

        return winnings;
    }

    private static int CheckLine(string[,] grid, int row, int wagerPerLine, Dictionary<string, int> payouts)
    {
        int winnings = 0;
        string firstSymbol = grid[row, 0];
        bool isWinningLine = true;

        for (int col = 1; col < GRID_SIZE; col++)
        {
            if (grid[row, col] != firstSymbol)
            {
                isWinningLine = false;
                break;
            }
        }

        if (isWinningLine)
        {
            winnings = wagerPerLine * payouts[firstSymbol];
        }

        return winnings;
    }

    private static int CheckColumn(string[,] grid, int col, int wagerPerLine, Dictionary<string, int> payouts)
    {
        int winnings = 0;
        string firstSymbol = grid[0, col];
        bool isWinningLine = true;

        for (int row = 1; row < GRID_SIZE; row++)
        {
            if (grid[row, col] != firstSymbol)
            {
                isWinningLine = false;
                break;
            }
        }

        if (isWinningLine)
        {
            winnings = wagerPerLine * payouts[firstSymbol];
        }

        return winnings;
    }

    private static int CheckDiagonal(string[,] grid, bool isTopLeftToBottomRight, int wagerPerLine, Dictionary<string, int> payouts)
    {
        int winnings = 0;
        string firstSymbol = isTopLeftToBottomRight ? grid[0, 0] : grid[0, GRID_SIZE - 1];
        bool isWinningLine = true;

        for (int i = 1; i < GRID_SIZE; i++)
        {
            int row = isTopLeftToBottomRight ? i : i;
            int col = isTopLeftToBottomRight ? i : GRID_SIZE - 1 - i;
            if (grid[row, col] != firstSymbol)
            {
                isWinningLine = false;
                break;
            }
        }

        if (isWinningLine)
        {

           
          winnings = wagerPerLine * payouts[firstSymbol];
        }

        return winnings;
    }

    private static void ShowRoundResult(int winnings, int wager)
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
}
