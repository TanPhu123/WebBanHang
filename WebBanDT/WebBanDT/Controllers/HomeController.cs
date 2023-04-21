using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebBanDT.Context;
using WebBanDT.Models;

namespace WebBanDT.Controllers
{
	public class HomeController : Controller
	{
		WebMayTinhEntities objWebMayTinhEntities = new WebMayTinhEntities();
		public ActionResult Index()
		{
			HomeModel objHomeModel = new HomeModel();
			objHomeModel.ListCategory = objWebMayTinhEntities.Categories.ToList();
			objHomeModel.ListProduct = objWebMayTinhEntities.Products.ToList();
			/*objHomeModel.ListMenu = objWebMayTinhEntities.Menus.ToList();*/

			return View(objHomeModel);
		}

		//GET: Register

		public ActionResult Register()
		{
			return View();
		}

		//POST: Register
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Register(User _user)
		{
			if (ModelState.IsValid)
			{
				var check = objWebMayTinhEntities.Users.FirstOrDefault(s => s.email == _user.email);
				if (check == null)
				{
					_user.password = GetMD5(_user.password);
					objWebMayTinhEntities.Configuration.ValidateOnSaveEnabled = false;
					objWebMayTinhEntities.Users.Add(_user);
					objWebMayTinhEntities.SaveChanges();
					return RedirectToAction("Index");
				}
				else
				{
					ViewBag.error = "Email already exists";
					return View();
				}


			}
			return View();


		}

		[HttpGet]
		//Login
		public ActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(string email, string password)
		{
			if (ModelState.IsValid)
			{


				var f_password = GetMD5(password);
				var data = objWebMayTinhEntities.Users.Where(s => s.email.Equals(email) && s.password.Equals(f_password)).ToList();
				if (data.Count() > 0)
				{
					//add session
					Session["FullName"] = data.FirstOrDefault().name;
					Session["Email"] = data.FirstOrDefault().email;
					Session["idUser"] = data.FirstOrDefault().id;
					return RedirectToAction("Index","Home");
				}
				else
				{
					ViewBag.error = "Login failed";
					return RedirectToAction("Login");
				}
			}
			return View();
		}
		//Logout
		public ActionResult Logout()
		{
			Session.Clear();//remove session
			return RedirectToAction("Login");
		}

		//create a string MD5
		public static string GetMD5(string str)
		{
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] fromData = Encoding.UTF8.GetBytes(str);
			byte[] targetData = md5.ComputeHash(fromData);
			string byte2String = null;

			for (int i = 0; i < targetData.Length; i++)
			{
				byte2String += targetData[i].ToString("x2");

			}
			return byte2String;
		}
	}

}