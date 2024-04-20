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
        Console.WriteLine("1. Center Horizontal Line");
        Console.WriteLine("2. All Horizontal Lines");
        Console.WriteLine("3. All Vertical Lines");
        Console.WriteLine("4. Both Diagonals");
        Console.WriteLine("5. All Lines");

        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
        {
            Console.WriteLine("Invalid choice. Please try again.");
        }

        return (BetChoice)choice;
    }

    private static int GetLinesToBet(BetChoice choice)
    {
        switch (choice)
        {
            case BetChoice.CenterHorizontalLine:
                return 1;
            case BetChoice.AllHorizontalLines:
            case BetChoice.AllVerticalLines:
                return GRID_SIZE;
            case BetChoice.BothDiagonals:
                return 2;
            case BetChoice.AllLines:
                return GRID_SIZE * 2 + 2;
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
                grid[i, j] = ((int)RANDOM.Next(MIN_SYMBOL_VALUE, MAX_SYMBOL_VALUE + 1)).ToString();
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