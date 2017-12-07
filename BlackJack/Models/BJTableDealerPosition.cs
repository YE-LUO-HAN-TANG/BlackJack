namespace BlackJack.Models
{
    public class BJTableDealerPosition : BJTablePosition
    {
        public void Clear()
        {
            Cards.Clear();
            Goals = 0;
        }
    }
}