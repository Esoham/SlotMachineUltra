using System;
using SlotMachine;

namespace SlotMachineUltra
{
    class Program
    {
        /// <summary>
        /// The main entry point of the application.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        static void Main(string[] args)
        {
            Console.WriteLine(Constants.WelcomeMessage);

            // Initialize the slot machine game with the default grid size.
            SlotMachineGame game = new SlotMachineGame(Constants.GridSize);

            // Generate the grid with random symbols.
            game.GenerateGrid();

            // Display the generated grid to the console.
            game.DisplayGrid();

            // Get the number of lines to bet on
            int linesToBet = game.GetLinesToBet();

            // Get the wager per line from the player
            int wagerPerLine = SlotMachineUI.GetWagerPerLine(Constants.DefaultWager);

            // Get the player's betting choice
            BetChoice betChoice = SlotMachineUI.GetPlayerChoice();

            // Calculate the winnings based on the grid and bet
            int winnings = game.CalculateWinnings(betChoice, wagerPerLine);

            // Display the result
            SlotMachineUI.DisplayResult(game.GetGrid(), winnings, wagerPerLine * linesToBet, betChoice);

            // Display game over message
            SlotMachineUI.DisplayGameOver();

            // Ask if the player wants to play again
            if (SlotMachineUI.PlayAgain())
            {
                Main(args); // Restart the game
            }
        }
    }
}
