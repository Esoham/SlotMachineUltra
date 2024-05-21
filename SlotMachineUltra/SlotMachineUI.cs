using System;
namespace SlotMachine
{
    public static class SlotMachineUI
    {
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static BetChoice GetPlayerChoice()
        {
            while (true)
            {
                DisplayMessage("Choose your bet:");
                foreach (BetChoice option in Enum.GetValues(typeof(BetChoice)))
                {
                    DisplayMessage($"{(int)option}. {option}");
                }

                if (int.TryParse(Console.ReadLine(), out int selectedChoice) && Enum.IsDefined(typeof(BetChoice), selectedChoice))
                {
                    return (BetChoice)selectedChoice;
                }

                DisplayMessage("Invalid choice. Please try again.");
            }
        }

        public static int GetWagerPerLine(int maxBetPerLine)
        {
            while (true)
            {
                DisplayMessage($"Enter your wager per line (max ${maxBetPerLine}): ");
                if (int.TryParse(Console.ReadLine(), out int wagerPerLine) && wagerPerLine >= 1 && wagerPerLine <= maxBetPerLine)
                {
                    return wagerPerLine;
                }

                DisplayMessage("Invalid wager. Please try again.");
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

        public static void DisplayRoundResult(int winnings, int wager)
        {
            if (winnings > 0)
            {
                DisplayMessage($"You won ${winnings}! Your total wager was ${wager}.");
            }
            else
            {
                DisplayMessage($"You did not win anything. Your total wager was ${wager}.");
            }
            DisplayMessage(string.Empty);
        }
    }
}
