using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameEngine;

namespace GameProjectApp.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
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

        private GameStateModel UpdateGame(GameInstruction instruction)
        {
            return Program.ExecuteInstruction(instruction);
        }

        private GameInstruction CreateGameInstruction(FormCollection collection)
        {

            return new GameInstruction();
        }

        private GameInstruction NormalGameInstruction(FormCollection collection)
        {
            return new GameInstruction();
        }
    }
}