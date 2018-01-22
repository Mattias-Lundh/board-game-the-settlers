using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameEngine
{
    public class Game
    {
        public enum DevelopmentCard { Soldier, VictoryPoint, BuildRoad, Monopoly, YearOfPlenty };
        public Guid Id { get; set; }
        public Player ActivePlayer { get; set; }
        public List<Player> Players { get; set; }
        public GameStateModel State { get; set; }
        public int WoolBank { get; set; }
        public int BrickBank { get; set; }
        public int OreBank { get; set; }
        public int LumberBank { get; set; }
        public int GrainBank { get; set; }
        public List<DevelopmentCard> DevelopmentCardDeck { get; set; }

        public Game(Guid gameId, string[] playerNames)
        {
            Id = gameId;
            WoolBank = 19;
            BrickBank = 19;
            OreBank = 19;
            LumberBank = 19;
            GrainBank = 19;
            DevelopmentCardDeck = LoadNewDevelopmentDeck();
            PopulatePlayers(playerNames);
            ActivePlayer = Players[0];


        }

        private List<DevelopmentCard> LoadNewDevelopmentDeck()
        {
            //create a new deck and shuffle according to rule book
            return new List<DevelopmentCard>();
        }

        private void PopulatePlayers(string[] Names)
        {
            //add new players
            
        }
    }
}
