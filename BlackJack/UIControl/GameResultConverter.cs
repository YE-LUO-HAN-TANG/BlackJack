using System;
using System.Globalization;
using System.Windows.Data;
using BlackJack.Models;

namespace BlackJack.UIControl
{
    internal class GameResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var tempResult = (BJTablePlayerPositon.BJGameResult) value;
            if (tempResult == BJTablePlayerPositon.BJGameResult.win)
                return "胜利";
            if (tempResult == BJTablePlayerPositon.BJGameResult.lose)
                return "失败";
            return "平局";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}