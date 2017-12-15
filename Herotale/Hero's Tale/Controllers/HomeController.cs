using Herotale.Contexts;
using Herotale.Models;
using Herotale.MSSQL_Repositories;
using Herotale.ViewModels;
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
		public ActionResult Index(StoryViewModel mod)
		{
			if (mod.Inp != null)
			{
				Input command = new Input
				{
					Message = mod.Inp.Message,
					Char = Session["Character"] as Character
				};
				
				Logic log = new Logic();
				StoryViewModel str = log.Hub(command);

				ModelState.Clear();

				return View("Index", str);
			}
			ModelState.Clear();
			return View(new Input());
		}

		[HttpGet]
		public ActionResult CharacterCreation()
		{
			return View(new CharacterViewModel());
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult CharacterCreation(CharacterViewModel Chaa)
		{
			TicketAuth ticket = new TicketAuth();
			Chaa.Acc.Id = ticket.Decrypt();
			Chaa = CharCon.Calculate(Chaa);

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
