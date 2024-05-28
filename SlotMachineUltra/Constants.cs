using System.Collections.Generic;

namespace SlotMachine
{
    /// <summary>
    /// Class containing constants used throughout the slot machine game.
    /// </summary>
    public static class Constants
    {
        public const int GRID_SIZE = 3;
        public const int STARTING_PLAYER_MONEY = 100;
        public const int SYMBOL_MIN = 1;
        public const int SYMBOL_MAX = 6;
        public const int MAX_BET_MULTIPLIER = 5;

        public const string WELCOME_MESSAGE = "Welcome to the Slot Machine Game!";
        public const string GAME_OVER_MESSAGE = "Game Over! You've run out of money.";
        public const string INVALID_WAGER_MESSAGE = "Invalid wager. Please try again.";

        /// <summary>
        /// Nested dictionary representing the payout values for different bet choices and symbol combinations.
        /// The outer dictionary key represents the BetChoice, and the inner dictionary key represents the number of consecutive symbols.
        /// </summary>
        public static readonly Dictionary<BetChoice, Dictionary<int, int>> PAYOUTS = new Dictionary<BetChoice, Dictionary<int, int>>
        {
            { BetChoice.CenterHorizontalLine, new Dictionary<int, int> { { 3, 2 } } },
            { BetChoice.AllHorizontalLines, new Dictionary<int, int> { { 3, 3 } } },
            { BetChoice.AllVerticalLines, new Dictionary<int, int> { { 3, 4 } } },
            { BetChoice.BothDiagonals, new Dictionary<int, int> { { 3, 5 } } },
            { BetChoice.AllLines, new Dictionary<int, int> { { 3, 10 } } }
        };
    }
}
