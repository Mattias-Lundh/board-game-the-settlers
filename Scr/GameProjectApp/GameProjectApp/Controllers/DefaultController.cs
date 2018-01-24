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
        // GET: Default
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GameLogin()
        {

            return View();
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
            GameLobby lobby = new GameLobby();
            Session["Player"] = "";
            lobby.Id = Session.SessionID;

            lobby.Message = collection["textbox1"];

            return View(lobby);
        }

        private GameStateModel UpdateGame(GameInstruction instruction)
        {
            return Program.ExecuteInstruction(instruction);
        }

        private GameInstruction CreateGameInstruction(FormCollection collection)
        {
            GameInstruction result = new GameInstruction();
            for (int i = 1; i <= Convert.ToInt32(collection["PlayerCount"]); i++)
            {
                result.NewGamePlayers.Add(collection["Player" + i + "Name"]);

            }

            return result;
        }

        private GameInstruction NormalGameInstruction(FormCollection collection)
        {
            return new GameInstruction();
        }
    }
}