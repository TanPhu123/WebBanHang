using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanDT.Models;
using WebBanDT.Context;


namespace WebBanDT.Controllers
{
    public class PaymentController : Controller
    {
		WebMayTinhEntities objWebMayTinhEntities = new WebMayTinhEntities();

		// GET: Payment
		public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login","Home");
            }
            else
            {
                //lấy thông tin từ giỏ hàng từ biến session
                var lstCart = (List<CartModel>)Session["Cart"];
                //gán dữ liệu cho order
                Order objOrder = new Order();
				/*objOrder.User_id = int.Parse(Session["idUser"].ToString());*/
                objOrder.CreatedDate = DateTime.Now;
                objOrder.status = true;
                objWebMayTinhEntities.Orders.Add(objOrder);
                //lưu thông tin vào dữ liệu bảng order
                objWebMayTinhEntities.SaveChanges();

                //lây orderId vừa mới tạo để lưu vào orderDetail
                int intOrderId = objOrder.id;
                List<Order_Detail> listOrderDetail = new List<Order_Detail>();
                foreach (var item in lstCart)
                {
                    Order_Detail obj = new Order_Detail();
                    obj.Quantity = item.Quantity;
                    obj.Order_id = intOrderId;
                    obj.Product_id = item.Product.id;
					listOrderDetail.Add(obj);
                }
                objWebMayTinhEntities.Order_Detail.AddRange(listOrderDetail);
                objWebMayTinhEntities.SaveChanges();
            }
            return View();
        }
    }
}