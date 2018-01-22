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
        public string Name { get; set; }
        public bool IsActivePlayer { get; set; }
        public Color Color { get; set; }
        public Inventory Inventory { get; set; }
        public int Soldier { get; set; }
        public bool HasLargestArmy { get; set; }
        public int LongestRoad { get; set; }
        public bool HasLongestRoad { get; set; }
        public int VictoryPoints { get; set; }

        public Player(string name, bool isActive, Color color)
        {
            //new player settings
            Name = name;
            Color = color;
            Inventory = new Inventory();
            IsActivePlayer = isActive;
            Soldier = 0;
            HasLargestArmy = false;
            LongestRoad = 0;
            HasLongestRoad = false;
            VictoryPoints = 0;
        }
    }
}
