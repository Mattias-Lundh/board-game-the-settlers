using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class GameInstruction
    {
        public enum InstructionType { newGame, normal }
        public InstructionType Type {get;set;} 
        public Guid GameId { get; set; }
        public List<string> NewGamePlayers { get; set; }
        public List<string> NewGamePlayersId { get; set; }
        public BoardState.BoardOptions BoardTemplate { get; set; }
        public GameStateModel Game
        {
            get
            {
                return Program.FindGame(GameId);
            }
        }
        public List<int> RoadChange { get; set; }
        public List<int> SettlementChange { get; set; }
        public List<int> CityChange { get; set; }
    
    }
}