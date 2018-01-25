using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Event
    {
        public List<string> GameLog { get; set; }
        public string TradeMessage { get; set; }
        public TradeDeal Deal { get; set; }
        public int[] Offer { get; set; }
        public int[] Request { get; set; }
        public bool ThiefLock { get; set; }
        public int DiceResult { get; set; }

        public Event()
        {
            GameLog = new List<string>();
        }
    }
}
