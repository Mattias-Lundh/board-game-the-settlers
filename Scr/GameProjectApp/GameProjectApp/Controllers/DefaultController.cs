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
            foreach(User user in FindAllUsersInGame(gameId))
            {
                user.NewGameContent = true;
            }
        }

        public User FindUser(string userId)
        {
            foreach(User user in Users)
            {
                if(user.Id == userId)
                {
                    return user;
                }
            }
            throw new Exception("cant find user");
        }

        private List<User> FindAllUsersInGame(Guid gameId)
        {
            List<User> result = new List<User>();
            foreach(User user in Users)
            {
                if(user.InGameId == gameId)
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
                    if (game.Started)
                    {
                        return game;
                    }
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

            return View();
        }
        //                                                                                     LOGIN                                                                      
        public ActionResult Login()
        {
            return View();
        }
        //                                                                                     SANDBOX
        public ActionResult Sandbox()
        {

            return View();
        }

        public ActionResult Register(FormCollection collection)
        {

            if (UserExists(Session.SessionID))
            {
                ViewBag.Message = "You are already registered";
                ViewBag.UserName = GetUserName(Session.SessionID);
                return View("Login");
            }

            if (collection["name"] == "" || collection["email"] == "")
            {
                ViewBag.Message = "Please Fill in text boxes";
                ViewBag.UserName = GetUserName(Session.SessionID);
                return View("Login");
            }
            else
            {
                Session["Player"] = "created";
                Users.Add(new User(Session.SessionID, collection["name"], collection["email"]));
                ViewBag.Message = "New user created";
            }
            ViewBag.UserName = GetUserName(Session.SessionID);
            return View("Login");
        }
        //                                                                                     GAME
        [HttpPost]
        public ActionResult Game(FormCollection collection)
        {
            Guid ThisGame = Guid.Parse(collection["gameId"]);

            if(collection["poke"] == "refresh")
            {
                if(FindUser(collection["PlayerId"]).NewGameContent)
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
            //check if user is registered
            if (GetUserName(Session.SessionID) != collection["userName"])
            {
                ViewBag.Message = "invalid User Name";
                ViewBag.UserName = GetUserName(Session.SessionID);
                return View("Login");
            }
            // start game button pressed
            if (collection["formType"] == "newGame")
            {
                GameLobby thisLobby = FindGameLobby(collection["gameId"]);
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
                    return new HttpStatusCodeResult(304, "Not Modified");
                }
                else
                {
                    return View("Game", SettlersOfCatan.FindGame(FindGameLobby(collection["gameId"]).Id));
                }
            }
            // joined lobby successfully
            return View();
        }
        //                                                                                     BROWSE LOBBY
        public ActionResult BrowseLobby(FormCollection collection)
        {
            return View();
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
            gameLobby.Participants = (List<User>)Randomize(gameLobby.Participants);

            GameInstruction result = new GameInstruction
            {
                Type = GameInstruction.InstructionType.newGame,
                GameId = gameLobby.Id,
                BoardTemplate = gameLobby.Template                
            };

            //populate game with players
            result.NewGamePlayers = new List<string>();
            for (int i = 1; i <= gameLobby.Participants.Count; i++)
            {
                result.NewGamePlayers.Add(gameLobby.Participants[i].Name);
                result.NewGamePlayersId.Add(gameLobby.Participants[i].Id);
            }
            return result;
        }

        private GameInstruction NormalGameInstruction(FormCollection collection)
        {
            return new GameInstruction();
        }
    }
}