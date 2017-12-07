namespace BlackJack.Models
{
    public class BJHouse
    {
        public BJHouse(int HouseId)
        {
            Id = HouseId;
            table = new BJTable();
            Dealer = new BJDealer();
            Dealer.Table = table;
        }

        public int Id { get; }

        public BJTable table { get; }

        public BJDealer Dealer { get; }
    }
}