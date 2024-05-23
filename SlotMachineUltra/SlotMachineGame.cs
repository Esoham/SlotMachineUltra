namespace SlotMachine
{
    public static class SlotMachineGame
    {
        private static readonly Random random = new Random();

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

        public static int CalculateWinnings(int[,] grid, BetChoice choice, int wagerPerLine)
        {
            int winnings = 0;
            switch (choice)
            {
                case BetChoice.CenterHorizontalLine:
                    if (IsWinningLine(grid, 1, 0, 1, 2))
                        winnings += Constants.PAYOUTS[3] * wagerPerLine;
                    break;
                case BetChoice.AllHorizontalLines:
                    for (int i = 0; i < Constants.GRID_SIZE; i++)
                    {
                        if (IsWinningLine(grid, i, 0, i, 2))
                            winnings += Constants.PAYOUTS[3] * wagerPerLine;
                    }
                    break;
                case BetChoice.AllVerticalLines:
                    for (int j = 0; j < Constants.GRID_SIZE; j++)
                    {
                        if (IsWinningLine(grid, 0, j, 2, j))
                            winnings += Constants.PAYOUTS[3] * wagerPerLine;
                    }
                    break;
                case BetChoice.BothDiagonals:
                    if (IsWinningLine(grid, 0, 0, 2, 2) || IsWinningLine(grid, 0, 2, 2, 0))
                        winnings += Constants.PAYOUTS[3] * wagerPerLine;
                    break;
                case BetChoice.AllLines:
                    for (int i = 0; i < Constants.GRID_SIZE; i++)
                    {
                        if (IsWinningLine(grid, i, 0, i, 2))
                            winnings += Constants.PAYOUTS[3] * wagerPerLine;
                    }
                    for (int j = 0; j < Constants.GRID_SIZE; j++)
                    {
                        if (IsWinningLine(grid, 0, j, 2, j))
                            winnings += Constants.PAYOUTS[3] * wagerPerLine;
                    }
                    if (IsWinningLine(grid, 0, 0, 2, 2) || IsWinningLine(grid, 0, 2, 2, 0))
                        winnings += Constants.PAYOUTS[3] * wagerPerLine;
                    break;
            }
            return winnings;
        }

        private static bool IsWinningLine(int[,] grid, int startX, int startY, int endX, int endY)
        {
            int symbol = grid[startX, startY];
            int deltaX = (endX - startX) / 2;
            int deltaY = (endY - startY) / 2;
            for (int i = 1; i <= 2; i++)
            {
                if (grid[startX + i * deltaX, startY + i * deltaY] != symbol)
                    return false;
            }
            return true;
        }
    }
}
