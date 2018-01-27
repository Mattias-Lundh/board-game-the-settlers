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

        // GET: Default
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

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

        [HttpPost]
        public ActionResult Game(FormCollection collection)
        {
            GameInstruction instruction;
            switch (collection["FormType"])
            {
                case "newGame":
                    instruction = CreateGameInstruction(collection);
                    break;
                case "Instruction":
                    instruction = NormalGameInstruction(collection);
                    break;
                default:
                    throw new Exception("something went wrong");
            }
            GameStateModel model = UpdateGame(instruction);
            return View(model);
        }


        public ActionResult GameLobby(FormCollection collection)
        {

            if(GetUserName(Session.SessionID) != collection["userName"])
            {
                ViewBag.Message = "invalid User Name";
                ViewBag.UserName = GetUserName(Session.SessionID);
                return View("Login");
            }

            //Users user = new Users();

            //if (collection["poke"] == "refresh")
            //{
            //    if (!MetaData.Me(Session.SessionID).NewGameState)
            //    {
            //        //stop exectuting code
            //        //dont return
            //        //abort
            //        //break
            //        //ignore request
            //        return new HttpStatusCodeResult(304, "Not Modified");
            //    }
            //}
            //else
            //{
            //    if (MetaData.Me(Session.SessionID) != null)
            //    {
            //        user = MetaData.Me(Session.SessionID);
            //    }
            //    else
            //    {
            //        Session["Player"] = "participating";
            //        user.Id = Session.SessionID;
            //        MetaData.Users.Add(user);

            //    }

            //    user.NewGameState = false;

            //    foreach (Users u in MetaData.NotMe(Session.SessionID))
            //    {
            //        u.NewGameState = true;
            //    }
            //}
            //return View(MetaData.Me(Session.SessionID));
            return View();
        }

        public bool StartGame(Game game)
        {
            if (game.Participants.Count >= game.requiredPlayers)
            {
                game.Participants = (List<User>)Randomize(game.Participants);
                return true;
            }
            else
            {
                return false;
            }
        }

        private IEnumerable<T> Randomize<T>(IEnumerable<T> source)
        {
            Random rnd = new Random();
            return source.OrderBy<T, int>((item) => rnd.Next());
        }


        private GameStateModel UpdateGame(GameInstruction instruction)
        {
            return Program.ExecuteInstruction(instruction);
        }

        private GameInstruction CreateGameInstruction(FormCollection collection)
        {
            // --- required input ---
            //PlayerCount
            //player" + i + "Name
            //player" + i + "Id
            //template

            GameInstruction result = new GameInstruction();
            result.Type = GameInstruction.InstructionType.newGame;
            //set game Id
            result.GameId = Guid.NewGuid();
            //populate game with players
            result.NewGamePlayers = new List<string>();
            for (int i = 1; i <= Convert.ToInt32(collection["PlayerCount"]); i++)
            {
                result.NewGamePlayers.Add(collection["player" + i + "Name"]);
                result.NewGamePlayersId.Add(collection["player" + i + "Id"]);
            }
            //define setup template for board
            BoardState.BoardOptions template = BoardState.BoardOptions.tutorial;
            switch (collection["template"])
            {
                case "tutorial":
                    template = BoardState.BoardOptions.tutorial;
                    break;
                case "random":
                    template = BoardState.BoardOptions.random;
                    break;
                case "center":
                    template = BoardState.BoardOptions.center;
                    break;
            }
            result.BoardTemplate = template;

            return result;
        }

        private GameInstruction NormalGameInstruction(FormCollection collection)
        {
            return new GameInstruction();
        }
    }
}