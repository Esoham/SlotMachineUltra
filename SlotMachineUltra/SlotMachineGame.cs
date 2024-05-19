public class SlotMachineGame
{
    private static readonly Random random = new Random();

    public static void StartGame()
    {
        int playerMoney = Constants.STARTING_PLAYER_MONEY;
        Console.WriteLine("Welcome to the Slot Machine Game!");

        while (playerMoney > 0)
        {
            Console.WriteLine($"Current Money: ${playerMoney}");
            BetChoice betChoice = SlotMachineUI.GetPlayerChoice();
            int linesToBet = SlotMachineUI.GetLinesToBet(betChoice);
            int maxBetPerLine = playerMoney / (linesToBet * 5);
            int wagerPerLine = SlotMachineUI.GetWagerPerLine(maxBetPerLine);
            int totalWager = wagerPerLine * linesToBet;

            if (totalWager > playerMoney)
            {
                Console.WriteLine("You do not have enough money for that wager.");
                continue;
            }

            playerMoney -= totalWager;
            int[,] grid = GenerateSlotOutcomes();
            SlotMachineUI.DisplayGrid(grid);
            int totalWinnings = CalculateWinnings(grid, betChoice, wagerPerLine);
            playerMoney += totalWinnings;

            SlotMachineUI.DisplayRoundResult(totalWinnings, totalWager);
        }

        Console.WriteLine("Game Over! You've run out of money.");
    }

    private static int[,] GenerateSlotOutcomes()
    {
        int[,] grid = new int[Constants.GRID_SIZE, Constants.GRID_SIZE];
        for (int i = 0; i < Constants.GRID_SIZE; i++)
        {
            for (int j = 0; j < Constants.GRID_SIZE; j++)
            {
                grid[i, j] = random.Next(1, 6);
            }
        }
        return grid;
    }

    private static int CalculateWinnings(int[,] grid, BetChoice choice, int wagerPerLine)
    {
        int winnings = 0;
        // Calculation logic based on BetChoice
        return winnings;
    }
}
