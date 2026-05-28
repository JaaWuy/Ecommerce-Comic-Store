using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private DSTruyen db = new DSTruyen();

        // GET: Orders
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var orders = db.Orders.Where(o => o.UserId == userId).OrderByDescending(o => o.OrderDate).ToList();
            return View(orders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var userId = User.Identity.GetUserId();
            var order = db.Orders.Include(o => o.OrderDetails.Select(od => od.Truyen))
                                 .FirstOrDefault(o => o.OrderId == id && o.UserId == userId);
                                 
            if (order == null)
            {
                return HttpNotFound();
            }
            
            return View(order);
        }

        // POST: Orders/Cancel/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var order = db.Orders.FirstOrDefault(o => o.OrderId == id && o.UserId == userId);

            if (order == null)
            {
                return HttpNotFound();
            }

            // Chỉ cho phép hủy khi đơn hàng đang ở trạng thái "Chờ xử lý"
            if (order.Status != "Chờ xử lý" && order.Status != "Pending")
            {
                TempData["Error"] = "Không thể hủy đơn hàng này. Chỉ có thể hủy đơn hàng đang ở trạng thái 'Chờ xử lý'.";
                return RedirectToAction("Details", new { id = id });
            }

            order.Status = "Đã hủy";
            db.SaveChanges();

            TempData["Success"] = "Đơn hàng #" + id + " đã được hủy thành công.";
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
