namespace SlotMachine
{
    public static class SlotMachineUI
    {
        public static void StartGame()
        {
            int playerMoney = Constants.STARTING_PLAYER_MONEY;
            DisplayMessage(Constants.WELCOME_MESSAGE);

            while (playerMoney > 0)
            {
                DisplayMessage($"Current Money: ${playerMoney}");
                BetChoice betChoice = GetPlayerChoice();
                int linesToBet = GetLinesToBet(betChoice);
                int maxBetPerLine = playerMoney / (linesToBet * Constants.MAX_BET_MULTIPLIER);
                int wagerPerLine = GetWagerPerLine(maxBetPerLine);
                int totalWager = wagerPerLine * linesToBet;

                if (totalWager > playerMoney)
                {
                    DisplayMessage(Constants.NOT_ENOUGH_MONEY_MESSAGE);
                    continue;
                }

                playerMoney -= totalWager;
                int[,] grid = SlotMachineGame.GenerateSlotOutcomes();
                DisplayGrid(grid);
                int totalWinnings = SlotMachineGame.CalculateWinnings(grid, betChoice, wagerPerLine);
                playerMoney += totalWinnings;

                DisplayRoundResult(totalWinnings, totalWager);
            }

            DisplayMessage(Constants.GAME_OVER_MESSAGE);
        }

        private static BetChoice GetPlayerChoice()
        {
            while (true)
            {
                DisplayMessage("Choose your bet:");
                foreach (BetChoice option in Enum.GetValues(typeof(BetChoice)))
                {
                    DisplayMessage($"{(int)option}. {option.ToString().Replace('_', ' ')}");
                }

                if (int.TryParse(Console.ReadLine(), out int selectedChoice) && Enum.IsDefined(typeof(BetChoice), selectedChoice))
                {
                    return (BetChoice)selectedChoice;
                }

                DisplayMessage(Constants.INVALID_WAGER_MESSAGE);
            }
        }

        private static int GetWagerPerLine(int maxBetPerLine)
        {
            while (true)
            {
                DisplayMessage($"Enter your wager per line (max ${maxBetPerLine}): ");
                if (int.TryParse(Console.ReadLine(), out int wagerPerLine) && wagerPerLine >= 1 && wagerPerLine <= maxBetPerLine)
                {
                    return wagerPerLine;
                }

                DisplayMessage(Constants.INVALID_WAGER_MESSAGE);
            }
        }

        public static void DisplayGrid(int[,] grid)
        {
            DisplayMessage("Slot Machine Outcome:");
            for (int i = 0; i < Constants.GRID_SIZE; i++)
            {
                for (int j = 0; j < Constants.GRID_SIZE; j++)
                {
                    Console.Write($"{grid[i, j]}{(j < Constants.GRID_SIZE - 1 ? " | " : "")}");
                }
                DisplayMessage(i < Constants.GRID_SIZE - 1 ? "\n---+---+---" : "\n");
            }
        }

        private static void DisplayRoundResult(int winnings, int wager)
        {
            if (winnings > 0)
            {
                DisplayMessage($"You won ${winnings}! Your total wager was ${wager}.");
            }
            else
            {
                DisplayMessage($"You did not win anything. Your total wager was ${wager}.");
            }
            DisplayMessage();
        }

        public static void DisplayMessage(string message = "")
        {
            Console.WriteLine(message);
        }

        private static int GetLinesToBet(BetChoice choice)
        {
            switch (choice)
            {
                case BetChoice.CenterHorizontalLine: return 1;
                case BetChoice.AllHorizontalLines:
                case BetChoice.AllVerticalLines: return Constants.GRID_SIZE;
                case BetChoice.BothDiagonals: return 2;
                case BetChoice.AllLines: return Constants.GRID_SIZE * 2 + 2;
                default: throw new ArgumentOutOfRangeException(nameof(choice), "Invalid betting choice");
            }
        }
    }
}
