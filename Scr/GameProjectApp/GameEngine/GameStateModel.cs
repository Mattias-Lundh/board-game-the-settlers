using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace GameEngine
{
    public class GameStateModel
    {
        public Guid Id { get; set; }
        public BoardState Board { get; set; }
        public Bank Bank { get; set; }
        public List<Player> Players { get; set; }
        public Player ActivePlayer { get; set; }
        public Event Events { get; set; }

        public GameStateModel(GameInstruction instruction)
        {
            //new id
            Id = Guid.NewGuid();
            //new board
            Board = new BoardState(instruction.BoardTemplate);
            //populate player list
            Players = new List<Player>();
            List<Color> civColor = new List<Color> { Color.Blue, Color.Red, Color.Orange, Color.White };
            for (int i = 0; i < instruction.NewGamePlayers.Count; i++)
            {
                Players.Add(new Player(instruction.NewGamePlayers[i], instruction.NewGamePlayersId[i], civColor[i]));
            }
            //set starting player
            Random rdm = new Random();
            ActivePlayer = Players[rdm.Next(0, instruction.NewGamePlayers.Count - 1)];

            Events.GameLog.Add("A new game has begun");
            Events.GameLog.Add(ActivePlayer.Name + " turn to deploy 1 settlement and 1 road");
        }
    }
}
