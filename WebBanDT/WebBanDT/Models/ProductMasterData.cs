using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebBanDT.Context;

namespace WebBanDT.Models
{
	public partial class ProductMasterData
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public ProductMasterData()
		{
			this.Order_Detail = new HashSet<Order_Detail>();
			this.Reviews = new HashSet<Review>();
		}
		public int id { get; set; }
		public string name { get; set; }
		[Required(ErrorMessage ="Vui lòng nhập tên sản phẩm")]
		[Display(Name = "Tên sản phẩm")]
		public Nullable<double> price { get; set; }
		[Display(Name = "Giá bán")]
		public Nullable<double> newprice { get; set; }
		[Display(Name = "Giá mới")]
		public string img { get; set; }
		[Display(Name = "Hình ảnh sản phẩm")]
		public string description { get; set; }
		[Display(Name = "Mô tả sản phẩm")]
		public string meta { get; set; }
		[Display(Name = "Phiên bản")]
		public Nullable<bool> hide { get; set; }
		public Nullable<int> order { get; set; }
		public Nullable<System.DateTime> datebegin { get; set; }
		[Display(Name = "Ngày nhập sản phẩm")]
		public Nullable<int> categoryid { get; set; }
		[Display(Name = "Thương hiệu sản phẩm")]
		public Nullable<int> quantity { get; set; }
		[Display(Name = "Số lượng sản phẩm")]
		public Nullable<int> typeid { get; set; }
		[Display(Name = "Loại sản phẩm")]

		public virtual Category Category { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Order_Detail> Order_Detail { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Review> Reviews { get; set; }

		[NotMapped]
		public System.Web.HttpPostedFileBase ImageUpload { get; set; }
	}
}