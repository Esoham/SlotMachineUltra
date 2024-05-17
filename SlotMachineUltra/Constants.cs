namespace SlotMachine
{
    public static class Constants
    {
        public const int GRID_SIZE = 3;
        public const int STARTING_PLAYER_MONEY = 100;
        public const char LINE_SEPARATOR = '|';
        public const string ROW_SEPARATOR = "\n---+---+---\n";

        // Bet lines
        public const int CENTER_HORIZONTAL_LINE = 1;
        public const int ALL_HORIZONTAL_LINES = 3;
        public const int ALL_VERTICAL_LINES = 3;
        public const int BOTH_DIAGONALS = 2;
        public const int ALL_LINES = 8;

        // Payouts
        public const int PAYOUT_CENTER_HORIZONTAL = 2;
        public const int PAYOUT_ALL_HORIZONTAL = 3;
        public const int PAYOUT_ALL_VERTICAL = 4;
        public const int PAYOUT_BOTH_DIAGONALS = 5;
        public const int PAYOUT_ALL_LINES = 10;
    }
}
