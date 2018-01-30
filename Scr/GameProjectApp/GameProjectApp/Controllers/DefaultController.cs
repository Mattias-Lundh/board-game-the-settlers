using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameEngine;
using GameProjectApp.Models;

namespace GameProjectApp.Controllers
{
    public class DefaultController : Controller
    {
        public static List<User> Users { get; set; } = new List<User>();
        public static List<GameLobby> Games { get; set; } = new List<GameLobby>();
        public bool UserExists(string id)
        {
            foreach (User user in Users)
            {
                if (user.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetGameChangeFlagsForAllParticipants(Guid gameId)
        {
            foreach (User user in FindAllUsersInGame(gameId))
            {
                user.NewGameContent = true;
            }
        }

        public User FindUser(string userId)
        {
            foreach (User user in Users)
            {
                if (user.Id == userId)
                {
                    return user;
                }
            }
            throw new Exception("cant find user");
        }

        private List<User> FindAllUsersInGame(Guid gameId)
        {
            List<User> result = new List<User>();
            foreach (User user in Users)
            {
                if (user.InGameId == gameId)
                {
                    result.Add(user);
                }
            }
            return result;
        }

        public string GetUserName(string id)
        {
            foreach (User user in Users)
            {
                if (user.Id == id)
                {
                    return user.Name;
                }
            }
            return "";
        }

        public GameLobby FindGameLobby(string gameLobbyId)
        {
            foreach (GameLobby game in Games)
            {
                if (game.Id.ToString() == gameLobbyId)
                {
                    return game;
                }
            }
            throw new Exception("Game not found");
        }

        public bool GameStarted(string gameLobbyId)
        {
            if (FindGameLobby(gameLobbyId).Started)
            {
                return true;
            }
            return false;
        }

        //GET: Default 
        //                                                                                     INDEX
        public ActionResult Index()
        {
            DebugString model = new DebugString() { String = "" };
            return View(model);
        }
        //                                                                                     LOGIN                                                                      
        public ActionResult Login()
        {
            return View();
        }
        //                                                                                     SANDBOX
        public ActionResult Sandbox()
        {
            ViewBag.Id = Session.SessionID;
            ViewBag.Settlement = @"\\Content\\Images\\doodad\\Settlement.png"; //not in use
            ViewBag.City = @"\\Content\\Images\\doodad\\City.png"; //not in use
            //set up fake game for testing
            //   ----- FAKE DATA -----
            GameInstruction instruction = new GameInstruction();

            instruction.Type = GameInstruction.InstructionType.newGame;
            instruction.GameId = Guid.NewGuid();
            instruction.BoardTemplate = BoardState.BoardOptions.tutorial;

            //populate game with players
            instruction.NewGamePlayers = new List<string>();
            instruction.NewGamePlayersId = new List<string>();

            instruction.NewGamePlayers.Add("Frodo");
            instruction.NewGamePlayersId.Add(Session.SessionID);

            instruction.NewGamePlayers.Add("Sam");
            instruction.NewGamePlayersId.Add(Session.SessionID);

            instruction.NewGamePlayers.Add("Gandalf");
            instruction.NewGamePlayersId.Add(Session.SessionID);

            instruction.NewGamePlayers.Add("Gimli");
            instruction.NewGamePlayersId.Add(Session.SessionID);
            //   ----- FAKE DATA END -----

            GameStateModel model = UpdateGame(instruction);
            return View("Game", model);
        }
        
        //                                                                                     GAME
        [HttpPost]
        public ActionResult Game(FormCollection collection)
        {
            ViewBag.Id = Session.SessionID;
            Guid ThisGame = FindUser(Session.SessionID).InGameId;

            if (collection["poke"] == "refresh")
            {
                if (FindUser(collection["PlayerId"]).NewGameContent)
                {
                    return View(SettlersOfCatan.FindGame(ThisGame));
                }
                return new HttpStatusCodeResult(304, "Not Modified");
            }

            GameInstruction instruction;
            if (collection["formType"] == "normal")
            {
                instruction = NormalGameInstruction(collection);
            }
            else
            {
                throw new Exception("something went wrong");
            }

            GameStateModel model = UpdateGame(instruction);
            SetGameChangeFlagsForAllParticipants(ThisGame);
            return View(model);
        }

        //                                                                                     GAME LOBBY
        public ActionResult GameLobby(FormCollection collection)
        {
            string id = Session.SessionID;
            ViewBag.Id = Session.SessionID;
            GameLobby thisLobby = FindGameLobby(FindUser(id).InGameId.ToString());
            // start game button pressed
            if (collection["formType"] == "newGame")
            {
                GameInstruction instruction = CreateGameInstruction(thisLobby);
                GameStateModel model = UpdateGame(instruction);
                //flag game as started
                thisLobby.Started = true;
                //go to game page
                return View("Game", model);
            }
            //auto redirect when game starts
            if (collection["poke"] == "refresh")
            {
                if (!GameStarted(collection["gameId"]))
                {
                    return View(thisLobby);
                    return new HttpStatusCodeResult(304, "Not Modified");
                }
                else
                {
                    return View("Game", SettlersOfCatan.FindGame(thisLobby.Id));
                }
            }
            // joined lobby successfully
            return View(thisLobby);
        }
        //                                                                                     BROWSE LOBBY
        public ActionResult BrowseLobby(FormCollection collection)
        {           
            string id = Session.SessionID;
            ViewBag.Id = Session.SessionID;
            if (collection["userName"] =="")
            {
                ViewBag.Message = "invalid User Name";
                return View("Login");
            }
            else
            {
                Session["player"] = "registered";
                User user = new User(Session.SessionID, collection["userName"]);
                Users.Add(user);
            }            

            //create game
            if (collection["createLobby"] != null)
            {
                Guid lobbyId = Guid.NewGuid();
                GameLobby lobby = new GameLobby(FindUser(id), lobbyId);
                //link player to game
                FindUser(id).InGameId = lobbyId;
                // set lobby values
                lobby.Name = collection["createLobby"];
                switch (collection["template"])
                {
                    case "tutorial":
                        lobby.Template = BoardState.BoardOptions.tutorial;
                        break;
                    case "center":
                        lobby.Template = BoardState.BoardOptions.center;
                        break;
                    case "random":
                        lobby.Template = BoardState.BoardOptions.random;
                        break;
                    default:
                        throw new Exception("invalid template");
                }
                lobby.RequiredPlayers = Convert.ToInt32(collection["players"]);
                Games.Add(lobby);
                return View("GameLobby", lobby);
            }
            //Join game
            ViewBag.JoinLobby = collection["joinLobby"];
            if (collection["joinLobby"] != null && collection["joinLobby"] != "unselected")
            {
                //link player to game
                GameLobby lobby = FindGameLobby(collection["joinLobby"]);
                FindUser(id).InGameId = lobby.Id;
                lobby.Participants.Add(FindUser(id));
                return View("GameLobby",lobby);
            }            
            return View(Games);
        }

        private IEnumerable<T> Randomize<T>(IEnumerable<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy<T, int>((item) => rnd.Next());
        }

        private GameStateModel UpdateGame(GameInstruction instruction)
        {
            return SettlersOfCatan.ExecuteInstruction(instruction);
        }

        private GameInstruction CreateGameInstruction(GameLobby gameLobby)
        {
            List<User> tempList = new List<User>();
            tempList.AddRange(Randomize(gameLobby.Participants));
            gameLobby.Participants.Clear();
            gameLobby.Participants.AddRange(tempList);

            GameInstruction result = new GameInstruction
            {
                Type = GameInstruction.InstructionType.newGame,
                GameId = gameLobby.Id,
                BoardTemplate = gameLobby.Template
            };

            //populate game with players
            result.NewGamePlayers = new List<string>();
            result.NewGamePlayersId = new List<string>();
            for (int i = 0; i < gameLobby.Participants.Count; i++)
            {
                result.NewGamePlayers.Add(gameLobby.Participants[i].Name);
                result.NewGamePlayersId.Add(gameLobby.Participants[i].Id);
            }
            return result;
        }

        private GameInstruction NormalGameInstruction(FormCollection collection)
        {
            GameInstruction result = new GameInstruction() { };
            result.Type = GameInstruction.InstructionType.normal;
            result.GameId = FindUser(Session.SessionID).InGameId;
            if (collection["setup"] == "true")
            {
                result.RoadChange.Add(Convert.ToInt32(collection["roadChange"]));
                result.SettlementChange.Add(Convert.ToInt32(collection["settlementChange"]));
                result.StartingResources = Convert.ToBoolean(collection["collectResources"]);
            }
            return result;
        }
    }
}