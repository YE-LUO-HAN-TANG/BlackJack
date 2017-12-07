using System;

namespace BlackJack.Models
{
    internal class Poker
    {
        private static int PokerNums;
        private readonly PokerCard[] PokerCards;

        //用于发牌的随机数生成
        private readonly Random rdm = new Random();

        public Poker()
        {
            PokerNums = 1;
            PokerCards = new PokerCard[52 * PokerNums];
            //初始化52张扑克牌
            var temp = 0;
            for (var k = 0; k < PokerNums; k++)
            for (var i = 0; i < 4; i++)
            for (var j = 1; j < 14; j++)
            {
                PokerCards[temp].Suit = (PokerCard.Suits) i;
                PokerCards[temp++].Points = j;
            }

            //将扑克牌数量设置为52乘几副扑克牌
            RemainingNums = 52 * PokerNums;
        }

        public Poker(int tablePokerNums)
        {
            PokerNums = tablePokerNums;
            PokerCards = new PokerCard[52 * PokerNums];
            //初始化52张扑克牌
            var temp = 0;
            for (var k = 0; k < PokerNums; k++)
            for (var i = 0; i < 4; i++)
            for (var j = 1; j < 14; j++)
                PokerCards[temp++] = new PokerCard((PokerCard.Suits) i, j);

            //将扑克牌数量设置为52乘几副扑克牌
            RemainingNums = 52 * PokerNums;
        }

        public int RemainingNums { get; private set; }

        public PokerCard DealCard()
        {
            var CardNumber = rdm.Next(RemainingNums);
            var tempPokerCard = PokerCards[CardNumber];
            RemainingNums--;
            if (CardNumber != RemainingNums)
            {
                PokerCards[CardNumber] = PokerCards[RemainingNums];
                PokerCards[RemainingNums] = tempPokerCard;
            }
            return tempPokerCard;
        }

        public void Shuffle()
        {
            RemainingNums = 52 * PokerNums;
        }
    }
}