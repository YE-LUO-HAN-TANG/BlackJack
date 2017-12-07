using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using BlackJack.Models;

namespace BlackJack.UI
{
    public class CardConventer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tempCard = (PokerCard) value;
            if (tempCard.Open == false)
            {
                var url = "../Resource/PokerBackGround/Blue.png";
                var imageuri = new Uri(url, UriKind.Relative);
                return new BitmapImage(imageuri);
            }
            else
            {
                var CardSuit = tempCard.Suit;
                var tempSuit = "";
                if (CardSuit == PokerCard.Suits.Heart)
                    tempSuit = "H";
                else if (CardSuit == PokerCard.Suits.Club)
                    tempSuit = "C";
                else if (CardSuit == PokerCard.Suits.Spade)
                    tempSuit = "S";
                else if (CardSuit == PokerCard.Suits.Diamond)
                    tempSuit = "D";
                var url = "../Resource/Poker/" + tempSuit + tempCard.Points + ".png";
                var imageuri = new Uri(url, UriKind.Relative);
                return new BitmapImage(imageuri);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}