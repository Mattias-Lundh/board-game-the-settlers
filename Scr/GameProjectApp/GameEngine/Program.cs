using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Program
    {
        private static List<GameStateModel> Games { get; set; }

        public static GameStateModel ExecuteInstruction(GameInstruction instruction)
        {
            GameStateModel model;

            switch (instruction.Type)
            {
                case GameInstruction.InstructionType.newGame:
                    model = new GameStateModel(instruction);
                    break;

                case GameInstruction.InstructionType.normal:
                    model = FindGame(instruction.GameId);
                    PlayerAction action = new PlayerAction(model);
                    //build road
                    if (instruction.RoadChange != null)
                    {
                        foreach (int roadLocation in instruction.RoadChange)
                        {
                            action.BuildRoad(roadLocation);
                        }
                    }
                    //build Settlement
                    if (instruction.SettlementChange != null)
                    {
                        foreach (int SettlementLocation in instruction.SettlementChange)
                        {
                            action.BuildSettlement(SettlementLocation);
                        }
                    }
                    //build City
                    if (instruction.CityChange != null)
                    {
                        foreach (int CityLocation in instruction.CityChange)
                        {
                            action.BuildCity(CityLocation);
                        }
                    }
                    //development card
                    if (instruction.BuyDevelopmentCard)
                    {
                        action.BuyDevelopmentCard();
                    }

                    switch (instruction.UseDevelopmentCard)
                    {
                        case Bank.DevelopmentCard.BuildRoad:
                            model.ActivePlayer.Inventory.Brick += 2;
                            model.ActivePlayer.Inventory.Lumber += 2;
                            model.ActivePlayer.Inventory.DevelopmentCards.Remove(Bank.DevelopmentCard.BuildRoad);
                            model.Events.GameLog.Add(model.ActivePlayer.Name + " Uses Build road Development Card");
                            break;
                        case Bank.DevelopmentCard.Monopoly:
                            int amount = 0;
                            foreach (Player player in model.Players)
                            {
                                amount += action.TakeResources(instruction.Monopoly, player);
                            }
                            int[] resources = new int[5];
                            resources[instruction.Monopoly] = amount;
                            action.AddResources(resources, model.ActivePlayer);
                            model.ActivePlayer.Inventory.DevelopmentCards.Remove(Bank.DevelopmentCard.Monopoly);
                            model.Events.GameLog.Add(model.ActivePlayer.Name + " Uses Monopoly Development Card");
                            break;
                        case Bank.DevelopmentCard.Soldier:
                            model.ActivePlayer.Soldier += 1;
                            model.ActivePlayer.Inventory.DevelopmentCards.Remove(Bank.DevelopmentCard.Soldier);
                            model.Events.GameLog.Add(model.ActivePlayer.Name + " Uses Soldier Development Card");
                            break;
                        case Bank.DevelopmentCard.YearOfPlenty:
                            action.AddResources(instruction.YearOfPlenty, model.ActivePlayer);
                            action.ReduceBankResources(instruction.YearOfPlenty);
                            model.ActivePlayer.Inventory.DevelopmentCards.Remove(Bank.DevelopmentCard.YearOfPlenty);
                            model.Events.GameLog.Add(model.ActivePlayer.Name + " Uses Year Of Plenty Development Card");
                            break;
                        default:
                            // no development card action requested
                            break;
                    }

                    if (instruction.TradeOffer != null)
                    {
                        //Other player is willing to trade
                        if (instruction.TradeCounterOffer)
                        {
                            action.OfferTrade(instruction.TradeOffer, instruction.TradeAccept, instruction.TradeWith);
                        }
                        //player makes a bank trade
                        else if (instruction.BankTrade)
                        {
                            action.TradeWithBank(instruction.TradeOffer, instruction.TradeAccept);
                        }
                        //player make a public trade offer
                        else if (instruction.TradeWith == null)
                        {
                            action.OfferTrade(instruction.TradeOffer, instruction.TradeAccept);
                        }
                        //player confirms an excisting trade offer
                        else
                        {
                            action.AcceptTrade(instruction.TradeOffer, instruction.TradeAccept, instruction.TradeWith);
                        }
                    }

                    if (instruction.Thief)
                    {
                        model.Events.ThiefLock = false;
                        //thief moves
                        action.PlaceThief(instruction.ThiefLocation);
                        if (instruction.ThiefVictim != null)
                        {//thief steals                            
                            action.Steal(instruction.ThiefVictim);
                        }
                    }

                    //player end turn
                    if (instruction.EndTurn)
                    {
                        action.EndTurn();
                        action.RollDice();
                        model.Events.PayDay = true;
                    }
                    else
                    {
                        model.Events.PayDay = false;
                    }

                    break;

                default:
                    throw new Exception("unknown instruction type");
            }

            return model;
        }

        public static GameStateModel FindGame(Guid id)
        {
            foreach (GameStateModel game in Games)
            {
                if (game.Id == id)
                {
                    return game;
                }
            }
            throw new Exception("Invalid id");
        }
    }
}
