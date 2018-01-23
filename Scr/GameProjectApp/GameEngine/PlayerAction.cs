using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class PlayerAction
    {
        private Game Game { get; set; }
        public PlayerAction(Guid GameId)
        {
            Game = Program.FindGame(GameId);
        }

        public void BuildRoad(int location)
        {
            Game.State.Board.Road[location] = Game.ActivePlayer;
        }

        public void BuildSettlement(int location)
        {
            Game.State.Board.Settlement[location] = Game.ActivePlayer;
        }

        public void BuildCity(int location)
        {
            Game.State.Board.City[location] = Game.ActivePlayer;
        }

        public void RollDice()
        {
            //incomplete
            Game.State.DiceRoll = new int[3];
        }

        public void OfferTrade(int[] offer, int[] request)
        {
            //Wool [0]
            //Brick [1]
            //Ore [2]
            //Lumber [3]
            //Grain [4]
            
        }

        public void AcceptTrade()
        {

        }

        public void TradeWithBank()
        {

        }

        public void BuyDevelopmentCard()
        {
            //Game.ActivePlayer.Inventory.DevelopmentCards.Add()
        }
    }
}
