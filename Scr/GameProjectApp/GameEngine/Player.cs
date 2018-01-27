using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameEngine
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Color Color { get; set; }
        public Inventory Inventory { get; set; }
        public bool[] TradePerk { get; set; }
        public int Soldier { get; set; }
        public bool HasLargestArmy { get; set; }
        public int LongestRoad { get; set; }
        public bool HasLongestRoad { get; set; }
        public int VictoryPoints { get; set; }
        public int HiddenVictoryPoints { get; set; }
        public int TotalVictoryPoints
        {
            get
            {
                int sum = 0;
                if (HasLargestArmy)
                {
                    sum += 2;
                }
                if (HasLongestRoad)
                {
                    sum += 2;
                }

                return VictoryPoints + HiddenVictoryPoints + sum;
            }
        }

        public Player(string name, string id, Color color)
        {
            //new player settings
            Id = id;
            Name = name;
            Color = color;
            Inventory = new Inventory();
            Soldier = 0;
            HasLargestArmy = false;
            LongestRoad = 0;
            HasLongestRoad = false;
            VictoryPoints = 0;
            TradePerk = new bool[] { false, false, false, false, false, false };
        }
    }
}
