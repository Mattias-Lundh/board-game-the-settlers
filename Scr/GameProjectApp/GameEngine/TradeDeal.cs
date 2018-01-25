using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class TradeDeal
    {
        public Player Merchant { get; set; }
        public int[] Offer { get; set; }
        public int[] Request { get; set; }
        public string Message { get; set; }

        public TradeDeal(Player merchant, int[] offer, int[] request)
        {
            Merchant = merchant;
            Offer = offer;
            Request = request;
        }
    }

   
}
