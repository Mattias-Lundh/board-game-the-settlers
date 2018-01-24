using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Program
    {
        private static List<Game> Games { get; set; }

        public static GameStateModel ExecuteInstruction(GameInstruction instruction)
        {
            return new GameStateModel(instruction);
        }

        public static Guid CreateNewGame(string[] playerNames)
        {
            Games.Add(new Game(Guid.NewGuid(), playerNames));
            return Games[Games.Count - 1].Id;
        }

        public static Game FindGame(Guid id)
        {
            foreach(Game game in Games)
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
