using System;
namespace SlotMachine
{
    /// <summary>
    /// Contains the core game logic for the slot machine game.
    /// </summary>
    public static class SlotMachineGame
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Generates a 3x3 grid of random slot machine symbols.
        /// </summary>
        /// <returns>A 2D array representing the slot machine grid.</returns>
        public static int[,] GenerateSlotGrid()
        {
            int[,] grid = new int[Constants.GridSize, Constants.GridSize];
            for (int i = 0; i < Constants.GridSize; i++)
            {
                for (int j = 0; j < Constants.GridSize; j++)
                {
                    grid[i, j] = random.Next(1, Constants.SymbolsCount + 1);
                }
            }
            return grid;
        }

        /// <summary>
        /// Calculates the total winnings based on the generated grid, bet choice, and wager per line.
        /// </summary>
        /// <param name="grid">The generated slot machine grid.</param>
        /// <param name="betChoice">The player's betting choice.</param>
        /// <param name="wagerPerLine">The wager per line.</param>
        /// <returns>The total winnings.</returns>
        public static int CalculateWinnings(int[,] grid, BetChoice betChoice, int wagerPerLine)
        {
            int baseWinnings = CalculateBaseWinnings(grid, betChoice, wagerPerLine);

            if (random.NextDouble() < Constants.MultiplierChance)
            {
                baseWinnings *= Constants.MultiplierValue;
            }

            return baseWinnings;
        }

        /// <summary>
        /// Calculates the base winnings without any multipliers.
        /// </summary>
        /// <param name="grid">The generated slot machine grid.</param>
        /// <param name="betChoice">The player's betting choice.</param>
        /// <param name="wagerPerLine">The wager per line.</param>
        /// <returns>The base winnings.</returns>
        private static int CalculateBaseWinnings(int[,] grid, BetChoice betChoice, int wagerPerLine)
        {
            int matches = 0;

            switch (betChoice)
            {
                case BetChoice.CenterHorizontalLine:
                    matches = CheckHorizontalMatch(grid, 1);
                    break;
                case BetChoice.AllHorizontalLines:
                    for (int i = 0; i < Constants.GridSize; i++)
                    {
                        matches += CheckHorizontalMatch(grid, i);
                    }
                    break;
                case BetChoice.AllVerticalLines:
                    for (int j = 0; j < Constants.GridSize; j++)
                    {
                        matches += CheckVerticalMatch(grid, j);
                    }
                    break;
                case BetChoice.BothDiagonals:
                    matches = CheckDiagonalMatches(grid);
                    break;
                case BetChoice.AllLines:
                    for (int i = 0; i < Constants.GridSize; i++)
                    {
                        matches += CheckHorizontalMatch(grid, i);
                    }
                    for (int j = 0; j < Constants.GridSize; j++)
                    {
                        matches += CheckVerticalMatch(grid, j);
                    }
                    matches += CheckDiagonalMatches(grid);
                    break;
            }

            if (matches > 0)
            {
                return matches * wagerPerLine * Constants.WinMultiplier;
            }

            return 0;
        }

        /// <summary>
        /// Checks for a horizontal match on the specified row.
        /// </summary>
        /// <param name="grid">The slot machine grid.</param>
        /// <param name="row">The row to check for a match.</param>
        /// <returns>1 if a match is found, 0 otherwise.</returns>
        private static int CheckHorizontalMatch(int[,] grid, int row)
        {
            bool match = true;
            int symbol = grid[row, 0];

            for (int j = 1; j < Constants.GridSize; j++)
            {
                if (grid[row, j] != symbol)
                {
                    match = false;
                    break;
                }
            }

            return match ? 1 : 0;
        }

        /// <summary>
        /// Checks for a vertical match on the specified column.
        /// </summary>
        /// <param name="grid">The slot machine grid.</param>
        /// <param name="column">The column to check for a match.</param>
        /// <returns>1 if a match is found, 0 otherwise.</returns>
        private static int CheckVerticalMatch(int[,] grid, int column)
        {
            bool match = true;
            int symbol = grid[0, column];

            for (int i = 1; i < Constants.GridSize; i++)
            {
                if (grid[i, column] != symbol)
                {
                    match = false;
                    break;
                }
            }

            return match ? 1 : 0;
        }

        /// <summary>
        /// Checks for matches on both diagonals.
        /// </summary>
        /// <param name="grid">The slot machine grid.</param>
        /// <returns>The number of diagonal matches found.</returns>
        private static int CheckDiagonalMatches(int[,] grid)
        {
            int matches = 0;

            // Check primary diagonal
            if (grid[0, 0] == grid[1, 1] && grid[1, 1] == grid[2, 2])
            {
                matches++;
            }

            // Check secondary diagonal
            if (grid[0, 2] == grid[1, 1] && grid[1, 1] == grid[2, 0])
            {
                matches++;
            }

            return matches;
        }

        /// <summary>
        /// Determines the number of lines to bet on based on the player's choice.
        /// </summary>
        /// <param name="choice">The player's betting choice.</param>
        /// <returns>The number of lines to bet on.</returns>
        public static int GetLinesToBet(BetChoice choice)
        {
            return choice switch
            {
                BetChoice.CenterHorizontalLine => 1,
                BetChoice.AllHorizontalLines => Constants.GridSize,
                BetChoice.AllVerticalLines => Constants.GridSize,
                BetChoice.BothDiagonals => 2,
                BetChoice.AllLines => 2 * Constants.GridSize + 2,
                _ => throw new ArgumentOutOfRangeException(nameof(choice), Constants.InvalidBetChoiceMessage),
            };
        }
    }
}
