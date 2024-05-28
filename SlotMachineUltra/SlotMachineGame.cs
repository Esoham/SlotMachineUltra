using System;
namespace SlotMachine
{
    /// <summary>
    /// Manages the game logic for the slot machine.
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
            SlotMachineUI.DisplayGameRulesAndPayouts();

            while (playerMoney > 0)
            {
                SlotMachineUI.DisplayMessage($"Current Money: ${playerMoney}");
                BetChoice betChoice = SlotMachineUI.GetPlayerChoice();
                int linesToBet = GetLinesToBet(betChoice);
                int maxPerLine = playerMoney / (linesToBet * Constants.MAX_BET_MULTIPLIER);
                int wagerPerLine = SlotMachineUI.GetWagerPerLine(playerMoney, maxPerLine);

                // Check if the total wager exceeds the player's money
                int totalWager = wagerPerLine * linesToBet;
                if (totalWager > playerMoney)
                {
                    SlotMachineUI.DisplayMessage(Constants.INVALID_WAGER_MESSAGE);
                    continue;
                }

                // Generate slot grid and calculate winnings
                int[,] grid = GenerateSlotGrid();
                int winnings = CalculateWinnings(grid, betChoice, wagerPerLine);
                playerMoney += winnings - totalWager; // Update player's money

                SlotMachineUI.DisplayResult(grid, winnings, totalWager, betChoice);

                // Ask the player if they want to play again
                if (playerMoney > 0 && !SlotMachineUI.PlayAgain())
                {
                    break;
                }
            }

            SlotMachineUI.DisplayGameOver();
        }

        private static int[,] GenerateSlotGrid()
        {
            int[,] grid = new int[Constants.GRID_SIZE, Constants.GRID_SIZE];

            for (int i = 0; i < Constants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Constants.GRID_SIZE; j++)
                {
                    grid[i, j] = random.Next(Constants.SYMBOL_MIN, Constants.SYMBOL_MAX + 1);
                }
            }

            return grid;
        }

        private static int CalculateWinnings(int[,] grid, BetChoice choice, int wagerPerLine)
        {
            int winnings = 0;

            // Calculate winnings for the center horizontal line
            if (choice == BetChoice.CenterHorizontalLine || choice == BetChoice.AllLines)
            {
                winnings += CalculateLineWinnings(grid, 1, 0, 0, 1, wagerPerLine);
            }

            // Calculate winnings for all horizontal lines
            if (choice == BetChoice.AllHorizontalLines || choice == BetChoice.AllLines)
            {
                for (int row = 0; row < Constants.GRID_SIZE; row++)
                {
                    winnings += CalculateLineWinnings(grid, row, 0, 0, 1, wagerPerLine);
                }
            }

            // Calculate winnings for all vertical lines
            if (choice == BetChoice.AllVerticalLines || choice == BetChoice.AllLines)
            {
                for (int col = 0; col < Constants.GRID_SIZE; col++)
                {
                    winnings += CalculateLineWinnings(grid, 0, col, 1, 0, wagerPerLine);
                }
            }

            // Calculate winnings for both diagonals
            if (choice == BetChoice.BothDiagonals || choice == BetChoice.AllLines)
            {
                winnings += CalculateLineWinnings(grid, 0, 0, 1, 1, wagerPerLine);
                winnings += CalculateLineWinnings(grid, 0, Constants.GRID_SIZE - 1, 1, -1, wagerPerLine);
            }

            return winnings;
        }

        private static int CalculateLineWinnings(int[,] grid, int startRow, int startCol, int rowStep, int colStep, int wagerPerLine)
        {
            int firstSymbol = grid[startRow, startCol];
            for (int i = 1; i < Constants.GRID_SIZE; i++)
            {
                if (grid[startRow + i * rowStep, startCol + i * colStep] != firstSymbol)
                {
                    return 0;
                }
            }
            return Constants.PAYOUTS[BetChoice.AllLines][Constants.GRID_SIZE] * wagerPerLine;
        }

        private static int GetLinesToBet(BetChoice choice)
        {
            switch (choice)
            {
                case BetChoice.CenterHorizontalLine: return 1;
                case BetChoice.AllHorizontalLines:
                case BetChoice.AllVerticalLines: return Constants.GRID_SIZE;
                case BetChoice.BothDiagonals: return 2;
                case BetChoice.AllLines: return 2 * Constants.GRID_SIZE + 2; // All lines plus two diagonals
                default:
                    throw new ArgumentOutOfRangeException(nameof(choice), "Invalid betting choice");
            }
        }
    }
}
