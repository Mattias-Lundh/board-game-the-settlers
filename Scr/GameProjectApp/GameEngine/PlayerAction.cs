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
            UpdateTradePerk(location);
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
        private void UpdateTradePerk(int location)
        {
            //Wool [0]
            //Brick [1]
            //Ore [2]
            //Lumber [3]
            //Grain [4]
            //Any [5]
            switch (location)
            {
                //3:1 trade with bank enabled
                case 0:
                case 1:
                case 14:
                case 15:
                case 26:
                case 37:
                case 47:
                case 48:
                    Game.ActivePlayer.TradePerk[5] = true;
                    break;
                //2:1 wool trade
                case 3:
                case 4:
                    Game.ActivePlayer.TradePerk[0] = true;
                    break;
                //2:1 ore trade
                case 7:
                case 17:
                    Game.ActivePlayer.TradePerk[2] = true;
                    break;
                //2:1 Grain trade
                case 28:
                case 38:
                    Game.ActivePlayer.TradePerk[4] = true;
                    break;
                //2:1 Brick trade
                case 45:
                case 46:
                    Game.ActivePlayer.TradePerk[1] = true;
                    break;
                //2:1 Lumber trade
                case 50:
                case 51:
                    Game.ActivePlayer.TradePerk[3] = true;
                    break;
                default:
                    break;
            }
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
            if (Game.Players.IndexOf(Game.ActivePlayer) == Game.Players.Count - 1)
            {
                Game.ActivePlayer = Game.Players[0];
            }
            else
            {
                Game.ActivePlayer = Game.Players[Game.Players.IndexOf(Game.ActivePlayer) + 1];
            }
        }

        public void EndSetupTurn()
        {
            Game.Events.SetupCounter -= 1;
            if (!Game.Events.SetupCollect)
            {
                if (Game.Events.SetupCounter == 0)
                {
                    Game.Events.SetupCollect = true;
                }
                else if (Game.Players.IndexOf(Game.ActivePlayer) == Game.Players.Count - 1)
                {
                    Game.ActivePlayer = Game.Players[0];
                }
                else
                {
                    Game.ActivePlayer = Game.Players[Game.Players.IndexOf(Game.ActivePlayer) + 1];
                }
            }
            else
            {
                if (Game.Events.SetupCounter <= (Game.Players.Count * -1) + 1)
                {
                    Game.Events.Setup = false;
                }
                else if (Game.Players.IndexOf(Game.ActivePlayer) == 0)
                {
                    Game.ActivePlayer = Game.Players[Game.Players.Count - 1];
                }
                else
                {
                    Game.ActivePlayer = Game.Players[Game.Players.IndexOf(Game.ActivePlayer) - 1];
                }
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
            int[] result = new int[3];
            Game.Events.DiceResult = result;
            SetTurnRewards(result[2]);

        }

        private void SetTurnRewards(int roll)
        {
            Game.Events.TurnReward = new Dictionary<Player, int[]>();
            Dictionary<Player, int[]> result = new Dictionary<Player, int[]>();

            foreach (Player player in Game.Players)
            {
                result.Add(player, new int[5] { 0, 0, 0, 0, 0 });
            }
            if (roll != 7)
            {
                List<List<int>> hexBuildings = FindHexagonBuildingLocations();

                for (int i = 0; i < 19; i++)
                {
                    if (Game.Events.DiceResult[2] == Game.Board.ResourceNumber[i])
                    {
                        foreach (int buildinglocation in hexBuildings[i])
                        {
                            if (Game.Board.Settlement[buildinglocation] != null)
                            {
                                int resourceInt = FindResource(Game.Board.HexGridImgPath[i]);
                                result[Game.Board.Settlement[buildinglocation]][resourceInt] += 1;
                            }
                            if (Game.Board.City[buildinglocation] != null)
                            {
                                int resourceInt = FindResource(Game.Board.HexGridImgPath[i]);
                                result[Game.Board.Settlement[buildinglocation]][resourceInt] += 2;
                            }
                        }
                    }
                }
            }
            Game.Events.TurnReward = result;
        }

        public int[] GenerateResourceBasket(int location)
        {
            int[] result = new int[] { 0, 0, 0, 0, 0 };
            List<List<int>> hexBuildings = FindHexagonBuildingLocations();

            for (int i = 0; i < 19; i++)
            {
                foreach (int buildinglocation in hexBuildings[i])
                {
                    if (Game.Board.Settlement[buildinglocation] != null && location == buildinglocation)
                    {
                        int resourceInt = FindResource(Game.Board.HexGridImgPath[i]);
                        result[resourceInt] += 1;
                    }
                }
            }
            return result;
        }

        private int FindResource(string path)
        {
            //Wool [0]
            //Brick [1]
            //Ore [2]
            //Lumber [3]
            //Grain [4]

            if (path.Contains("Pasture"))
            {
                return 0;
            }
            if (path.Contains("Hill"))
            {
                return 1;
            }
            if (path.Contains("Mountain"))
            {
                return 2;
            }
            if (path.Contains("Forest"))
            {
                return 3;
            }
            if (path.Contains("Field"))
            {
                return 4;
            }
            throw new Exception("something went wrong");
        }

        //List[HexId]
        //List[buildingId near hex]
        private List<List<int>> FindHexagonBuildingLocations()
        {
            List<List<int>> result = new List<List<int>>();

            string[] data = File.ReadAllLines(Environment.CurrentDirectory + @"/data/buildingHex.txt");
            foreach (string line in data)
            {
                int building1 = Convert.ToInt32(line.Substring(line.IndexOf(':') + 1, 2).Replace(",", ""));
                int building2 = building1 + 1;
                int building3 = building1 + 2;
                int building4 = Convert.ToInt32(line.Substring(line.Length - 3).Replace(",", ""));
                int building5 = building4 + 1;
                int building6 = building4 + 2;

                List<int> hex = new List<int>();
                hex.AddRange(new int[] { building1, building2, building3, building4, building5, building6 });
                result.Add(hex);
            }

            return result;
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

            Game.Events.Deals.Add(new TradeDeal(player, offer, request));
            Game.Events.Deals[Game.Events.Deals.Count - 1].Message = message;
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
            Game.Events.Deals.Clear();
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

        public void UseDevelopmentCard(Bank.DevelopmentCard card)
        {
            switch (card)
            {
                case Bank.DevelopmentCard.BuildRoad:
                    break;
                    //more
            }
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
                //made a change here
                int city1 = Convert.ToInt32(data[i].Substring(data[i].IndexOf('-') - 2, 2).Replace("#", ""));
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

        public void AddResources(int[] resources, Player player)
        {
            //Wool [0]
            //Brick [1]
            //Ore [2]
            //Lumber [3]
            //Grain [4]

            player.Inventory.Wool += resources[0];
            player.Inventory.Brick += resources[1];
            player.Inventory.Ore += resources[2];
            player.Inventory.Lumber += resources[3];
            player.Inventory.Grain += resources[4];
        }

        public int TakeResources(int resource, Player player)
        {
            int result = 0;
            switch (resource)
            {
                case 0:
                    result += player.Inventory.Wool;
                    player.Inventory.Wool = 0;
                    break;
                case 1:
                    result += player.Inventory.Brick;
                    player.Inventory.Brick = 0;
                    break;
                case 2:
                    result += player.Inventory.Ore;
                    player.Inventory.Ore = 0;
                    break;
                case 3:
                    result += player.Inventory.Lumber;
                    player.Inventory.Lumber = 0;
                    break;
                case 4:
                    result += player.Inventory.Grain;
                    player.Inventory.Grain = 0;
                    break;
            }

            return result;
        }

        public void ReduceBankResources(int[] resources)
        {
            Game.Bank.WoolBank -= resources[0];
            Game.Bank.BrickBank -= resources[1];
            Game.Bank.OreBank -= resources[2];
            Game.Bank.LumberBank -= resources[3];
            Game.Bank.GrainBank -= resources[4];
        }
    }
}
