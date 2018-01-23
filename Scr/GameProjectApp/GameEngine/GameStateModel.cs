using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class GameStateModel
    {
        public BoardState Board { get; set; }
        public List<Player> Player { get; set; }
        public int[] DiceRoll { get; set; }

        public GameStateModel(GameInstruction instruction)
        {
            Board = new BoardState("replace with enum that matches function");            
        }
    }
}
