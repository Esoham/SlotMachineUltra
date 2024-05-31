using System;
using SlotMachine;
namespace SlotMachineUltra
{
    /// <summary>
    /// Represents the slot machine game logic.
    /// </summary>
    public class SlotMachineGame
    {
        /// <summary>
        /// Gets the size of the grid.
        /// </summary>
        public int GRID_SIZE { get; private set; }

        private string[,] grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="SlotMachineGame"/> class with the specified grid size.
        /// </summary>
        /// <param name="gridSize">The size of the grid.</param>
        public SlotMachineGame(int gridSize)
        {
            GRID_SIZE = gridSize;
            grid = new string[GRID_SIZE, GRID_SIZE];
        }

        /// <summary>
        /// Generates the grid with random symbols.
        /// </summary>
        public void GenerateGrid()
        {
            Random random = new Random();
            string[] symbols = { "A", "B", "C", "D", "E", "F", "G" };

            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    grid[i, j] = symbols[random.Next(symbols.Length)];
                }
            }
        }

        /// <summary>
        /// Gets the number of lines to bet on.
        /// </summary>
        /// <returns>The number of lines to bet on.</returns>
        public int GetLinesToBet()
        {
            return GRID_SIZE;
        }

        /// <summary>
        /// Calculates the winnings based on the grid and bet.
        /// </summary>
        /// <param name="betChoice">The player's betting choice.</param>
        /// <param name="wagerPerLine">The wager per line.</param>
        /// <returns>The amount won.</returns>
        public int CalculateWinnings(BetChoice betChoice, int wagerPerLine)
        {
            int matches = CheckMatches();
            return matches * wagerPerLine * Constants.WinMultiplier;
        }

        /// <summary>
        /// Checks for matches in the grid.
        /// </summary>
        /// <returns>The number of matches found.</returns>
        public int CheckMatches()
        {
            int matches = 0;

            // Check primary diagonal
            bool primaryDiagonalMatch = true;
            for (int i = 1; i < GRID_SIZE; i++)
            {
                if (grid[i, i] != grid[0, 0])
                {
                    primaryDiagonalMatch = false;
                    break;
                }
            }
            if (primaryDiagonalMatch)
            {
                matches++;
            }

            // Check secondary diagonal
            bool secondaryDiagonalMatch = true;
            for (int i = 1; i < GRID_SIZE; i++)
            {
                if (grid[i, GRID_SIZE - i - 1] != grid[0, GRID_SIZE - 1])
                {
                    secondaryDiagonalMatch = false;
                    break;
                }
            }
            if (secondaryDiagonalMatch)
            {
                matches++;
            }

            return matches;
        }

        /// <summary>
        /// Displays the grid to the console.
        /// </summary>
        public void DisplayGrid()
        {
            for (int i = 0; i < GRID_SIZE; i++)
            {
                for (int j = 0; j < GRID_SIZE; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Gets the current grid.
        /// </summary>
        /// <returns>The current grid as a 2D array of strings.</returns>
        public string[,] GetGrid()
        {
            return grid;
        }
    }
}
