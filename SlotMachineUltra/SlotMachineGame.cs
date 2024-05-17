using System;
using System.Collections.Generic;

namespace SlotMachine
{
    class SlotMachineGame
    {
        private static readonly Random Random = new Random();

        private static readonly Dictionary<BetChoice, int> Payouts = new Dictionary<BetChoice, int>
        {
            { BetChoice.CenterHorizontalLine, Constants.PAYOUT_CENTER_HORIZONTAL },
            { BetChoice.AllHorizontalLines, Constants.PAYOUT_ALL_HORIZONTAL },
            { BetChoice.AllVerticalLines, Constants.PAYOUT_ALL_VERTICAL },
            { BetChoice.BothDiagonals, Constants.PAYOUT_BOTH_DIAGONALS },
            { BetChoice.AllLines, Constants.PAYOUT_ALL_LINES }
        };

        public static void StartGame()
        {
            int playerMoney = Constants.STARTING_PLAYER_MONEY;
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

        private static int GetLinesToBet(BetChoice choice)
        {
            return choice switch
            {
                BetChoice.CenterHorizontalLine => Constants.CENTER_HORIZONTAL_LINE,
                BetChoice.AllHorizontalLines => Constants.ALL_HORIZONTAL_LINES,
                BetChoice.AllVerticalLines => Constants.ALL_VERTICAL_LINES,
                BetChoice.BothDiagonals => Constants.BOTH_DIAGONALS,
                BetChoice.AllLines => Constants.ALL_LINES,
                _ => throw new ArgumentOutOfRangeException(nameof(choice), "Invalid betting choice"),
            };
        }

        private static int[,] GenerateSlotOutcomes()
        {
            int[,] grid = new int[Constants.GRID_SIZE, Constants.GRID_SIZE];
            for (int i = 0; i < Constants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Constants.GRID_SIZE; j++)
                {
                    grid[i, j] = Random.Next(1, 6);
                }
            }
            return grid;
        }

        private static void DisplayGrid(int[,] grid)
        {
            Console.WriteLine("Slot Machine Outcome:");
            for (int i = 0; i < Constants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Constants.GRID_SIZE; j++)
                {
                    Console.Write($"{grid[i, j]}{(j < Constants.GRID_SIZE - 1 ? $" {Constants.LINE_SEPARATOR} " : "")}");
                }
                Console.WriteLine(i < Constants.GRID_SIZE - 1 ? Constants.ROW_SEPARATOR : "\n");
            }
        }

        private static int CalculateWinnings(int[,] grid, BetChoice choice, int wagerPerLine)
        {
            int winnings = 0;

            switch (choice)
            {
                case BetChoice.CenterHorizontalLine:
                    if (IsLineWinning(grid, 1, 0, 1)) winnings += wagerPerLine * Payouts[choice];
                    break;
                case BetChoice.AllHorizontalLines:
                    for (int i = 0; i < Constants.GRID_SIZE; i++)
                    {
                        if (IsLineWinning(grid, i, 0, 1)) winnings += wagerPerLine * Payouts[choice];
                    }
                    break;
                case BetChoice.AllVerticalLines:
                    for (int j = 0; j < Constants.GRID_SIZE; j++)
                    {
                        if (IsLineWinning(grid, 0, j, Constants.GRID_SIZE)) winnings += wagerPerLine * Payouts[choice];
                    }
                    break;
                case BetChoice.BothDiagonals:
                    if (IsDiagonalWinning(grid, true)) winnings += wagerPerLine * Payouts[choice];
                    if (IsDiagonalWinning(grid, false)) winnings += wagerPerLine * Payouts[choice];
                    break;
                case BetChoice.AllLines:
                    for (int i = 0; i < Constants.GRID_SIZE; i++)
                    {
                        if (IsLineWinning(grid, i, 0, 1)) winnings += wagerPerLine * Payouts[choice];
                        if (IsLineWinning(grid, 0, i, Constants.GRID_SIZE)) winnings += wagerPerLine * Payouts[choice];
                    }
                    if (IsDiagonalWinning(grid, true)) winnings += wagerPerLine * Payouts[choice];
                    if (IsDiagonalWinning(grid, false)) winnings += wagerPerLine * Payouts[choice];
                    break;
            }

            return winnings;
        }

        private static bool IsLineWinning(int[,] grid, int startRow, int startCol, int step)
        {
            int firstValue = grid[startRow, startCol];
            for (int i = 1; i < Constants.GRID_SIZE; i++)
            {
                if (grid[startRow + i * step / Constants.GRID_SIZE, startCol + i * step % Constants.GRID_SIZE] != firstValue)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsDiagonalWinning(int[,] grid, bool isTopLeftToBottomRight)
        {
            int firstValue = grid[0, isTopLeftToBottomRight ? 0 : Constants.GRID_SIZE - 1];
            for (int i = 1; i < Constants.GRID_SIZE; i++)
            {
                int row = i;
                int col = isTopLeftToBottomRight ? i : Constants.GRID_SIZE - 1 - i;
                if (grid[row, col] != firstValue)
                {
                    return false;
                }
            }
            return true;
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
    }
}
