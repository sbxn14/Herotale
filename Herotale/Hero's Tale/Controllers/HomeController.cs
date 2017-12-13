<<<<<<< HEAD
﻿using Herotale.Contexts;
using Herotale.Models;
using Herotale.MSSQL_Repositories;
using Herotale.ViewModels;
=======
﻿using System;
using Herotale.Contexts;
using Herotale.Models;
using Herotale.MSSQL_Repositories;
>>>>>>> master
using System.Web.Mvc;
using System.Web.Security;

namespace Herotale.Controllers
{
<<<<<<< HEAD
	[Authorize]
=======
    [Authorize]
>>>>>>> master
    public class HomeController : Controller
    {
        private CharacterContext CharCon = new CharacterContext(new MssqlCharacterRep());
        private Character Chaa { get; set; }

        [HttpGet]
        public ActionResult Index()
        {
            Logic log = new Logic();

            Input inp = new Input
            {
                Char = Session["Character"] as Character
            };

            ModelState.Clear();
            return View("Index", log.Hub(inp));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
<<<<<<< HEAD
        public ActionResult Index(StoryViewModel mod)
        {
            if (mod.Inp != null)
            {
				Input command = new Input
				{
					Message = mod.Inp.Message,
=======
        public ActionResult Index(Input cmd)
        {
            if (cmd != null)
            {
                Input command = new Input
                {
                    Message = cmd.Message,
>>>>>>> master
                    Char = Session["Character"] as Character
                };

                Chaa = command.Char;

                Logic log = new Logic();
<<<<<<< HEAD
				StoryViewModel str = log.Hub(command);
=======
				Story str = log.Hub(command);
>>>>>>> master

                ModelState.Clear();

                return View("Index", str);
            }
            ModelState.Clear();
            return View(new Input());
        }

        [HttpGet]
        public ActionResult CharacterCreation()
        {
            return View(new Character());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult CharacterCreation(Character Chaa)
        {
            TicketAuth ticket = new TicketAuth();
            Chaa.AccId = ticket.Decrypt();
            Chaa = CharCon.Create(Chaa);

            if (CharCon.InsertCharacter(Chaa))
            {
                Session["Character"] = Chaa;
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login", "Account");
        }
    }
}

