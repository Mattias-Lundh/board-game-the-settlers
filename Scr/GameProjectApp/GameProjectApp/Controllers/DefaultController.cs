using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameProjectApp.Models;

namespace GameProjectApp.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            ManageViewModels x = new ManageViewModels();
            //HttpCookieCollection.
            return View(x);
        }
    }
}