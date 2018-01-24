using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Bank
    {
        public enum DevelopmentCard { Soldier, VictoryPoint, BuildRoad, Monopoly, YearOfPlenty };
        public int WoolBank { get; set; }
        public int BrickBank { get; set; }
        public int OreBank { get; set; }
        public int LumberBank { get; set; }
        public int GrainBank { get; set; }
        public List<DevelopmentCard> DevelopmentCardDeck { get; set; }

        public Bank()
        {
            WoolBank = 19;
            BrickBank = 19;
            OreBank = 19;
            LumberBank = 19;
            GrainBank = 19;
            DevelopmentCardDeck = LoadNewDevelopmentDeck();

        }

        private List<DevelopmentCard> LoadNewDevelopmentDeck()
        {
            //create a new deck and shuffle according to rule book
            return new List<DevelopmentCard>();
        }
    }
}
