using System;

namespace SlotMachine
{
    public static class SlotMachineGame
    {
        private static readonly Random random = new Random();
        private static readonly int[,] PAYOUTS = new int[,]
        {
            { 1, 2 }, { 2, 3 }, { 3, 4 }, { 4, 5 }, { 5, 10 }
        };

        public static void StartGame()
        {
            int playerMoney = Constants.STARTING_PLAYER_MONEY;
            SlotMachineUI.DisplayMessage("Welcome to the Slot Machine Game!");

            while (playerMoney > 0)
            {
                SlotMachineUI.DisplayMessage($"Current Money: ${playerMoney}");
                BetChoice betChoice = SlotMachineUI.GetPlayerChoice();
                int linesToBet = GetLinesToBet(betChoice);
                int maxBetPerLine = playerMoney / (linesToBet * Constants.MAX_WAGER);
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
            }

            SlotMachineUI.DisplayMessage("Game Over! You've run out of money.");
        }

        private static int[,] GenerateSlotOutcomes()
        {
            int[,] grid = new int[Constants.GRID_SIZE, Constants.GRID_SIZE];
            for (int i = 0; i < Constants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Constants.GRID_SIZE; j++)
                {
                    grid[i, j] = random.Next(Constants.MIN_SLOT_NUMBER, Constants.MAX_SLOT_NUMBER);
                }
            }
            return grid;
        }

        private static int GetLinesToBet(BetChoice choice)
        {
            return choice switch
            {
                BetChoice.CenterHorizontalLine => 1,
                BetChoice.AllHorizontalLines => Constants.GRID_SIZE,
                BetChoice.AllVerticalLines => Constants.GRID_SIZE,
                BetChoice.BothDiagonals => 2,
                BetChoice.AllLines => Constants.GRID_SIZE * 2 + 2,
                _ => throw new ArgumentOutOfRangeException(nameof(choice), "Invalid betting choice"),
            };
        }

        private static int CalculateWinnings(int[,] grid, BetChoice choice, int wagerPerLine)
        {
            int winnings = 0;
            if (choice == BetChoice.CenterHorizontalLine)
            {
                winnings += CheckLine(grid, 1, wagerPerLine);
            }
            else if (choice == BetChoice.AllHorizontalLines)
            {
                for (int i = 0; i < Constants.GRID_SIZE; i++)
                {
                    winnings += CheckLine(grid, i, wagerPerLine);
                }
            }
            else if (choice == BetChoice.AllVerticalLines)
            {
                for (int j = 0; j < Constants.GRID_SIZE; j++)
                {
                    winnings += CheckColumn(grid, j, wagerPerLine);
                }
            }
            else if (choice == BetChoice.BothDiagonals)
            {
                winnings += CheckDiagonals(grid, wagerPerLine);
            }
            else if (choice == BetChoice.AllLines)
            {
                for (int i = 0; i < Constants.GRID_SIZE; i++)
                {
                    winnings += CheckLine(grid, i, wagerPerLine);
                    winnings += CheckColumn(grid, i, wagerPerLine);
                }
                winnings += CheckDiagonals(grid, wagerPerLine);
            }
            return winnings;
        }

        private static int CheckLine(int[,] grid, int line, int wagerPerLine)
        {
            for (int j = 1; j < Constants.GRID_SIZE; j++)
            {
                if (grid[line, j] != grid[line, 0]) return 0;
            }
            return wagerPerLine * PAYOUTS[grid[line, 0] - 1, 1];
        }

        private static int CheckColumn(int[,] grid, int column, int wagerPerLine)
        {
            for (int i = 1; i < Constants.GRID_SIZE; i++)
            {
                if (grid[i, column] != grid[0, column]) return 0;
            }
            return wagerPerLine * PAYOUTS[grid[0, column] - 1, 1];
        }

        private static int CheckDiagonals(int[,] grid, int wagerPerLine)
        {
            int winnings = 0;
            bool leftToRight = true, rightToLeft = true;
            for (int i = 1; i < Constants.GRID_SIZE; i++)
            {
                if (grid[i, i] != grid[0, 0]) leftToRight = false;
                if (grid[i, Constants.GRID_SIZE - i - 1] != grid[0, Constants.GRID_SIZE - 1]) rightToLeft = false;
            }
            if (leftToRight) winnings += wagerPerLine * PAYOUTS[grid[0, 0] - 1, 1];
            if (rightToLeft) winnings += wagerPerLine * PAYOUTS[grid[0, Constants.GRID_SIZE - 1] - 1, 1];
            return winnings;
        }
    }
}
