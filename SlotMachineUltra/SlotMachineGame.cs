using System;
namespace SlotMachine
{
    /// <summary>
    /// Class containing the logic for the Slot Machine game.
    /// </summary>
    public static class SlotMachineGame
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Generates the outcomes for the slot machine.
        /// </summary>
        /// <returns>A 2D array representing the slot outcomes.</returns>
        public static int[,] GenerateSlotOutcomes()
        {
            int[,] grid = new int[Constants.GRID_SIZE, Constants.GRID_SIZE];
            for (int i = 0; i < Constants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Constants.GRID_SIZE; j++)
                {
                    grid[i, j] = random.Next(Constants.SYMBOL_MIN, Constants.SYMBOL_MAX);
                }
            }
            return grid;
        }

        /// <summary>
        /// Calculates the total winnings based on the slot outcomes and the player's bet choice.
        /// </summary>
        /// <param name="grid">The slot outcomes grid.</param>
        /// <param name="choice">The player's bet choice.</param>
        /// <param name="wagerPerLine">The wager per line.</param>
        /// <returns>The total winnings.</returns>
        public static int CalculateWinnings(int[,] grid, BetChoice choice, int wagerPerLine)
        {
            int winnings = 0;

            switch (choice)
            {
                case BetChoice.CenterHorizontalLine:
                    winnings += IsWinningLine(grid, 1, 0, 1, 0) ? wagerPerLine * Constants.PAYOUTS[1] : 0;
                    break;
                case BetChoice.AllHorizontalLines:
                    for (int i = 0; i < Constants.GRID_SIZE; i++)
                    {
                        winnings += IsWinningLine(grid, i, 0, 0, 1) ? wagerPerLine * Constants.PAYOUTS[2] : 0;
                    }
                    break;
                case BetChoice.AllVerticalLines:
                    for (int i = 0; i < Constants.GRID_SIZE; i++)
                    {
                        winnings += IsWinningLine(grid, 0, i, 1, 0) ? wagerPerLine * Constants.PAYOUTS[3] : 0;
                    }
                    break;
                case BetChoice.BothDiagonals:
                    winnings += IsWinningLine(grid, 0, 0, 1, 1) ? wagerPerLine * Constants.PAYOUTS[4] : 0;
                    winnings += IsWinningLine(grid, 0, Constants.GRID_SIZE - 1, 1, -1) ? wagerPerLine * Constants.PAYOUTS[4] : 0;
                    break;
                case BetChoice.AllLines:
                    for (int i = 0; i < Constants.GRID_SIZE; i++)
                    {
                        winnings += IsWinningLine(grid, i, 0, 0, 1) ? wagerPerLine * Constants.PAYOUTS[5] : 0;
                        winnings += IsWinningLine(grid, 0, i, 1, 0) ? wagerPerLine * Constants.PAYOUTS[5] : 0;
                    }
                    winnings += IsWinningLine(grid, 0, 0, 1, 1) ? wagerPerLine * Constants.PAYOUTS[5] : 0;
                    winnings += IsWinningLine(grid, 0, Constants.GRID_SIZE - 1, 1, -1) ? wagerPerLine * Constants.PAYOUTS[5] : 0;
                    break;
            }

            return winnings;
        }

        /// <summary>
        /// Checks if a line in the slot outcomes grid is a winning line.
        /// </summary>
        /// <param name="grid">The slot outcomes grid.</param>
        /// <param name="row">The starting row of the line.</param>
        /// <param name="column">The starting column of the line.</param>
        /// <param name="rowStep">The step size for rows.</param>
        /// <param name="columnStep">The step size for columns.</param>
        /// <returns>True if the line is a winning line, otherwise false.</returns>
        private static bool IsWinningLine(int[,] grid, int row, int column, int rowStep, int columnStep)
        {
            try
            {
                int symbol = grid[row, column];
                for (int i = 1; i < Constants.GRID_SIZE; i++)
                {
                    if (grid[row + i * rowStep, column + i * columnStep] != symbol)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                // Log the error if necessary
                return false;
            }
        }
    }
}
