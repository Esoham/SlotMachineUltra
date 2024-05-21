﻿namespace SlotMachine
{
    public static class Constants
    {
        public const int GRID_SIZE = 3;
        public const int STARTING_PLAYER_MONEY = 100;
        public const int SYMBOL_MIN = 1;
        public const int SYMBOL_MAX = 6;
        public const int MAX_BET_MULTIPLIER = 5;
        public const string WELCOME_MESSAGE = "Welcome to the Slot Machine Game!";
        public const string GAME_OVER_MESSAGE = "Game Over! You've run out of money.";
        public static readonly Dictionary<int, int> PAYOUTS = new Dictionary<int, int>
        {
            {1, 2}, {2, 3}, {3, 4}, {4, 5}, {5, 10}
        };
    }
}
