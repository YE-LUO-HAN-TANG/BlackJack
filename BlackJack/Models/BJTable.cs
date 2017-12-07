using System.Collections.Generic;

namespace BlackJack.Models
{
    public class BJTable
    {
        private static readonly int maxPlayerNum = 5; //本桌最大玩家数量


        //扑克牌
        private readonly Poker TablePoker;

        private readonly int TablePokerNums; //本桌扑克牌有几套

        //牌桌自身的属性
        private bool TableIsGaming; //本桌是否正在游戏

        public BJTable()
        {
            TableIsGaming = false;
            CurrentPlayerNum = 0;
            TablePokerNums = 1;
            TablePoker = new Poker(TablePokerNums);
        }

        public BJTable(int PokerNums)
        {
            TableIsGaming = false;
            CurrentPlayerNum = 0;
            TablePokerNums = PokerNums;
            TablePoker = new Poker(TablePokerNums);
        }

        public List<BJTablePlayerPositon> AllPlayerPositons { get; } = new List<BJTablePlayerPositon>();

        public BJTableDealerPosition Dealerposition { get; } = new BJTableDealerPosition();

        public bool IsGaming
        {
            get => TableIsGaming;
            set => TableIsGaming = true;
        }

        public int CurrentPlayerNum { get; private set; }

        public int[] AllGoals()
        {
            var goals = new int[CurrentPlayerNum + 1];
            goals[0] = Dealerposition.Goals;
            for (var i = 0; i < CurrentPlayerNum; i++)
                goals[i + 1] = AllPlayerPositons[i].Goals;
            return goals;
        }

        public void Clear()
        {
            TablePoker.Shuffle();
            Dealerposition.Clear();
            for (var i = 0; i < CurrentPlayerNum; i++)
                AllPlayerPositons[i].Clear();
        }

        public bool IsFull()
        {
            if (CurrentPlayerNum <= maxPlayerNum)
                return false;
            return true;
        }

        public bool IsAllPreparing()
        {
            for (var i = 0; i < CurrentPlayerNum; i++)
                if (AllPlayerPositons[i].PlayerInfo.State != BJPlayerInfo.playerState.Preparing)
                    return false;
            return true;
        }

        public bool IsAllStanding()
        {
            for (var i = 0; i < CurrentPlayerNum; i++)
                if (AllPlayerPositons[i].PlayerInfo.State != BJPlayerInfo.playerState.Standing)
                    return false;
            return true;
        }

        public void BeginGame()
        {
            IsGaming = true;
            for (var i = 0; i < CurrentPlayerNum; i++)
                AllPlayerPositons[i].PlayerInfo.State = BJPlayerInfo.playerState.Gaming;
        }

        //将牌加入牌桌
        public void AddCard(int TablePlayerId, bool IsOpen)
        {
            if (TablePlayerId == -1)
            {
                var TablePokerCard = TablePoker.DealCard();
                TablePokerCard.Open = IsOpen;
                TablePokerCard.OwnerID = TablePlayerId;
                Dealerposition.AddCard(TablePokerCard);
            }
            else if (TablePlayerId >= 0 && TablePlayerId < maxPlayerNum)
            {
                var TablePokerCard = TablePoker.DealCard();
                TablePokerCard.Open = IsOpen;
                TablePokerCard.OwnerID = TablePlayerId;
                AllPlayerPositons[TablePlayerId].AddCard(TablePokerCard);
            }
            else
            {
                throw new BlackJackException("此桌上的用户ID非法");
            }
        }

        //增加赌注
        public void AddStake(int TablePlayerId, int TablePlayerStake)
        {
            if (TablePlayerId >= 0 && TablePlayerId < maxPlayerNum)
                AllPlayerPositons[TablePlayerId].AddStake(TablePlayerStake);
            else
                throw new BlackJackException("此桌上的用户ID非法");
        }

        //撤销赌注
        public void CancelStake(int TablePlayerId)
        {
            if (TablePlayerId >= 0 && TablePlayerId < maxPlayerNum)
                AllPlayerPositons[TablePlayerId].CancelStake();
            else
                throw new BlackJackException("此桌上的用户ID非法");
        }

        //增加玩家
        public int AddPlayer(BJPlayerInfo newPlayer)
        {
            if (CurrentPlayerNum == maxPlayerNum)
                throw new BlackJackException("此牌桌玩家已满");
            if (TableIsGaming)
                throw new BlackJackException("正在游戏中，请稍后加入");
            for (var i = 0; i < CurrentPlayerNum; i++)
                if (AllPlayerPositons[i].PlayerInfo.Id == newPlayer.Id)
                    throw new BlackJackException("您已在此牌桌中");
            var newPlayerPositon = new BJTablePlayerPositon(newPlayer);
            AllPlayerPositons.Add(newPlayerPositon);
            CurrentPlayerNum = AllPlayerPositons.Count;
            return CurrentPlayerNum - 1;
        }

        //
        public void DetermineStake(int PlayerId, BJTablePlayerPositon.BJGameResult GameResult)
        {
            AllPlayerPositons[PlayerId].DetermineStake(GameResult);
        }
    }
}