using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Event
    {
        public bool Setup { get; set; }
        public bool SetupCollect { get; set; }
        public int SetupCounter { get; set; }
        public List<string> GameLog { get; set; }
        public string TradeMessage { get; set; }
        public List<TradeDeal> Deals { get; set; }
        public int[] Offer { get; set; }
        public int[] Request { get; set; }
        public bool ThiefLock { get; set; }
        public int[] DiceResult { get; set; }
        public Dictionary<Player,int[]> TurnReward { get; set; }
        public bool PayDay { get; set; }

        public Event(int setupCounter)
        {
            GameLog = new List<string>();
            Setup = true;
            SetupCollect = false;
            SetupCounter = setupCounter;
        }
    }
}
