using System;
using SlotMachine;
namespace SlotMachineUltra
{
    /// <summary>
    /// Handles all user interactions for the slot machine game.
    /// </summary>
    public static class SlotMachineUI
    {
        /// <summary>
        /// Displays a message to the user.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Gets the player's betting choice.
        /// </summary>
        /// <returns>The player's betting choice.</returns>
        public static BetChoice GetPlayerChoice()
        {
            Console.WriteLine(Messages.CHOOSE_BET_MESSAGE);
            foreach (var choice in Enum.GetValues(typeof(BetChoice)))
            {
                Console.WriteLine($"{(int)choice}. {choice}");
            }

            while (true)
            {
                Console.Write(Messages.ENTER_CHOICE_MESSAGE);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int selectedChoice) && Enum.IsDefined(typeof(BetChoice), selectedChoice))
                {
                    return (BetChoice)selectedChoice;
                }
                Console.WriteLine(Messages.INVALID_CHOICE_MESSAGE);
            }
        }

        /// <summary>
        /// Gets the wager per line from the player.
        /// </summary>
        /// <param name="maxPerLine">The maximum wager per line.</param>
        /// <returns>The wager per line.</returns>
        public static int GetWagerPerLine(int maxPerLine)
        {
            while (true)
            {
                Console.Write(string.Format(Messages.ENTER_WAGER_MESSAGE, maxPerLine));
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int wagerPerLine) && wagerPerLine >= 1 && wagerPerLine <= maxPerLine)
                {
                    return wagerPerLine;
                }
                Console.WriteLine(Messages.INVALID_WAGER_MESSAGE);
            }
        }

        /// <summary>
        /// Displays the slot machine result grid and the outcome.
        /// </summary>
        /// <param name="grid">The slot machine grid.</param>
        /// <param name="winnings">The amount won.</param>
        /// <param name="totalWager">The total wager amount.</param>
        /// <param name="betChoice">The player's betting choice.</param>
        public static void DisplayResult(string[,] grid, int winnings, int totalWager, BetChoice betChoice)
        {
            Console.WriteLine(Messages.SLOT_GRID_MESSAGE);
            for (int i = 0; i < Constants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Constants.GRID_SIZE; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine(string.Format(Messages.WINNINGS_MESSAGE, winnings, totalWager, betChoice));
        }

        /// <summary>
        /// Displays the game rules and payout information.
        /// </summary>
        public static void DisplayGameRulesAndPayouts()
        {
            Console.WriteLine(Messages.GAME_RULES_MESSAGE);
        }

        /// <summary>
        /// Displays the game over message.
        /// </summary>
        public static void DisplayGameOver()
        {
            Console.WriteLine(Messages.GAME_OVER_MESSAGE);
        }

        /// <summary>
        /// Asks the player if they want to play again.
        /// </summary>
        /// <returns>True if the player wants to play again, otherwise false.</returns>
        public static bool PlayAgain()
        {
            Console.Write(Messages.PLAY_AGAIN_MESSAGE);
            string? choice = Console.ReadLine()?.ToLower();
            return choice == "y" || choice == "yes";
        }
    }
}
