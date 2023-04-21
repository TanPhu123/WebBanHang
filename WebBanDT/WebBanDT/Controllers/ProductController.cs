using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDT.Context;

namespace WebBanDT.Controllers
{
    public class ProductController : Controller
    {
		WebMayTinhEntities objWebMayTinhEntities = new WebMayTinhEntities();
		// GET: Product
		public ActionResult Detail(int Id)
        {
            var objProduct = objWebMayTinhEntities.Products.Where(n=>n.id==Id).FirstOrDefault();
            return View(objProduct);
        }
    }
}