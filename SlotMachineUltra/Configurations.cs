using System.Collections.Generic;
namespace SlotMachine
{
    public static class Configurations
    {
        public const int StartingPlayerMoney = 100;
        public const int SymbolsCount = 7; // Number of different symbols on the slot machine
        public const int GridSize = 3; // 3x3 grid
        public const int MaxBetMultiplier = 5; // Maximum multiplier for betting calculations
        public const double MultiplierChance = 0.1; // 10% chance to trigger a multiplier
        public const int MultiplierValue = 2; // Multiplier value
        public const int WinMultiplier = 5; // Example multiplier for winnings

        public const string WelcomeMessage = "Welcome to the Slot Machine Game!";
        public const string ChooseBetMessage = "Choose your bet:";
        public const string EnterChoiceMessage = "Enter your choice: ";
        public const string InvalidChoiceMessage = "Invalid choice, please try again.";
        public const string EnterWagerMessage = "Enter your wager per line (1 to {0}): ";
        public const string InvalidWagerMessage = "Invalid wager. Please try again.";
        public const string SlotGridMessage = "Slot Grid:";
        public const string WinningsMessage = "Winnings: ${0}, Total Wager: ${1}, Bet Choice: {2}";
        public const string NoWinMessage = "You did not win anything.";
        public const string GameOverMessage = "Game Over! You've run out of money.";
        public const string PlayAgainMessage = "Do you want to play again? (y/n): ";
        public const string CurrentMoneyMessage = "Current Money: ${0}";
        public const string InvalidBetChoiceMessage = "Invalid betting choice";
        public const string MultiplierMessage = "Multiplier Activated! Winnings doubled.";
        public const string GameRulesMessage = "Game Rules and Payouts: Match three symbols to win. Different lines provide different multipliers.";

        // Example of a payout table based on symbol matches
        public static readonly Dictionary<BetChoice, Dictionary<int, int>> Payouts = new Dictionary<BetChoice, Dictionary<int, int>>()
        {
            { BetChoice.AllLines, new Dictionary<int, int> { {3, 10}, {2, 5} } }
        };
    }
}
