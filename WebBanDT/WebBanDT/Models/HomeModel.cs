using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanDT.Context;

namespace WebBanDT.Models
{
	public class HomeModel
	{
		public List<Product> ListProduct { get; set; }
		public List<Category> ListCategory { get; set; }
		public List<Menu> ListMenu { get; set; }

	}
}