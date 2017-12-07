namespace BlackJack.Models
{
    public class BJTablePlayerPositon : BJTablePosition
    {
        public enum BJGameResult
        {
            win,
            lose,
            push,
            gaming
        }

        private int PStake;

        public BJTablePlayerPositon(BJPlayerInfo newPlayerInfo)
        {
            GameResult = BJGameResult.gaming;
            PlayerInfo = newPlayerInfo;
            PStake = 0;
        }

        public BJGameResult GameResult { get; private set; }

        public BJPlayerInfo PlayerInfo { get; set; }

        public int Stake
        {
            get => PStake;
            set
            {
                PStake = value;
                Notify("Stake");
            }
        }

        public void Clear()
        {
            PStake = 0;
            Cards.Clear();
            Goals = 0;
            GameResult = BJGameResult.gaming;
            Notify("Stake");
        }

        public void CancelStake()
        {
            PlayerInfo.Chip += PStake - 10;
            PStake = 10;
            Notify("Stake");
        }

        public void AddStake(int newStake)
        {
            PlayerInfo.Chip -= newStake;
            PStake += newStake;
            Notify("Stake");
            Notify("PlayerInfo");
        }

        public void DetermineStake(BJGameResult GameResult)
        {
            if (GameResult == BJGameResult.push)
                PlayerInfo.Chip += PStake;
            else if (GameResult == BJGameResult.win)
                PlayerInfo.Chip += PStake * 2;
            this.GameResult = GameResult;
            Notify("GameResult");
            PlayerInfo.State = BJPlayerInfo.playerState.JoinInTable;
        }
    }
}