using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameProjectApp.Models
{
    public class GameLobby
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<User> Participants { get; set; }
        public int RequiredPlayers { get; set; }
        public bool Started { get; set; } 
        public GameEngine.BoardState.BoardOptions Template { get; set; }
        public GameLobby(User host, Guid id)
        {
            Id = id;
            Participants = new List<User> { host };
            Started = false;
        }        
    }
}