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
        public Player Me(string id)
        {

            foreach(Player player in Players)
            {
                if(player.Id == id)
                {
                    return player;
                }
             
            }
            throw new Exception("Player is not in game");
        }

        public GameStateModel(GameInstruction instruction)
        {
            //new id
            Id = instruction.GameId;
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
            Events = new Event(instruction.NewGamePlayers.Count);
            Bank = new Bank();

        }
    }
}
