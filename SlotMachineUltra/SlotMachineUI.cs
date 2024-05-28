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
            Console.WriteLine(Configurations.ChooseBetMessage);
            foreach (var choice in Enum.GetValues(typeof(BetChoice)))
            {
                Console.WriteLine($"{(int)choice}. {choice}");
            }

            while (true)
            {
                Console.Write(Configurations.EnterChoiceMessage);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int selectedChoice) && Enum.IsDefined(typeof(BetChoice), selectedChoice))
                {
                    return (BetChoice)selectedChoice;
                }
                Console.WriteLine(Configurations.InvalidChoiceMessage);
            }
        }

        public static int GetWagerPerLine(int playerMoney, int maxPerLine)
        {
            while (true)
            {
                Console.Write(string.Format(Configurations.EnterWagerMessage, maxPerLine));
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int wagerPerLine) && wagerPerLine >= 1 && wagerPerLine <= maxPerLine)
                {
                    return wagerPerLine;
                }
                Console.WriteLine(Configurations.InvalidWagerMessage);
            }
        }

        public static void DisplayResult(int[,] grid, int winnings, int totalWager, BetChoice betChoice)
        {
            Console.WriteLine(Configurations.SlotGridMessage);
            for (int i = 0; i < Configurations.GridSize; i++)
            {
                for (int j = 0; j < Configurations.GridSize; j++)
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine(string.Format(Configurations.WinningsMessage, winnings, totalWager, betChoice));
        }

        public static void DisplayGameRulesAndPayouts()
        {
            Console.WriteLine(Configurations.GameRulesMessage);
        }

        public static void DisplayGameOver()
        {
            Console.WriteLine(Configurations.GameOverMessage);
        }

        public static bool PlayAgain()
        {
            Console.Write(Configurations.PlayAgainMessage);
            string? choice = Console.ReadLine()?.ToLower();
            return choice == "y" || choice == "yes";
        }
    }
}
