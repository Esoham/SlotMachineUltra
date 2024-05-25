using System;
namespace SlotMachine
{
    /// <summary>
    /// Class handling the user interface for the Slot Machine game.
    /// </summary>
    public static class SlotMachineUI
    {
        /// <summary>
        /// Starts the Slot Machine game.
        /// </summary>
        public static void StartGame()
        {
            int playerMoney = Constants.STARTING_PLAYER_MONEY;
            Console.WriteLine(Constants.WELCOME_MESSAGE);

            while (playerMoney > 0)
            {
                Console.WriteLine($"Current Money: ${playerMoney}");
                BetChoice betChoice = GetPlayerChoice();
                int linesToBet = GetLinesToBet(betChoice);
                int maxBetPerLine = playerMoney / (linesToBet * Constants.MAX_BET_MULTIPLIER);
                int wagerPerLine = GetWagerPerLine(maxBetPerLine);
                int totalWager = wagerPerLine * linesToBet;

                if (totalWager > playerMoney)
                {
                    Console.WriteLine(Constants.INVALID_WAGER_MESSAGE);
                    continue;
                }

                playerMoney -= totalWager;
                int[,] grid = SlotMachineGame.GenerateSlotOutcomes();
                DisplayGrid(grid);
                int totalWinnings = SlotMachineGame.CalculateWinnings(grid, betChoice, wagerPerLine);
                playerMoney += totalWinnings;

                DisplayRoundResult(totalWinnings, totalWager);
            }

            Console.WriteLine(Constants.GAME_OVER_MESSAGE);
        }

        /// <summary>
        /// Gets the player's bet choice.
        /// </summary>
        /// <returns>The player's bet choice.</returns>
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

        /// <summary>
        /// Gets the wager per line from the player.
        /// </summary>
        /// <param name="maxBetPerLine">The maximum bet per line.</param>
        /// <returns>The wager per line.</returns>
        private static int GetWagerPerLine(int maxBetPerLine)
        {
            while (true)
            {
                Console.Write($"Enter your wager per line (max ${maxBetPerLine}): ");
                if (int.TryParse(Console.ReadLine(), out int wagerPerLine) && wagerPerLine >= 1 && wagerPerLine <= maxBetPerLine)
                {
                    return wagerPerLine;
                }

                Console.WriteLine(Constants.INVALID_WAGER_MESSAGE);
            }
        }

        /// <summary>
        /// Displays the slot outcomes grid.
        /// </summary>
        /// <param name="grid">The slot outcomes grid.</param>
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

        /// <summary>
        /// Displays the result of the round.
        /// </summary>
        /// <param name="winnings">The total winnings.</param>
        /// <param name="wager">The total wager.</param>
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

        /// <summary>
        /// Determines the number of lines to bet based on the player's bet choice.
        /// </summary>
        /// <param name="choice">The player's bet choice.</param>
        /// <returns>The number of lines to bet.</returns>
        private static int GetLinesToBet(BetChoice choice)
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
}
