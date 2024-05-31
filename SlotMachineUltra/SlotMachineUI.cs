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
            Console.WriteLine(Constants.ChooseBetMessage);
            foreach (var choice in Enum.GetValues(typeof(BetChoice)))
            {
                Console.WriteLine($"{(int)choice}. {choice}");
            }

            while (true)
            {
                Console.Write(Constants.EnterChoiceMessage);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int selectedChoice) && Enum.IsDefined(typeof(BetChoice), selectedChoice))
                {
                    return (BetChoice)selectedChoice;
                }
                Console.WriteLine(Constants.InvalidChoiceMessage);
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
                Console.Write(string.Format(Constants.EnterWagerMessage, maxPerLine));
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int wagerPerLine) && wagerPerLine >= 1 && wagerPerLine <= maxPerLine)
                {
                    return wagerPerLine;
                }
                Console.WriteLine(Constants.InvalidWagerMessage);
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
            Console.WriteLine(Constants.SlotGridMessage);
            for (int i = 0; i < Constants.GridSize; i++)
            {
                for (int j = 0; j < Constants.GridSize; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine(string.Format(Constants.WinningsMessage, winnings, totalWager, betChoice));
        }

        /// <summary>
        /// Displays the game rules and payout information.
        /// </summary>
        public static void DisplayGameRulesAndPayouts()
        {
            Console.WriteLine(Constants.GameRulesMessage);
        }

        /// <summary>
        /// Displays the game over message.
        /// </summary>
        public static void DisplayGameOver()
        {
            Console.WriteLine(Constants.GameOverMessage);
        }

        /// <summary>
        /// Asks the player if they want to play again.
        /// </summary>
        /// <returns>True if the player wants to play again, otherwise false.</returns>
        public static bool PlayAgain()
        {
            Console.Write(Constants.PlayAgainMessage);
            string? choice = Console.ReadLine()?.ToLower();
            return choice == "y" || choice == "yes";
        }
    }
}
