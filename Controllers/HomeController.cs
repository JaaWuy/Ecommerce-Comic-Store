using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class HomeController : Controller
    {
        private DSTruyen db = new DSTruyen();

        // GET: Home
        public ActionResult Index()
        {
            // Lấy sản phẩm nổi bật (mới nhất hoặc bán chạy)
            ViewBag.FeaturedProducts = db.Truyens
                .Include(t => t.TheLoai)
                .OrderByDescending(t => t.MaTruyen)
                .Take(8)
                .ToList();

            // Lấy sản phẩm theo thể loại
            ViewBag.MangaProducts = db.Truyens
                .Include(t => t.TheLoai)
                .Where(t => t.MaTL == "TL01")
                .Take(4)
                .ToList();

            ViewBag.ManhwaProducts = db.Truyens
                .Include(t => t.TheLoai)
                .Where(t => t.MaTL == "TL02")
                .Take(4)
                .ToList();

            ViewBag.ComicsProducts = db.Truyens
                .Include(t => t.TheLoai)
                .Where(t => t.MaTL == "TL04")
                .Take(4)
                .ToList();

            // Lấy danh sách thể loại
            ViewBag.TheLoai = db.TheLoais.ToList();

            // Lấy 12 sản phẩm ngẫu nhiên cho Slider (mỗi lần refresh sẽ khác)
            ViewBag.SliderProducts = db.Truyens
                .Include(t => t.TheLoai)
                .OrderBy(t => Guid.NewGuid())
                .Take(12)
                .ToList();

            return View();
        }

        // GET: Home/ProductsApi
        public ActionResult ProductsApi()
        {
            return View();
        }

        // GET: Home/TeamMembers
        [Authorize(Roles = "Admin")]
        public ActionResult TeamMembers()
        {
            return View();
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