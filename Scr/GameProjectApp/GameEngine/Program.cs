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
                    if(instruction.RoadChange != null)
                    {
                        foreach(int roadLocation in instruction.RoadChange)
                        {
                            PlayerAction action = new PlayerAction(model);
                            action.BuildRoad(roadLocation);
                            model.Events.GameLog.Add(model.ActivePlayer.Name + " built a new road");
                        }
                    }
                    //build Settlement
                    if (instruction.SettlementChange != null)
                    {
                        foreach (int SettlementLocation in instruction.SettlementChange)
                        {
                            PlayerAction action = new PlayerAction(model);
                            action.BuildSettlement(SettlementLocation);
                            model.Events.GameLog.Add(model.ActivePlayer.Name + " built a new Settlement");
                        }
                    }
                    //build City
                    if (instruction.CityChange != null)
                    {
                        foreach (int CityLocation in instruction.CityChange)
                        {
                            PlayerAction action = new PlayerAction(model);
                            action.BuildCity(CityLocation);
                            model.Events.GameLog.Add(model.ActivePlayer.Name + " built a new City");
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
            foreach(GameStateModel game in Games)
            {
                if(game.Id == id)
                {
                    return game;
                }
            }
            throw new Exception("Invalid id");
        }
    }
}
