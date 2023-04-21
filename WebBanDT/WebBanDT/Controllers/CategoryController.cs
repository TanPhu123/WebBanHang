using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDT.Context;
using WebBanDT.Models;

namespace WebBanDT.Controllers
{
    public class CategoryController : Controller
    {

		WebMayTinhEntities objWebMayTinhEntities = new WebMayTinhEntities();
		// GET: Category
		public ActionResult Index()
        {
			HomeModel objHomeModel = new HomeModel();
			objHomeModel.ListCategory = objWebMayTinhEntities.Categories.ToList();
			objHomeModel.ListProduct = objWebMayTinhEntities.Products.ToList();

			return View(objHomeModel);
		}

		public ActionResult ProductCategory(int Id)
		{
			var listProduct = objWebMayTinhEntities.Products.Where(n=>n.categoryid == Id).ToList();
			return View(listProduct);
		}
    }
}