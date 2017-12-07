using System.Collections.ObjectModel;

namespace BlackJack.Models
{
    public class BJTablePosition : BJNotify
    {
        private readonly ObservableCollection<PokerCard> PCards;
        private int PGoals;

        public BJTablePosition()
        {
            PCards = new ObservableCollection<PokerCard>();
            PGoals = 0;
        }

        public int Goals
        {
            get => PGoals;
            set
            {
                PGoals = value;
                Notify("Goals");
            }
        }

        public Collection<PokerCard> Cards => PCards;

        public void AddCard(PokerCard newCard)
        {
            PCards.Add(newCard);
            Notify("Cards");
            ComputeGoals();
        }

        public void AddInFirst(PokerCard newCard)
        {
            PCards.Insert(0, newCard);
            Notify("Cards");
            ComputeGoals();
        }


        private void ComputeGoals()
        {
            var tempTen = 0;
            var sum = 0;
            var num = PCards.Count;
            for (var i = 0; i < num; i++)
                if (PCards[i].Open)
                    if (PCards[i].Points == 1)
                    {
                        tempTen++;
                        sum++;
                    }
                    else if (PCards[i].Points > 10)
                    {
                        sum += 10;
                    }
                    else
                    {
                        sum += PCards[i].Points;
                    }
            for (var i = 0; i < tempTen; i++)
                if (sum + 10 > 21)
                    break;
                else
                    sum += 10;
            PGoals = sum;
            Notify("Goals");
        }
    }
}