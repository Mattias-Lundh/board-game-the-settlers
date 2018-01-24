using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class PlayerAction
    {
        private GameStateModel Game { get; set; }
        public PlayerAction(GameStateModel game)
        {
            Game = game;
        }

        public void BuildRoad(int location)
        {
            Game.Board.Road[location] = Game.ActivePlayer;
        }

        public void BuildSettlement(int location)
        {
            Game.Board.Settlement[location] = Game.ActivePlayer;
        }

        public void BuildCity(int location)
        {
            Game.Board.City[location] = Game.ActivePlayer;
        }

        public void RollDice()
        {
            //incomplete
            //Game.DiceRoll = new int[3];
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
