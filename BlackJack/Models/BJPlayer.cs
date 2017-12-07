namespace BlackJack.Models
{
    public class BJPlayer
    {
        private readonly BJPlayerInfo PlayerInfo;
        private Casino currenCasino;
        private BJDealer currentDealer;
        private BJTable currentTable;

        public BJPlayer()
        {
            PlayerInfo = new BJPlayerInfo();
        }

        public int TablePlayerId { set; get; }

        public void JoinTable()
        {
            var currentHouse = currenCasino.JoinHouse();
            currentTable = currentHouse.table;
            currentDealer = currentHouse.Dealer;
            PlayerInfo.State = BJPlayerInfo.playerState.JoinInTable;
            TablePlayerId = currentTable.AddPlayer(PlayerInfo);
            AddStake(10);
        }

        public void Restart()
        {
            currentTable.Clear();
        }

        public void JoinTable(int BJHouseId)
        {
            var currentHouse = currenCasino.JoinHouse(BJHouseId);
            currentTable = currentHouse.table;
            currentDealer = currentHouse.Dealer;
            PlayerInfo.State = BJPlayerInfo.playerState.JoinInTable;
            TablePlayerId = currentTable.AddPlayer(PlayerInfo);
            AddStake(10);
        }

        public BJTable GetCurrentTable()
        {
            return currentTable;
        }

        public void GetInCasino(Casino newCasino)
        {
            currenCasino = newCasino;
        }

        public void Prepare()
        {
            if (PlayerInfo.State != BJPlayerInfo.playerState.JoinInTable)
                throw new BlackJackException("您需要先加入一个房间");
            PlayerInfo.State = BJPlayerInfo.playerState.Preparing;
            currentDealer.CheckAllPreparing();
        }

        public void Hit()
        {
            if (PlayerInfo.State != BJPlayerInfo.playerState.Gaming)
                throw new BlackJackException("您不能再加牌了");
            currentTable.AddCard(TablePlayerId, true);
            if (currentTable.AllPlayerPositons[TablePlayerId].Goals > 21)
                throw new BlackJackException("您的牌点数爆了");
        }

        public void CancelStake()
        {
            if (PlayerInfo.State == BJPlayerInfo.playerState.JoinInTable)
                currentTable.CancelStake(TablePlayerId);
            else
                throw new BlackJackException("当前您不能撤回赌注");
        }

        public void AddStake(int TablePlayerStake)
        {
            if (TablePlayerStake <= PlayerInfo.Chip)
                currentTable.AddStake(TablePlayerId, TablePlayerStake);
            else
                throw new BlackJackException("您已经没有更多的钱");
        }

        public void Stand()
        {
            if (PlayerInfo.State != BJPlayerInfo.playerState.Gaming)
                throw new BlackJackException("您的游戏尚未开始");
            PlayerInfo.State = BJPlayerInfo.playerState.Standing;
            currentDealer.CheckAllStanding();
        }
    }
}