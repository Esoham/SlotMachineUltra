using System;
namespace SlotMachine
{
    /// <summary>
    /// Manages the user interface for the slot machine game.
    /// </summary>
    public static class SlotMachineUI
    {
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static BetChoice GetPlayerChoice()
        {
            Console.WriteLine("Choose your bet:");
            foreach (var choice in Enum.GetValues(typeof(BetChoice)))
            {
                Console.WriteLine($"{(int)choice}. {choice}");
            }
            int selectedChoice;
            while (!int.TryParse(Console.ReadLine(), out selectedChoice) || !Enum.IsDefined(typeof(BetChoice), selectedChoice))
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
            return (BetChoice)selectedChoice;
        }

        public static int GetWagerPerLine(int playerMoney, int maxPerLine)
        {
            int wagerPerLine;
            while (true)
            {
                Console.Write($"Enter your wager per line (1 to {maxPerLine}): ");
                if (int.TryParse(Console.ReadLine(), out wagerPerLine) && wagerPerLine >= 1 && wagerPerLine <= maxPerLine)
                {
                    return wagerPerLine;
                }
                Console.WriteLine("Invalid wager. Please try again.");
            }
        }

        public static void DisplayResult(int[,] grid, int winnings, int totalWager, BetChoice betChoice)
        {
            // Display the slot grid
            Console.WriteLine("Slot Grid:");
            for (int i = 0; i < Constants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Constants.GRID_SIZE; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }

            if (winnings > 0)
            {
                Console.WriteLine($"You won ${winnings}! Your total wager was ${totalWager}.");
                DisplayWinningLines(grid, betChoice);
            }
            else
            {
                Console.WriteLine($"You did not win anything. Your total wager was ${totalWager}.");
            }
        }

        private static void DisplayWinningLines(int[,] grid, BetChoice betChoice)
        {
            Console.WriteLine("Winning Lines:");

            if (betChoice == BetChoice.CenterHorizontalLine || betChoice == BetChoice.AllLines)
            {
                DisplayLine(grid, 1, 0, 0, 1);
            }

            if (betChoice == BetChoice.AllHorizontalLines || betChoice == BetChoice.AllLines)
            {
                for (int row = 0; row < Constants.GRID_SIZE; row++)
                {
                    DisplayLine(grid, row, 0, 0, 1);
                }
            }

            if (betChoice == BetChoice.AllVerticalLines || betChoice == BetChoice.AllLines)
            {
                for (int col = 0; col < Constants.GRID_SIZE; col++)
                {
                    DisplayLine(grid, 0, col, 1, 0);
                }
            }

            if (betChoice == BetChoice.BothDiagonals || betChoice == BetChoice.AllLines)
            {
                DisplayLine(grid, 0, 0, 1, 1);
                DisplayLine(grid, 0, Constants.GRID_SIZE - 1, 1, -1);
            }
        }

        private static void DisplayLine(int[,] grid, int startRow, int startCol, int rowStep, int colStep)
        {
            Console.Write($"({startRow}, {startCol}) -> ");
            for (int i = 0; i < Constants.GRID_SIZE; i++)
            {
                Console.Write(grid[startRow, startCol] + " ");
                startRow += rowStep;
                startCol += colStep;
            }
            Console.WriteLine();
        }

        public static void DisplayGameOver()
        {
            Console.WriteLine(Constants.GAME_OVER_MESSAGE);
        }

        public static void DisplayGameRulesAndPayouts()
        {
            Console.WriteLine("Game Rules and Payout Table:");
            Console.WriteLine("Bet Choice\tConsecutive Symbols\tPayout Multiplier");
            foreach (var betChoice in Constants.PAYOUTS)
            {
                Console.WriteLine($"{betChoice.Key}:");
                foreach (var payout in betChoice.Value)
                {
                    Console.WriteLine($"\t\t{payout.Key}\t\t{payout.Value}x");
                }
            }
        }

        public static bool PlayAgain()
        {
            Console.Write("Do you want to play again? (y/n): ");
            string choice = Console.ReadLine().ToLower();
            return choice == "y" || choice == "yes";
        }
    }
}
