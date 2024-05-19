public static class Constants
{
    public const int GRID_SIZE = 3;
    public const int STARTING_PLAYER_MONEY = 100;
    public static readonly Dictionary<int, int> PAYOUTS = new Dictionary<int, int>
    {
        {1, 2}, {2, 3}, {3, 4}, {4, 5}, {5, 10}
    };
}
