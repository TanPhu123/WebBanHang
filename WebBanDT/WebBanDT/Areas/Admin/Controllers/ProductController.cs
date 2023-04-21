using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebBanDT.Context;
using static WebBanDT.Common;

namespace WebBanDT.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
		WebMayTinhEntities objWebMayTinhEntities = new WebMayTinhEntities();

		// GET: Admin/Product
		public ActionResult Index(string SearchString)
        {
			var lstProduct = objWebMayTinhEntities.Products.Where(n => n.name.Contains(SearchString)).ToList();

            return View(lstProduct);
        }

		//Create
		[HttpGet]
		public ActionResult Create()
		{
			Common objCommon=new Common();
			//lấy dữ liệu danh mục dưới db
			var lstCat = objWebMayTinhEntities.Categories.ToList();
			//Convert sang select list dạng value, text
			//lấy dữ liệu thương hiệu dưới db
			ListtoDataTableConverter converter = new ListtoDataTableConverter();
			DataTable dtCategory = converter.ToDataTable(lstCat);
			ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "id", "name");


			//lấy dữ liệu thương hiệu dưới db
			/*var lstBrand = objWebMayTinhEntities.Categories.ToList();
			DataTable dtBrand = converter.ToDataTable(lstBrand);
			ViewBag.LstBrand = objCommon.ToSelectList(dtBrand, "id", "name");*/

			//Loại sp
			List<ProductType> lstProductType = new List<ProductType>();
			ProductType objProductType = new ProductType();
			objProductType.Id = 01;
			objProductType.Name = "Giảm giá sốc";
			lstProductType.Add(objProductType);

			objProductType = new ProductType();
			objProductType.Id = 02;
			objProductType.Name = "Đề xuất";
			lstProductType.Add(objProductType);
			DataTable dtProductType = converter.ToDataTable(lstProductType);
			ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "id", "name");

			return View();
		}

		[ValidateInput(false)]
		[HttpPost]
		public ActionResult Create(Product objProduct)
		{
			if (ModelState.IsValid)
			{
				try
				{
					if (objProduct.ImageUpload != null)
					{
						string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
						string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
						fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
						objProduct.img = fileName;
						objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName));

					}
					objWebMayTinhEntities.Products.Add(objProduct);
					objWebMayTinhEntities.SaveChanges();
					return RedirectToAction("Index");
				}
				catch (Exception)
				{
					return View();
				}
			}
			return View(objProduct);
		}


		//Detail
		[HttpGet]
		public ActionResult Details(int id) {
			var objProduct = objWebMayTinhEntities.Products.Where(n=>n.id == id).FirstOrDefault();
			return View(objProduct);
		}


		//Delete
		[HttpGet]
		public ActionResult Delete(int id) {
			var objProduct = objWebMayTinhEntities.Products.Where(n=>n.id==id).FirstOrDefault();
			return View(objProduct);
		}

		[HttpPost]
		public ActionResult Delete(Product objPro)
		{
			var objProduct = objWebMayTinhEntities.Products.Where(n => n.id == objPro.id).FirstOrDefault();
			objWebMayTinhEntities.Products.Remove(objProduct);
			objWebMayTinhEntities.SaveChanges();
			return RedirectToAction("Index");
		}


		//Edit
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var objProduct = objWebMayTinhEntities.Products.Where(n => n.id == id).FirstOrDefault();
			return View(objProduct);
		}

		[HttpPost]
		public ActionResult Edit(int id, Product objProduct)
		{
			if(objProduct.ImageUpload != null)
			{
				string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
				string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
				fileName = fileName + "_" + long.Parse(DateTime.Now.ToString("yyyyMMddhhmmss")) + extension;
				objProduct.img = fileName;
				objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/img/"), fileName));
			}
			objWebMayTinhEntities.Entry(objProduct).State=EntityState.Modified;
			objWebMayTinhEntities.SaveChanges();
			return RedirectToAction("Index");

		}

		public class ProductType
		{
			public int Id { get; set; }
			public string Name { get; set; }
		}
		
	}
}