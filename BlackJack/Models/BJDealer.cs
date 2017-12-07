namespace BlackJack.Models
{
    public class BJDealer
    {
        public BJTable Table { get; set; }

        public void CheckAllPreparing()
        {
            var result = Table.IsAllPreparing();
            if (result)
            {
                Table.BeginGame();
                DealCard(2);
            }
        }

        public void CheckAllStanding()
        {
            var result = Table.IsAllStanding();
            if (result)
            {
                OpenFirstCard();
                AddCard();
                Determine();
            }
        }

        private void AddCard()
        {
            var Goals = Table.AllGoals();
            var num = Goals.Length;
            var Playerfail = 0;
            for (var i = 1; i < num; i++)
                if (Goals[i] > 21)
                    Playerfail++;
            if (Playerfail < num - 1)
                while (Table.Dealerposition.Goals < 17)
                    Table.AddCard(-1, true);
        }

        private void OpenFirstCard()
        {
            var tempCard = Table.Dealerposition.Cards[0];
            Table.Dealerposition.Cards.Remove(tempCard);
            tempCard.Open = true;
            Table.Dealerposition.AddInFirst(tempCard);
        }

        public void Determine()
        {
            var Goals = Table.AllGoals();
            var num = Goals.Length;
            for (var i = 1; i < num; i++)
                if (Goals[i] < 22)
                    if (Goals[0] < 22)
                        if (Goals[0] > Goals[i])
                            Table.DetermineStake(i - 1, BJTablePlayerPositon.BJGameResult.lose);
                        else if (Goals[0] == Goals[i])
                            Table.DetermineStake(i - 1, BJTablePlayerPositon.BJGameResult.push);
                        else
                            Table.DetermineStake(i - 1, BJTablePlayerPositon.BJGameResult.win);
                    else
                        Table.DetermineStake(i - 1, BJTablePlayerPositon.BJGameResult.win);

                else
                    Table.DetermineStake(i - 1, BJTablePlayerPositon.BJGameResult.lose);
        }

        public void DealCard(int OnePlayerCardsNum)
        {
            var currentPlayerNum = Table.CurrentPlayerNum;

            for (var j = 0; j < OnePlayerCardsNum; j++)
            for (var i = -1; i < currentPlayerNum; i++)
                if (j == 0 && i == -1)
                    Table.AddCard(i, false);
                else
                    Table.AddCard(i, true);
        }
    }
}