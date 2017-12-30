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
				Message = "",
				Char = Session["Char"] as Character
			};
			StoryViewModel mod = log.Hub(inp);
			Session["Title"] = mod.Str.Sgt.Title;

			ModelState.Clear();
			return View("Index", mod);
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
					Char = Session["Char"] as Character,
					Title = Session["Title"] as string

				};

				Logic log = new Logic();
				StoryViewModel str = log.Hub(command);
				str.Inp.Message = string.Empty;
				Session["Char"] = str.Str.Char;
				Session["Title"] = str.Str.Sgt.Title;
				ModelState.Clear();

				return View(str);
			}
			ModelState.Clear();
			return View(new Input());
		}

		[HttpGet]
		public ActionResult CharacterCreation(Account acc)
		{
			return View(new CharacterViewModel(acc));
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult CharacterCreation(CharacterViewModel Chaa)
		{
			TicketAuth ticket = new TicketAuth();
			Chaa.Acc = new Account();
			Chaa.Acc.Id = ticket.Decrypt();
			Chaa = CharCon.Calculate(Chaa);

			if (CharCon.InsertCharacter(Chaa))
			{
				Chaa.Id = CharCon.Get(Chaa.Acc).Id;
				Character ca = new Character(Chaa);
				Session["Char"] = ca;
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
