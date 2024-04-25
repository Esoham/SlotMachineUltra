using System;
using System.Collections.Generic;

class SlotMachineGame
{
    private const int GridSize = 3;
    private const int MinSymbolValue = 1;
    private const int MaxSymbolValue = 5;
    private static readonly Random random = new Random();
    private const int StartingPlayerMoney = 100;
    private const int MaxBetMultiplier = 5;

    private enum BetChoice
    {
        CenterHorizontalLine = 1,
        AllHorizontalLines,
        AllVerticalLines,
        BothDiagonals,
        AllLines
    }

    // Constants for the number of lines corresponding to each betting option
    private const int LinesCenterHorizontal = 1;
    private const int LinesHorizontal = GridSize;
    private const int LinesVertical = GridSize;
    private const int LinesDiagonals = 2;
    private const int LinesAll = GridSize * 2 + 2;

    // Symbol Constants
    private const string SymbolOne = "1";
    private const string SymbolTwo = "2";
    private const string SymbolThree = "3";
    private const string SymbolFour = "4";
    private const string SymbolFive = "5";

    // Payout Constants
    private const int PayoutOne = 2;
    private const int PayoutTwo = 3;
    private const int PayoutThree = 4;
    private const int PayoutFour = 5;
    private const int PayoutFive = 10;

    // Payout Dictionary
    private static readonly Dictionary<string, int> Payouts = new Dictionary<string, int>
    {
        {SymbolOne, PayoutOne},
        {SymbolTwo, PayoutTwo},
        {SymbolThree, PayoutThree},
        {SymbolFour, PayoutFour},
        {SymbolFive, PayoutFive}
    };

    static void Main()
    {
        Console.WriteLine("Welcome to the Slot Machine Game!");
        int playerMoney = StartingPlayerMoney;

        while (playerMoney > 0)
        {
            ShowPlayerMoney(playerMoney);
            BetChoice choice = GetPlayerChoice();
            int linesToBet = GetLinesToBet(choice);
            int maxBetPerLine = playerMoney / (linesToBet * MaxBetMultiplier);
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
        string[] options = Enum.GetNames(typeof(BetChoice));
        for (int i = 0; i < options.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i].Replace('_', ' ')}");
        }

        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= options.Length)
        {
            return (BetChoice)choice;
        }
        else
        {
            Console.WriteLine("Invalid choice. Please try again.");
            return GetPlayerChoice(); // Recursion to handle invalid input
        }
    }

    private static int GetLinesToBet(BetChoice choice)
    {
        switch (choice)
        {
            case BetChoice.CenterHorizontalLine: return LinesCenterHorizontal;
            case BetChoice.AllHorizontalLines:
            case BetChoice.AllVerticalLines: return LinesHorizontal;
            case BetChoice.BothDiagonals: return LinesDiagonals;
            case BetChoice.AllLines: return LinesAll;
            default: throw new ArgumentOutOfRangeException(nameof(choice), "Invalid betting choice");
        }
    }

    private static int GetWagerPerLine(int maxBetPerLine)
    {
        Console.Write($"Enter your wager per line (max ${maxBetPerLine}): ");
        if (int.TryParse(Console.ReadLine(), out int wagerPerLine) && wagerPerLine >= 1 && wagerPerLine <= maxBetPerLine)
        {
            return wagerPerLine;
        }
        else
        {
            Console.WriteLine("Invalid wager. Please try again.");
            return GetWagerPerLine(maxBetPerLine); // Recursion to handle invalid input
        }
    }

    private static string[,] GenerateSlotOutcomes()
    {
        string[,] grid = new string[GridSize, GridSize];
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                grid[i, j] = random.Next(MinSymbolValue, MaxSymbolValue + 1).ToString();  // Simplified random number generation
            }
        }
        return grid;
    }

    private static void DisplayGrid(string[,] grid)
    {
        Console.WriteLine("Slot Machine Outcome:");
        for (int i = 0; i < GridSize; i++)
        {
            for (int j = 0; j < GridSize; j++)
            {
                Console.Write(grid[i, j] + (j < GridSize - 1 ? " | " : ""));
            }
            Console.WriteLine(i < GridSize - 1 ? "\n---+---+---" : "\n");
        }
    }

    private static int CalculateWinnings(string[,] grid, BetChoice choice, int wagerPerLine)
    {
        int winnings = 0;
        if (choice == BetChoice.CenterHorizontalLine)
        {
            winnings += CheckLine(grid, 1, wagerPerLine, Payouts);
        }
        else if (choice == BetChoice.AllHorizontalLines || choice == BetChoice.AllVerticalLines)
        {
            for (int i = 0; i < GridSize; i++)
            {
                winnings += (choice == BetChoice.AllHorizontalLines) ? CheckLine(grid, i, wagerPerLine, Payouts) : CheckColumn(grid, i, wagerPerLine, Payouts);
            }
        }
        else if (choice == BetChoice.BothDiagonals)
        {
            winnings += CheckDiagonal(grid, true, wagerPerLine, Payouts);
            winnings += CheckDiagonal(grid, false, wagerPerLine, Payouts);
        }
        else if (choice == BetChoice.AllLines)
        {
            for (int i = 0; i < GridSize; i++)
            {
                winnings += CheckLine(grid, i, wagerPerLine, Payouts);
                winnings += CheckColumn(grid, i, wagerPerLine, Payouts);
            }
            winnings += CheckDiagonal(grid, true, wagerPerLine, Payouts);
            winnings += CheckDiagonal(grid, false, wagerPerLine, Payouts);
        }

        return winnings;
    }

    private static int CheckLine(string[,] grid, int row, int wagerPerLine, Dictionary<string, int> payouts)
    {
        string firstSymbol = grid[row, 0];
        bool isWinningLine = true;
        for (int col = 1; col < GridSize; col++)
        {
            if (grid[row, col] != firstSymbol)
            {
                isWinningLine = false;
                break;
            }
        }

        return isWinningLine ? wagerPerLine * payouts.GetValueOrDefault(firstSymbol, 0) : 0;
    }

    private static int CheckColumn(string[,] grid, int col, int wagerPerLine, Dictionary<string, int> payouts)
    {
        string firstSymbol = grid[0, col];
        bool isWinningLine = true;
        for (int row = 1; row < GridSize; row++)
        {
            if (grid[row, col] != firstSymbol)
            {
                isWinningLine = false;
                break;
            }
        }

        return isWinningLine ? wagerPerLine * payouts.GetValueOrDefault(firstSymbol, 0) : 0;
    }

    private static int CheckDiagonal(string[,] grid, bool isTopLeftToBottomRight, int wagerPerine, Dictionary<string, int> payouts)
    {
        string firstSymbol = isTopLeftToBottomRight ? grid[0, 0] : grid[0, GridSize - 1];
        bool isWinningLine = true;
        for (int i = 1; i < GridSize; i++)
        {
            int row = i;
            int col = isTopLeftToBottomRight ? i : GridSize - 1 - i;
            if (grid[row, col] != firstSymbol)
            {
                isWinningLine = false;
                break;
            }
        }

        return isWinningLine ? wagerPerine * payouts.GetValueOrDefault(firstSymbol, 0) : 0;
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
