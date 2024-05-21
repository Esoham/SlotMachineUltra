namespace SlotMachine
{
    public class SlotMachineGame
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
                    if (AllSymbolsMatch(grid, Constants.GRID_SIZE / 2, true))
                        winnings += Constants.PAYOUTS[wagerPerLine];
                    break;
                case BetChoice.AllHorizontalLines:
                    for (int i = 0; i < Constants.GRID_SIZE; i++)
                    {
                        if (AllSymbolsMatch(grid, i, true))
                            winnings += Constants.PAYOUTS[wagerPerLine];
                    }
                    break;
                case BetChoice.AllVerticalLines:
                    for (int j = 0; j < Constants.GRID_SIZE; j++)
                    {
                        if (AllSymbolsMatch(grid, j, false))
                            winnings += Constants.PAYOUTS[wagerPerLine];
                    }
                    break;
                case BetChoice.BothDiagonals:
                    if (AllSymbolsMatch(grid, 0, 0, true) || AllSymbolsMatch(grid, 0, Constants.GRID_SIZE - 1, false))
                        winnings += Constants.PAYOUTS[wagerPerLine];
                    break;
                case BetChoice.AllLines:
                    for (int i = 0; i < Constants.GRID_SIZE; i++)
                    {
                        if (AllSymbolsMatch(grid, i, true) || AllSymbolsMatch(grid, i, false))
                            winnings += Constants.PAYOUTS[wagerPerLine];
                    }
                    if (AllSymbolsMatch(grid, 0, 0, true) || AllSymbolsMatch(grid, 0, Constants.GRID_SIZE - 1, false))
                        winnings += Constants.PAYOUTS[wagerPerLine];
                    break;
            }
            return winnings;
        }

        private static bool AllSymbolsMatch(int[,] grid, int index, bool isRow)
        {
            int symbol = isRow ? grid[index, 0] : grid[0, index];
            for (int i = 1; i < Constants.GRID_SIZE; i++)
            {
                if (isRow)
                {
                    if (grid[index, i] != symbol)
                        return false;
                }
                else
                {
                    if (grid[i, index] != symbol)
                        return false;
                }
            }
            return true;
        }

        private static bool AllSymbolsMatch(int[,] grid, int startX, int startY, bool isTopLeftToBottomRight)
        {
            int symbol = grid[startX, startY];
            for (int i = 1; i < Constants.GRID_SIZE; i++)
            {
                if (grid[startX + i, isTopLeftToBottomRight ? startY + i : startY - i] != symbol)
                    return false;
            }
            return true;
        }
    }
}
