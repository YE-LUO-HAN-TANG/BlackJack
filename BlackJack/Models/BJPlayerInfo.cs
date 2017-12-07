namespace BlackJack.Models
{
    public class BJPlayerInfo : BJNotify
    {
        public enum gamePlayerState
        {
            Can,
            Ban,
            Done
        }

        public enum playerState
        {
            NotInGame,
            JoinInTable,
            Preparing,
            Gaming,
            Standing
        }

        private gamePlayerState Double;
        private gamePlayerState Insurance;
        private int PlayerChip;

        //游戏特殊状态
        private gamePlayerState Split;

        private gamePlayerState Surrend;

        public BJPlayerInfo()
        {
            State = playerState.NotInGame;
            Id = 1;
            PlayerChip = 500;
        }

        public int Id { get; set; }

        public int Chip
        {
            get => PlayerChip;
            set
            {
                PlayerChip = value;
                Notify("Chip");
            }
        }

        public playerState State { get; set; }
    }
}