using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class GameInstruction
    {
        public Guid GameId { get; set; }
        public List<string> NewGamePlayers { get; set; }        
        public Game Game
        {
            get
            {
                return Program.FindGame(GameId);
            }

        }
        public List<int> RoadChange { get; set; }
        public List<int> SettlementChange { get; set; }
        public List<int> CityChange { get; set; }
    
        private GameStateModel GetNewState()
        {
            return Game.State;
        }
    }
}