using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DoAnWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private DSTruyen db = new DSTruyen();
        private UserManager<ApplicationUser> userManager;

        public DashboardController()
        {
            userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
        }

        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            // Thống kê tổng quan
            ViewBag.TotalUsers = db.Users.Count();
            ViewBag.TotalProducts = db.Truyens.Count();
            ViewBag.TotalOrders = db.Orders.Count();
            ViewBag.TotalRevenue = db.Orders
                .Where(o => o.Status == "Delivered")
                .Sum(o => (decimal?)o.TotalAmount) ?? 0;

            // Đơn hàng gần đây
            ViewBag.RecentOrders = db.Orders
                .Include("User")
                .OrderByDescending(o => o.OrderDate)
                .Take(5)
                .ToList();

            // Sản phẩm sắp hết hàng
            ViewBag.LowStockProducts = db.Truyens
                .Where(t => t.SoLuongTon < 10)
                .OrderBy(t => t.SoLuongTon)
                .Take(5)
                .ToList();

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                userManager.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
