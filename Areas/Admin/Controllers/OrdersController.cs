using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DoAnWeb.Models;
using PagedList;

namespace DoAnWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private DSTruyen db = new DSTruyen();

        // GET: Admin/Orders
        public ActionResult Index(string status, DateTime? fromDate, DateTime? toDate, int? page)
        {
            var orders = db.Orders.Include(o => o.User).AsQueryable();

            // Lọc theo trạng thái
            if (!string.IsNullOrEmpty(status))
            {
                orders = orders.Where(o => o.Status == status);
                ViewBag.Status = status;
            }

            // Lọc theo ngày
            if (fromDate.HasValue)
            {
                orders = orders.Where(o => o.OrderDate >= fromDate.Value);
                ViewBag.FromDate = fromDate.Value.ToString("yyyy-MM-dd");
            }

            if (toDate.HasValue)
            {
                var endDate = toDate.Value.AddDays(1);
                orders = orders.Where(o => o.OrderDate < endDate);
                ViewBag.ToDate = toDate.Value.ToString("yyyy-MM-dd");
            }

            // Phân trang
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            return View(orders.OrderByDescending(o => o.OrderDate).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var order = db.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails.Select(od => od.Truyen))
                .FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                return HttpNotFound();
            }

            return View(order);
        }

        // POST: Admin/Orders/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateStatus(int orderId, string status)
        {
            var order = db.Orders.Find(orderId);
            if (order == null)
            {
                return HttpNotFound();
            }

            order.Status = status;
            db.SaveChanges();

            TempData["SuccessMessage"] = "Cập nhật trạng thái đơn hàng thành công!";
            return RedirectToAction("Details", new { id = orderId });
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
