using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Inventory
    {
        public int City { get; set; }
        public int Settlement { get; set; }
        public int Road { get; set; }
        public int Wool { get; set; }
        public int Brick { get; set; }
        public int Ore { get; set; }
        public int Lumber { get; set; }
        public int Grain { get; set; }
        public int HandSize
        {
            get
            {
                return Wool + Brick + Ore + Lumber + Grain;
            }
        }
        public List<Bank.DevelopmentCard> DevelopmentCards { get; set; }

        public Inventory()
        {
            //initial starting hand settings
            City = 4;
            Settlement = 5;
            Road = 15;
            Wool = 0;
            Brick = 0;
            Ore = 0;
            Lumber = 0;
            Grain = 0;
            DevelopmentCards = new List<Bank.DevelopmentCard>();
        }
    }
}
