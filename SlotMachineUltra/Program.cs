using System;
namespace SlotMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SlotMachineUI.DisplayMessage(Constants.WelcomeMessage);
                SlotMachineUI.DisplayGameRulesAndPayouts();

                bool continuePlaying = true;
                while (continuePlaying)
                {
                    int playerMoney = Constants.StartingPlayerMoney;

                    while (playerMoney > 0)
                    {
                        SlotMachineUI.DisplayMessage(string.Format(Constants.CurrentMoneyMessage, playerMoney));
                        BetChoice betChoice = SlotMachineUI.GetPlayerChoice();
                        int linesToBet = SlotMachineGame.GetLinesToBet(betChoice);
                        int maxPerLine = playerMoney / (linesToBet * Constants.MaxBetMultiplier);
                        int wagerPerLine = SlotMachineUI.GetWagerPerLine(playerMoney, maxPerLine);

                        int totalWager = wagerPerLine * linesToBet;
                        if (totalWager > playerMoney)
                        {
                            SlotMachineUI.DisplayMessage(Constants.InvalidWagerMessage);
                            continue;
                        }

                        int[,] grid = SlotMachineGame.GenerateSlotGrid();
                        int winnings = SlotMachineGame.CalculateWinnings(grid, betChoice, wagerPerLine);
                        playerMoney += winnings - totalWager;

                        SlotMachineUI.DisplayResult(grid, winnings, totalWager, betChoice);

                        if (winnings == 0)
                        {
                            SlotMachineUI.DisplayMessage(Constants.NoWinMessage);
                        }
                    }

                    SlotMachineUI.DisplayGameOver();
                    continuePlaying = SlotMachineUI.PlayAgain();
                    if (continuePlaying)
                    {
                        playerMoney = Constants.StartingPlayerMoney;
                    }
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine("Invalid input: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred: " + ex.Message);
            }
        }
    }
}


