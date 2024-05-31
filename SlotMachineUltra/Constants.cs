namespace SlotMachineUltra
{
    /// <summary>
    /// Provides constant values used in the slot machine game.
    /// </summary>
    public static class Constants
    {
        public const int DefaultGridSize = 3;
        public const int DefaultWager = 100; // Updated to 100
        public const string WelcomeMessage = "Welcome to the Slot Machine Game!"; // Added WelcomeMessage
        public const string ChooseBetMessage = "Choose your bet:";
        public const string EnterChoiceMessage = "Enter your choice: ";
        public const string InvalidChoiceMessage = "Invalid choice. Please try again.";
        public const string EnterWagerMessage = "Enter your wager per line (1 - {0}): ";
        public const string InvalidWagerMessage = "Invalid wager. Please enter a number between 1 and {0}.";
        public const string SlotGridMessage = "Slot Machine Grid:";
        public const string WinningsMessage = "You won {0} with a total wager of {1} on {2}.";
        public const string GameRulesMessage = "Game Rules and Payouts: ...";
        public const string GameOverMessage = "Game Over! Thanks for playing.";
        public const string PlayAgainMessage = "Do you want to play again? (y/n): ";
        public const int GridSize = 3; // This should be dynamic or appropriately set based on your game's requirement
        public const int WinMultiplier = 5; // Added missing constant
    }
}
