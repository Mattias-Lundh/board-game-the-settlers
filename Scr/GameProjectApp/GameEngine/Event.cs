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
        public List<string> CounterTradeMessages { get; set; }
        public Player TradeParticipant { get; set; }

        public Event()
        {
            GameLog = new List<string>();
            CounterTradeMessages = new List<string>();
        }
    }
}
