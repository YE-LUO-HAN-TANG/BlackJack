using System;

namespace BlackJack
{
    public class BlackJackException : ApplicationException
    {
        public BlackJackException(string message) : base(message)
        {
        }
    }
}