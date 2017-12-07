namespace BlackJack.Models
{
    public class PokerCard : BJNotify
    {
        public enum Suits
        {
            Heart, //红桃
            Spade, //黑桃
            Club, //梅花
            Diamond //方块
        }

        private bool PokerCardOpen;
        private int PokerCardPoints;
        private Suits PokerCardSuit;

        public PokerCard(Suits newSuit, int Point)
        {
            PokerCardSuit = newSuit;
            PokerCardPoints = Point;
        }

        public Suits Suit
        {
            get
            {
                if (PokerCardOpen)
                    return PokerCardSuit;
                throw new BlackJackException("禁止查看扣住的牌");
            }
            set => PokerCardSuit = value;
        }

        public bool Open
        {
            get => PokerCardOpen;
            set
            {
                PokerCardOpen = value;
                Notify("Open");
            }
        }

        public int Points
        {
            get
            {
                if (PokerCardOpen)
                    return PokerCardPoints;
                throw new BlackJackException("禁止查看扣住的牌");
            }
            set
            {
                if (value > 0 && value < 14)
                    PokerCardPoints = value;
                else
                    throw new BlackJackException("扑克牌点数非法");
            }
        }

        public int OwnerID { get; set; }

        public int OwnerGetPoint(int OwnerId)
        {
            if (OwnerID == OwnerId)
                return PokerCardPoints;
            throw new BlackJackException("您无权查看该牌");
        }
    }
}