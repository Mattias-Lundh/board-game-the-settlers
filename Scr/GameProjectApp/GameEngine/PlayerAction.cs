using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

        public void PlaceThief(int location)
        {
            Game.Board.Thief = location;
            Game.Events.GameLog.Add("The Thief has been moved");
        }

        public void EndTurn()
        {
            Game.Events.GameLog.Add(Game.ActivePlayer.Name + " ends the turn");
            if (Game.Players.IndexOf(Game.ActivePlayer) == Game.Players.Count -1)
            {
                Game.ActivePlayer = Game.Players[0];
            }
            else
            {
                Game.ActivePlayer = Game.Players[Game.Players.IndexOf(Game.ActivePlayer) + 1];
            }
        }
        
        public void Steal(Player player)
        {
            if (player.Inventory.HandSize > 0)
            {
                Random random = new Random();
                bool foundCard = false;
                while (!foundCard)
                {
                    switch (random.Next(0, 4))
                    {
                        case 0:
                            if (player.Inventory.Wool > 1)
                            {
                                Game.ActivePlayer.Inventory.Wool += 1;
                                player.Inventory.Wool -= 1;
                                Game.Events.GameLog.Add(Game.ActivePlayer.Name + " steals a resource from " + player.Name);
                                foundCard = true;
                            }
                            break;
                        case 1:
                            if (player.Inventory.Brick > 1)
                            {
                                Game.ActivePlayer.Inventory.Brick += 1;
                                player.Inventory.Brick -= 1;
                                Game.Events.GameLog.Add(Game.ActivePlayer.Name + " steals a resource from " + player.Name);
                                foundCard = true;
                            }
                            break;
                        case 2:
                            if (player.Inventory.Ore > 1)
                            {
                                Game.ActivePlayer.Inventory.Ore += 1;
                                player.Inventory.Ore -= 1;
                                Game.Events.GameLog.Add(Game.ActivePlayer.Name + " steals a resource from " + player.Name);
                                foundCard = true;
                            }
                            break;
                        case 3:
                            if (player.Inventory.Lumber > 1)
                            {
                                Game.ActivePlayer.Inventory.Lumber += 1;
                                player.Inventory.Lumber -= 1;
                                Game.Events.GameLog.Add(Game.ActivePlayer.Name + " steals a resource from " + player.Name);
                                foundCard = true;
                            }
                            break;
                        case 4:
                            if (player.Inventory.Grain > 1)
                            {
                                Game.ActivePlayer.Inventory.Grain += 1;
                                player.Inventory.Grain -= 1;
                                Game.Events.GameLog.Add(Game.ActivePlayer.Name + " steals a resource from " + player.Name);
                                foundCard = true;
                            }
                            break;
                    }
                }
            }
            else
            {
                Game.Events.GameLog.Add(player.Name + " has nothing to steal");
            }
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

            Game.Events.Offer = offer;
            Game.Events.Request = request;
            Game.Events.TradeMessage = message;
        }

        public void OfferTrade(int[] offer, int[] request, Player player)
        {
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

            string message = player.Name + " will trade" + offerConcat + ". if you provide" + requestConcat;

            Game.Events.Deal = new TradeDeal(player, offer, request);
            Game.Events.Deal.Message = message;
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

        public int LongestRoadLength(Player player)
        {
            int result = 0;
            for (int i = 0; i < Game.Board.Road.Length; i++)
            {
                if (Game.Board.Road[i] != null)
                {
                    if (Game.Board.Road[i] == player)
                    {
                        result = Math.Max(result, MeasureRoad(i, player));
                    }
                }
            }

            return result;
        }

        private int MeasureRoad(int road, Player player)
        {
            int result = 0;
            List<int> Visited = new List<int>();
            Visited.Add(road);
            result = BuildRoads(GetRoadNeighbourData(), Visited, player).Count;
            return result;
        }

        private List<int> BuildRoads(Dictionary<int, List<int>> neighbourData, List<int> visited, Player player)
        {
            List<int> result = visited;
            int currentLocation = visited[visited.Count - 1];
            for (int i = 2; i < neighbourData[currentLocation].Count; i++)
            {

                if (!visited.Contains(neighbourData[currentLocation][i]) &&
                    Game.Board.Road[neighbourData[currentLocation][i]] != null &&
                    Game.Board.Road[neighbourData[currentLocation][i]] == player &&
                    (Game.Board.City[FindCommonNeighbourCity(currentLocation, neighbourData[currentLocation][i], neighbourData)] == null ||
                    Game.Board.City[FindCommonNeighbourCity(currentLocation, neighbourData[currentLocation][i], neighbourData)] == player))
                {
                    List<int> followRoad = visited;
                    followRoad.Add(neighbourData[currentLocation][i]);
                    followRoad = BuildRoads(neighbourData, followRoad, player);
                    if (result.Count < followRoad.Count)
                    {
                        result = followRoad;
                    }
                }
            }
            return result;
        }

        private int FindCommonNeighbourCity(int road1, int road2, Dictionary<int, List<int>> neighbourData)
        {
            int result;

            if (neighbourData[road1][0] == neighbourData[road2][0])
            {
                result = neighbourData[road1][0];
            }
            else if (neighbourData[road1][1] == neighbourData[road2][0])
            {
                result = neighbourData[road1][1];
            }
            else if (neighbourData[road1][1] == neighbourData[road2][1])
            {
                result = neighbourData[road1][1];
            }
            else if (neighbourData[road1][0] == neighbourData[road2][1])
            {
                result = neighbourData[road1][0];
            }
            else
            {
                throw new Exception("your data file 'longestRoad.txt' is corrupt");
            }



            return result;
        }

        private Dictionary<int, List<int>> GetRoadNeighbourData()
        {
            // format of List<int>....
            //  [0] city 1                     a city that neighbours the road represented by the dictionary key
            //  [1] city 2                     another city that neighbours the road represented by the dictionary key
            //  [2] road 1                     a road that neig..... yeah you get it
            //  [3] road 2                     .....
            //  [4] road 3  (optional)         .....
            //  [5] road 4  (optional)         .....


            Dictionary<int, List<int>> result = new Dictionary<int, List<int>>();

            string[] data = File.ReadAllLines(Environment.CurrentDirectory + @"/GameEngine/data/longestRoad.txt");

            for (int i = 0; i < data.Length; i++)
            {
                int city1 = Convert.ToInt32(data[i].Substring(data[i].IndexOf('-') - 1, 2).Replace("#", ""));
                int city2 = Convert.ToInt32(data[i].Substring(data[i].IndexOf('-') + 1));

                List<int> neighbours = new List<int>();
                string[] rawNeighbours = data[i].Split(',');
                for (int j = 0; j < rawNeighbours.Length - 1; j++)
                {
                    if (j == 0)
                    {
                        neighbours.Add(Convert.ToInt32(rawNeighbours[j].Substring(rawNeighbours[j].IndexOf(':') + 1)));
                    }
                    else if (j == rawNeighbours.Length - 1)
                    {
                        neighbours.Add(Convert.ToInt32(rawNeighbours[j].Substring(0, rawNeighbours[j].IndexOf('#') - 1)));

                    }
                    else
                    {
                        neighbours.Add(Convert.ToInt32(rawNeighbours[j]));

                    }
                }

                List<int> finalvalues = new List<int>();
                finalvalues.Add(city1);
                finalvalues.Add(city2);

                foreach (int num in neighbours)
                {
                    finalvalues.Add(num);
                }
                result.Add(i, finalvalues);
            }

            return result;
        }
    }
}
