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
        public GameStateModel Game
        {
            get
            {
                return Program.FindGame(GameId);
            }
        }
        //new game
        public List<string> NewGamePlayers { get; set; }
        public List<string> NewGamePlayersId { get; set; }
        public BoardState.BoardOptions BoardTemplate { get; set; }
        //trade
        public int[] TradeOffer { get; set; }
        public int[] TradeAccept { get; set; }
        public bool BankTrade { get; set; }
        public Player TradeWith { get; set; }
        public bool TradeCounterOffer { get; set; }
        //standard actions
        public List<int> RoadChange { get; set; }
        public List<int> SettlementChange { get; set; }
        public List<int> CityChange { get; set; }
        public bool BuyDevelopmentCard { get; set; }
        public Bank.DevelopmentCard UseDevelopmentCard { get; set; }
        public int Monopoly { get; set; }
        public int[] YearOfPlenty { get; set; }

        public bool Thief { get; set; }
        public int ThiefLocation { get; set; }
        public Player ThiefVictim { get; set; }

        public bool EndTurn { get; set; }
    }
}