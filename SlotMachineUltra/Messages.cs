namespace SlotMachineUltra
{
    /// <summary>
    /// Provides message strings used in the slot machine game.
    /// </summary>
    public static class Messages
    {
        public const string WELCOME_MESSAGE = "Welcome to the Slot Machine Game!";
        public const string CHOOSE_BET_MESSAGE = "Choose your bet:";
        public const string ENTER_CHOICE_MESSAGE = "Enter your choice: ";
        public const string INVALID_CHOICE_MESSAGE = "Invalid choice. Please try again.";
        public const string ENTER_WAGER_MESSAGE = "Enter your wager per line (1 - {0}): ";
        public const string INVALID_WAGER_MESSAGE = "Invalid wager. Please enter a number between 1 and {0}.";
        public const string SLOT_GRID_MESSAGE = "Slot Machine Grid:";
        public const string WINNINGS_MESSAGE = "You won {0} with a total wager of {1} on {2}.";
        public const string GAME_RULES_MESSAGE = "Game Rules and Payouts: ...";
        public const string GAME_OVER_MESSAGE = "Game Over! Thanks for playing.";
        public const string PLAY_AGAIN_MESSAGE = "Do you want to play again? (y/n): ";
    }
}
