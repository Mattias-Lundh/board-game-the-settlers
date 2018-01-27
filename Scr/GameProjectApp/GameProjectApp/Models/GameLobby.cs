using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameProjectApp.Models
{
    public class GameLobby
    {
        public Guid Id { get; set; }
        public List<User> Participants { get; set; }
        public int requiredPlayers = 1;
        public bool Started { get; set; } 
        public GameEngine.BoardState.BoardOptions Template { get; set; }
        public GameLobby(User host)
        {
            Id = Guid.NewGuid();
            Participants = new List<User> { host };
            Started = false;
        }        
    }
}