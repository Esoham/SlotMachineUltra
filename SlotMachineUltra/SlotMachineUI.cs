using System;
namespace SlotMachine
{
    /// <summary>
    /// Static class responsible for user interaction in the slot machine game.
    /// </summary>
    public static class SlotMachineUI
    {
        /// <summary>
        /// Displays a message to the console.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Prompts the player to make a bet choice and returns the selected option.
        /// </summary>
        /// <returns>The player's bet choice.</returns>
        public static BetChoice GetPlayerChoice()
        {
            while (true)
            {
                Console.WriteLine("Choose your bet:");
                foreach (BetChoice option in Enum.GetValues(typeof(BetChoice)))
                {
                    Console.WriteLine($"{(int)option}. {option}");
                }

                if (int.TryParse(Console.ReadLine(), out int selectedChoice) && Enum.IsDefined(typeof(BetChoice), selectedChoice))
                {
                    return (BetChoice)selectedChoice;
                }

                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        /// <summary>
        /// Prompts the player to enter a wager per line and returns the wager amount.
        /// </summary>
        /// <param name="maxBetPerLine">The maximum allowable wager per line.</param>
        /// <returns>The wager amount per line.</returns>
        public static int GetWagerPerLine(int maxBetPerLine)
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
        /// Displays the slot machine grid.
        /// </summary>
        /// <param name="grid">The grid to display.</param>
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
        /// Displays the result of the round including winnings and total wager.
        /// </summary>
        /// <param name="winnings">The amount won.</param>
        /// <param name="wager">The total wager amount.</param>
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
        /// Asks the player if they want to play again.
        /// </summary>
        /// <returns>True if the player wants to play again; otherwise, false.</returns>
        public static bool AskToPlayAgain()
        {
            Console.Write("Do you want to play again? (y/n): ");
            return Console.ReadLine().Trim().ToLower() == "y";
        }
    }
}
