using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using DoAnWeb.Models;
using PagedList;

namespace DoAnWeb.Controllers
{
    public class TruyenController : Controller
    {
        private DSTruyen db = new DSTruyen();

        // GET: Truyen
        public ActionResult Index(string search, string category, string sortOrder, int? page)
        {
            // Lấy danh sách truyện
            var truyens = db.Truyens.Include(t => t.TheLoai).AsQueryable();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                truyens = truyens.Where(t => t.TenTruyen.Contains(search));
                ViewBag.Search = search;
            }

            // Lọc theo thể loại
            if (!string.IsNullOrEmpty(category))
            {
                truyens = truyens.Where(t => t.MaTL == category);
                ViewBag.Category = category;
            }

            // Sắp xếp
            ViewBag.NameSortParam = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParam = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.CurrentSort = sortOrder;

            switch (sortOrder)
            {
                case "name_desc":
                    truyens = truyens.OrderByDescending(t => t.TenTruyen);
                    break;
                case "price":
                    truyens = truyens.OrderBy(t => t.GiaBan);
                    break;
                case "price_desc":
                    truyens = truyens.OrderByDescending(t => t.GiaBan);
                    break;
                default:
                    truyens = truyens.OrderBy(t => t.TenTruyen);
                    break;
            }

            // Phân trang
            int pageSize = 12;
            int pageNumber = (page ?? 1);

            // Lấy danh sách thể loại
            ViewBag.TheLoai = db.TheLoais.ToList();

            return View(truyens.ToPagedList(pageNumber, pageSize));
        }

        // GET: Truyen/Detail/5
        public ActionResult Detail(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            var truyen = db.Truyens
                .Include(t => t.TheLoai)
                .FirstOrDefault(t => t.MaTruyen == id);

            if (truyen == null)
            {
                return HttpNotFound();
            }

            // Lấy sản phẩm cùng thể loại
            ViewBag.RelatedProducts = db.Truyens
                .Where(t => t.MaTL == truyen.MaTL && t.MaTruyen != id)
                .Take(4)
                .ToList();

            return View(truyen);
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