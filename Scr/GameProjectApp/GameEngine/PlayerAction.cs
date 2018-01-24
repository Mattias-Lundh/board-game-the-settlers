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
            Game.ActivePlayer.Inventory.Brick -= 1;
            Game.ActivePlayer.Inventory.Lumber -= 1;
            Game.Bank.BrickBank += 1;
            Game.Bank.LumberBank += 1;

            Game.Events.GameLog.Add(Game.ActivePlayer.Name + " builds a road");
        }

        public void BuildSettlement(int location)
        {
            Game.Board.Settlement[location] = Game.ActivePlayer;
            Game.ActivePlayer.Inventory.Grain -= 1;
            Game.ActivePlayer.Inventory.Wool -= 1;
            Game.ActivePlayer.Inventory.Brick -= 1;
            Game.ActivePlayer.Inventory.Lumber -= 1;
            Game.Bank.GrainBank += 1;
            Game.Bank.WoolBank += 1;
            Game.Bank.BrickBank += 1;
            Game.Bank.LumberBank += 1;

            Game.Events.GameLog.Add(Game.ActivePlayer.Name + " builds a settlement");

        }

        public void BuildCity(int location)
        {
            Game.Board.Settlement[location] = null;
            Game.Board.City[location] = Game.ActivePlayer;
            Game.ActivePlayer.Inventory.Grain -= 2;
            Game.ActivePlayer.Inventory.Ore -= 3;
            Game.Bank.GrainBank += 2;
            Game.Bank.OreBank += 3;

            Game.Events.GameLog.Add(Game.ActivePlayer.Name + " builds a city");

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
            string offerConcat = "";
            if (offer[0] > 0) { offerConcat = offer[0] + " Wool"; }
            if (offer[1] > 0) { offerConcat = offer[1] + " brick"; }
            if (offer[2] > 0) { offerConcat = offer[2] + " Ore"; }
            if (offer[3] > 0) { offerConcat = offer[3] + " Lumber"; }
            if (offer[4] > 0) { offerConcat = offer[4] + " Grain"; }
            string requestConcat = "";
            if (request[0] > 0) { requestConcat = offer[0] + " Wool"; }
            if (request[1] > 0) { requestConcat = offer[1] + " brick"; }
            if (request[2] > 0) { requestConcat = offer[2] + " Ore"; }
            if (request[3] > 0) { requestConcat = offer[3] + " Lumber"; }
            if (request[4] > 0) { requestConcat = offer[4] + " Grain"; }

            string message = Game.ActivePlayer.Name + " offers a trade of" + offerConcat + ". It will cost you" + requestConcat;

            Game.Events.TradeMessage = message;
        }

        public void AcceptTrade(int[] offer, int[] request, Player TradeParticipant)
        {
            Game.ActivePlayer.Inventory.Wool += request[0];
            Game.ActivePlayer.Inventory.Brick += request[1];
            Game.ActivePlayer.Inventory.Ore += request[2];
            Game.ActivePlayer.Inventory.Lumber += request[3];
            Game.ActivePlayer.Inventory.Grain += request[4];

            Game.ActivePlayer.Inventory.Wool -= offer[0];
            Game.ActivePlayer.Inventory.Brick -= offer[1];
            Game.ActivePlayer.Inventory.Ore -= offer[2];
            Game.ActivePlayer.Inventory.Lumber -= offer[3];
            Game.ActivePlayer.Inventory.Grain -= offer[4];

            TradeParticipant.Inventory.Wool += offer[0];
            TradeParticipant.Inventory.Brick += offer[1];
            TradeParticipant.Inventory.Ore += offer[2];
            TradeParticipant.Inventory.Lumber += offer[3];
            TradeParticipant.Inventory.Grain += offer[4];

            TradeParticipant.Inventory.Wool -= request[0];
            TradeParticipant.Inventory.Brick -= request[1];
            TradeParticipant.Inventory.Ore -= request[2];
            TradeParticipant.Inventory.Lumber -= request[3];
            TradeParticipant.Inventory.Grain -= request[4];

            Game.Events.GameLog.Add(Game.ActivePlayer.Name + " trades with " + TradeParticipant.Name);
        }

        public void TradeWithBank(int[] offer, int[] request)
        {

            Game.ActivePlayer.Inventory.Wool += request[0];
            Game.ActivePlayer.Inventory.Brick += request[1];
            Game.ActivePlayer.Inventory.Ore += request[2];
            Game.ActivePlayer.Inventory.Lumber += request[3];
            Game.ActivePlayer.Inventory.Grain += request[4];

            Game.ActivePlayer.Inventory.Wool -= offer[0];
            Game.ActivePlayer.Inventory.Brick -= offer[1];
            Game.ActivePlayer.Inventory.Ore -= offer[2];
            Game.ActivePlayer.Inventory.Lumber -= offer[3];
            Game.ActivePlayer.Inventory.Grain -= offer[4];

            Game.Bank.WoolBank -= request[0];
            Game.Bank.WoolBank -= request[1];
            Game.Bank.WoolBank -= request[2];
            Game.Bank.WoolBank -= request[3];
            Game.Bank.WoolBank -= request[4];

            Game.Bank.WoolBank += offer[0];
            Game.Bank.WoolBank += offer[1];
            Game.Bank.WoolBank += offer[2];
            Game.Bank.WoolBank += offer[3];
            Game.Bank.WoolBank += offer[4];

            Game.Events.GameLog.Add(Game.ActivePlayer.Name + " trades with the bank");
        }

        public void BuyDevelopmentCard()
        {
            List<Bank.DevelopmentCard> deck = Game.Bank.DevelopmentCardDeck;
            Game.ActivePlayer.Inventory.DevelopmentCards.Add(deck[0]);
            deck.RemoveAt(0);
            Game.ActivePlayer.Inventory.Wool -= 1;
            Game.ActivePlayer.Inventory.Grain -= 1;
            Game.ActivePlayer.Inventory.Ore -= 1;
            Game.Bank.WoolBank += 1;
            Game.Bank.GrainBank += 1;
            Game.Bank.OreBank += 1;

            Game.Events.GameLog.Add(Game.ActivePlayer.Name + " buys a development card");
        }
    }
}
