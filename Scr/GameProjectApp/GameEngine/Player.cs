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
        public int Soldier { get; set; }
        public bool HasLargestArmy { get; set; }
        public int LongestRoad { get; set; }
        public bool HasLongestRoad { get; set; }
        public int VictoryPoints { get; set; }

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
        }
    }
}
