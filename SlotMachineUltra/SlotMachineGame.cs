using System;

namespace SlotMachine
{
    public static class SlotMachineGame
    {
        private static readonly Random random = new Random();

        public static void StartGame()
        {
            int playerMoney = Configurations.StartingPlayerMoney;
            SlotMachineUI.DisplayMessage(Configurations.WelcomeMessage);
            SlotMachineUI.DisplayGameRulesAndPayouts();

            while (true)
            {
                // Reset player money to starting amount at the beginning of each game loop
                playerMoney = Configurations.StartingPlayerMoney;

                SlotMachineUI.DisplayMessage(string.Format(Configurations.CurrentMoneyMessage, playerMoney));
                BetChoice betChoice = SlotMachineUI.GetPlayerChoice();
                int linesToBet = GetLinesToBet(betChoice);
                int maxPerLine = playerMoney / (linesToBet * Configurations.MaxBetMultiplier);
                int wagerPerLine = SlotMachineUI.GetWagerPerLine(playerMoney, maxPerLine);

                int totalWager = wagerPerLine * linesToBet;
                if (totalWager > playerMoney)
                {
                    SlotMachineUI.DisplayMessage(Configurations.InvalidWagerMessage);
                    continue;
                }

                int[,] grid = GenerateSlotGrid();
                int winnings = CalculateWinnings(grid, betChoice, wagerPerLine);
                playerMoney += winnings - totalWager;

                SlotMachineUI.DisplayResult(grid, winnings, totalWager, betChoice);

                if (winnings == 0)
                {
                    SlotMachineUI.DisplayMessage(Configurations.NoWinMessage);
                }

                if (playerMoney <= 0)
                {
                    SlotMachineUI.DisplayGameOver();
                    if (!SlotMachineUI.PlayAgain())
                    {
                        break;  // Exit the loop and end the game if the player does not want to play again
                    }
                }
                else if (!SlotMachineUI.PlayAgain())
                {
                    break;  // Exit the loop and end the game if the player does not want to play again
                }
            }
        }

        private static int[,] GenerateSlotGrid()
        {
            int[,] grid = new int[Configurations.GridSize, Configurations.GridSize];
            for (int i = 0; i < Configurations.GridSize; i++)
            {
                for (int j = 0; j < Configurations.GridSize; j++)
                {
                    grid[i, j] = random.Next(1, Configurations.SymbolsCount + 1);
                }
            }
            return grid;
        }

        private static int CalculateWinnings(int[,] grid, BetChoice betChoice, int wagerPerLine)
        {
            int baseWinnings = CalculateBaseWinnings(grid, betChoice, wagerPerLine);

            if (random.NextDouble() < Configurations.MultiplierChance)
            {
                baseWinnings *= Configurations.MultiplierValue;
                SlotMachineUI.DisplayMessage(Configurations.MultiplierMessage);
            }

            return baseWinnings;
        }

        private static int CalculateBaseWinnings(int[,] grid, BetChoice betChoice, int wagerPerLine)
        {
            int matches = 0;

            for (int i = 0; i < Configurations.GridSize; i++)
            {
                bool match = true;
                int symbol = grid[i, 0];

                for (int j = 1; j < Configurations.GridSize; j++)
                {
                    if (grid[i, j] != symbol)
                    {
                        match = false;
                        break;
                    }
                }

                if (match)
                {
                    matches++;
                }
            }

            if (matches > 0)
            {
                return matches * wagerPerLine * Configurations.WinMultiplier;  // Example multiplier, adjust as needed
            }

            return 0;
        }

        private static int GetLinesToBet(BetChoice choice)
        {
            switch (choice)
            {
                case BetChoice.CenterHorizontalLine: return 1;
                case BetChoice.AllHorizontalLines:
                case BetChoice.AllVerticalLines: return Configurations.GridSize;
                case BetChoice.BothDiagonals: return 2;
                case BetChoice.AllLines: return 2 * Configurations.GridSize + 2; // All lines plus two diagonals
                default:
                    throw new ArgumentOutOfRangeException(nameof(choice), Configurations.InvalidBetChoiceMessage);
            }
        }
    }
}
