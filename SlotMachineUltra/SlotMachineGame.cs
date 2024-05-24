using System;

namespace SlotMachine
{
    /// <summary>
    /// Static class containing the core logic for the slot machine game.
    /// </summary>
    public static class SlotMachineGame
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// Starts the slot machine game.
        /// </summary>
        public static void StartGame()
        {
            int playerMoney = Constants.STARTING_PLAYER_MONEY;
            SlotMachineUI.DisplayMessage(Constants.WELCOME_MESSAGE);

            while (playerMoney > 0)
            {
                SlotMachineUI.DisplayMessage($"Current Money: ${playerMoney}");
                BetChoice betChoice = SlotMachineUI.GetPlayerChoice();
                int linesToBet = GetLinesToBet(betChoice);
                int maxBetPerLine = playerMoney / (linesToBet * Constants.MAX_BET_MULTIPLIER);
                int wagerPerLine = SlotMachineUI.GetWagerPerLine(maxBetPerLine);
                int totalWager = wagerPerLine * linesToBet;

                if (totalWager > playerMoney)
                {
                    SlotMachineUI.DisplayMessage("You do not have enough money for that wager.");
                    continue;
                }

                playerMoney -= totalWager;
                int[,] grid = GenerateSlotOutcomes();
                SlotMachineUI.DisplayGrid(grid);
                int totalWinnings = CalculateWinnings(grid, betChoice, wagerPerLine);
                playerMoney += totalWinnings;

                SlotMachineUI.DisplayRoundResult(totalWinnings, totalWager);

                if (!SlotMachineUI.AskToPlayAgain())
                {
                    break;
                }
            }

            SlotMachineUI.DisplayMessage(Constants.GAME_OVER_MESSAGE);
        }

        /// <summary>
        /// Generates the slot machine outcomes in a grid.
        /// </summary>
        /// <returns>The generated slot outcomes grid.</returns>
        private static int[,] GenerateSlotOutcomes()
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
        /// Calculates the winnings based on the grid, bet choice, and wager per line.
        /// </summary>
        /// <param name="grid">The slot outcomes grid.</param>
        /// <param name="choice">The bet choice.</param>
        /// <param name="wagerPerLine">The wager per line.</param>
        /// <returns>The total winnings.</returns>
        private static int CalculateWinnings(int[,] grid, BetChoice choice, int wagerPerLine)
        {
            int winnings = 0;

            switch (choice)
            {
                case BetChoice.CenterHorizontalLine:
                    if (IsWinningLine(grid, 1, 0, 1))
                    {
                        winnings += Constants.PAYOUTS[wagerPerLine];
                    }
                    break;
                case BetChoice.AllHorizontalLines:
                    for (int i = 0; i < Constants.GRID_SIZE; i++)
                    {
                        if (IsWinningLine(grid, i, 0, 1))
                        {
                            winnings += Constants.PAYOUTS[wagerPerLine];
                        }
                    }
                    break;
                case BetChoice.AllVerticalLines:
                    for (int j = 0; j < Constants.GRID_SIZE; j++)
                    {
                        if (IsWinningLine(grid, 0, j, Constants.GRID_SIZE))
                        {
                            winnings += Constants.PAYOUTS[wagerPerLine];
                        }
                    }
                    break;
                case BetChoice.BothDiagonals:
                    if (IsWinningLine(grid, 0, 0, 1) || IsWinningLine(grid, 0, Constants.GRID_SIZE - 1, -1))
                    {
                        winnings += Constants.PAYOUTS[wagerPerLine];
                    }
                    break;
                case BetChoice.AllLines:
                    for (int i = 0; i < Constants.GRID_SIZE; i++)
                    {
                        if (IsWinningLine(grid, i, 0, 1))
                        {
                            winnings += Constants.PAYOUTS[wagerPerLine];
                        }
                        if (IsWinningLine(grid, 0, i, Constants.GRID_SIZE))
                        {
                            winnings += Constants.PAYOUTS[wagerPerLine];
                        }
                    }
                    if (IsWinningLine(grid, 0, 0, 1) || IsWinningLine(grid, 0, Constants.GRID_SIZE - 1, -1))
                    {
                        winnings += Constants.PAYOUTS[wagerPerLine];
                    }
                    break;
            }

            return winnings;
        }

        /// <summary>
        /// Checks if a line is winning based on the slot outcomes grid.
        /// </summary>
        /// <param name="grid">The slot outcomes grid.</param>
        /// <param name="row">The starting row index.</param>
        /// <param name="column">The starting column index.</param>
        /// <param name="step">The step for checking the next symbol.</param>
        /// <returns>True if the line is winning; otherwise, false.</returns>
        private static bool IsWinningLine(int[,] grid, int row, int column, int step)
        {
            int symbol = grid[row, column];
            for (int i = 1; i < Constants.GRID_SIZE; i++)
            {
                if (grid[row + i * step, column + i * step] != symbol)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets the number of lines to bet based on the bet choice.
        /// </summary>
        /// <param name="choice">The bet choice.</param>
        /// <returns>The number of lines to bet.</returns>
        private static int GetLinesToBet(BetChoice choice)
        {
            return choice switch
            {
                BetChoice.CenterHorizontalLine => 1,
                BetChoice.AllHorizontalLines => Constants.GRID_SIZE,
                BetChoice.AllVerticalLines => Constants.GRID_SIZE,
                BetChoice.BothDiagonals => 2,
                BetChoice.AllLines => Constants.GRID_SIZE * 2 + 2,
                _ => throw new ArgumentOutOfRangeException(nameof(choice), "Invalid betting choice")
            };
        }
    }
}
