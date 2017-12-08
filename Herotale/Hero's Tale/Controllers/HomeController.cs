using System;
using Herotale.Contexts;
using Herotale.Models;
using Herotale.MSSQL_Repositories;
using System.Web.Mvc;
using System.Web.Security;

namespace Herotale.Controllers
{
    [Authorize]
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
        public ActionResult Index(Input cmd)
        {
            if (cmd != null)
            {
                Input command = new Input
                {
                    Message = cmd.Message,
                    Char = Session["Character"] as Character
                };

                Chaa = command.Char;

                Logic log = new Logic();
				Story str = log.Hub(command);

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

