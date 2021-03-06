﻿
using Herotale.Contexts;
using Herotale.Models;
using Herotale.MSSQL_Repositories;
using Herotale.ViewModels;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Herotale.Controllers
{

	[AllowAnonymous]
	public class AccountController : Controller
	{
		private AccountContext AccCon = new AccountContext(new MssqlAccountRep());
		private CharacterContext CharCon = new CharacterContext(new MssqlCharacterRep());

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Register(Account acc)
		{
			if (acc != null)
			{
				if (acc.Email.IsEmpty() || acc.Password.IsEmpty())
				{
					ViewBag.Message = "Please enter an Emailaddress and a Password.";
					return View();
				}

				string mailregex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
				string passregex = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$";

				bool isMailMatch = Regex.IsMatch(acc.Email, mailregex);
				bool isPassMatch = Regex.IsMatch(acc.Password, passregex);

				if (!isMailMatch)
				{
					ViewBag.Message = "Please enter a valid Emailaddress";
					return View();
				}

				if (!isPassMatch)
				{
					ViewBag.Message = "Please enter a 8-character password with atleast 1 uppercase letter, 1 lowercase letter and 1 digit";
					return View();
				}

				Account r = new Account
				{
					Email = acc.Email,
					Password = PasswordMan.Hash(acc.Password),
					Rights = false
				};

				bool register = AccCon.InsertAccount(r);

				if (register == false)
				{
					ViewBag.Message = "Your Emailaddress is already taken.";
					return View();
				}

				return RedirectToAction("Confirmation", "Account");
			}

			return View();
		}

		[HttpGet]
		public ActionResult Register()
		{
			return View();
		}

		[ValidateAntiForgeryToken]
		[HttpPost]
		public ActionResult Login(Account acc)
		{
			TicketAuth ticket = new TicketAuth();

			if (acc != null)
			{
				bool auth;
				string id = null;
				Account a = new Account();
				if (acc.Email == null || acc.Password == null)
				{
					auth = false;
				}
				else
				{
					a.Email = acc.Email;
					a.Password = PasswordMan.Hash(acc.Password);

					auth = AccCon.LoginAccount(a);
					id = AccCon.LoginId(a);
					a.Id = Convert.ToInt32(id);
					a.Rights = AccCon.GetById(a.Id).Rights;
				}
				if (auth == false)
				{
					ViewBag.Message = "This is not a registered Account. Check your Email or Password.";
					return View();
				}

				if (id != null)
				{
					HttpCookie c = ticket.Encrypt(id);
					HttpContext.Response.Cookies.Add(c);

					if(a.Rights)
					{
						return RedirectToAction("Admin", "Home");
					}

					bool HasCharacter = CharCon.CheckForCharacter(a);
					if (HasCharacter)
					{
						Character Chaa = CharCon.Get(a);
						Session["Char"] = Chaa;
						return RedirectToAction("Index", "Home");
					}
					return RedirectToAction("CharacterCreation", "Home");
				}
			}
			return View();
		}

		[HttpGet]
		public ActionResult Login()
		{
			return View();
		}

		public ActionResult Confirmation()
		{
			return View();
		}
	}
}