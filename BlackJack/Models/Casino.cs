using System.Collections.Generic;

namespace BlackJack.Models
{
    public class Casino
    {
        private readonly List<BJHouse> CasinoHouse = new List<BJHouse>();

        public BJHouse JoinHouse()
        {
            var num = CasinoHouse.Count;
            for (var i = 0; i < num; i++)
                if (CasinoHouse[i].table.IsGaming == false && CasinoHouse[i].table.IsFull() == false)
                    return CasinoHouse[i];
            var newHouse = new BJHouse(num + 1);
            CasinoHouse.Add(newHouse);
            return newHouse;
        }

        public BJHouse JoinHouse(int BJtableId)
        {
            var num = CasinoHouse.Count;
            for (var i = 0; i < num; i++)
                if (CasinoHouse[i].Id == BJtableId)
                    return CasinoHouse[i];
            throw new BlackJackException("您正在试图进入一个不存在的房间");
        }
    }
}