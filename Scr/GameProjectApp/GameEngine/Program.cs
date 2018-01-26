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
                    //build road
                    if (instruction.RoadChange != null)
                    {
                        foreach (int roadLocation in instruction.RoadChange)
                        {
                            PlayerAction action = new PlayerAction(model);
                            action.BuildRoad(roadLocation);
                        }
                    }
                    //build Settlement
                    if (instruction.SettlementChange != null)
                    {
                        foreach (int SettlementLocation in instruction.SettlementChange)
                        {
                            PlayerAction action = new PlayerAction(model);
                            action.BuildSettlement(SettlementLocation);
                        }
                    }
                    //build City
                    if (instruction.CityChange != null)
                    {
                        foreach (int CityLocation in instruction.CityChange)
                        {
                            PlayerAction action = new PlayerAction(model);
                            action.BuildCity(CityLocation);
                        }
                    }

                    if (instruction.TradeOffer != null)
                    {
                        //Other player is willing to trade
                        if (instruction.TradeCounterOffer)
                        {
                            PlayerAction action = new PlayerAction(model);
                            action.OfferTrade(instruction.TradeOffer, instruction.TradeAccept, instruction.TradeWith);
                        }
                        //player makes a bank trade
                        else if (instruction.BankTrade)
                        {
                            PlayerAction action = new PlayerAction(model);
                            action.TradeWithBank(instruction.TradeOffer, instruction.TradeAccept);
                        }
                        //player make a public trade offer
                        else if (instruction.TradeWith == null)
                        {
                            PlayerAction action = new PlayerAction(model);
                            action.OfferTrade(instruction.TradeOffer, instruction.TradeAccept);
                        }
                        //player confirms an excisting trade offer
                        else
                        {
                            PlayerAction action = new PlayerAction(model);
                            action.AcceptTrade(instruction.TradeOffer, instruction.TradeAccept, instruction.TradeWith);
                        }
                    }
                    
                    if (instruction.Thief)
                    {
                        model.Events.ThiefLock = false;
                        PlayerAction action = new PlayerAction(model);
                        //thief moves
                        action.PlaceThief(instruction.ThiefLocation);
                        if(instruction.ThiefVictim != null)
                        {//thief steals                            
                            action.Steal(instruction.ThiefVictim);
                        }
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
