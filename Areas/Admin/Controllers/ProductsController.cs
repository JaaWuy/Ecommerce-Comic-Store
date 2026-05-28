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
    public class ProductsController : Controller
    {
        private DSTruyen db = new DSTruyen();

        // GET: Admin/Products
        public ActionResult Index(string search, string category, int? page)
        {
            var products = db.Truyens.Include(t => t.TheLoai).AsQueryable();

            // Tìm kiếm
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(t => t.TenTruyen.Contains(search));
                ViewBag.Search = search;
            }

            // Lọc theo thể loại
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(t => t.MaTL == category);
                ViewBag.Category = category;
            }

            // Phân trang
            int pageSize = 15;
            int pageNumber = (page ?? 1);

            ViewBag.TheLoai = db.TheLoais.ToList();

            return View(products.OrderBy(t => t.TenTruyen).ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.TheLoai = new SelectList(db.TheLoais, "MaTL", "TenTL");
            return View();
        }

        // POST: Admin/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Truyen truyen)
        {
            if (ModelState.IsValid)
            {
                // Tạo mã truyện tự động
                var lastProduct = db.Truyens.OrderByDescending(t => t.MaTruyen).FirstOrDefault();
                if (lastProduct != null)
                {
                    var lastNumber = int.Parse(lastProduct.MaTruyen.Substring(2));
                    truyen.MaTruyen = "TR" + (lastNumber + 1).ToString("D3");
                }
                else
                {
                    truyen.MaTruyen = "TR001";
                }

                db.Truyens.Add(truyen);
                db.SaveChanges();

                TempData["SuccessMessage"] = "Thêm sản phẩm thành công!";
                return RedirectToAction("Index");
            }

            ViewBag.TheLoai = new SelectList(db.TheLoais, "MaTL", "TenTL", truyen.MaTL);
            return View(truyen);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            var truyen = db.Truyens.Find(id);
            if (truyen == null)
            {
                return HttpNotFound();
            }

            ViewBag.TheLoai = new SelectList(db.TheLoais, "MaTL", "TenTL", truyen.MaTL);
            return View(truyen);
        }

        // POST: Admin/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Truyen truyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(truyen).State = EntityState.Modified;
                db.SaveChanges();

                TempData["SuccessMessage"] = "Cập nhật sản phẩm thành công!";
                return RedirectToAction("Index");
            }

            ViewBag.TheLoai = new SelectList(db.TheLoais, "MaTL", "TenTL", truyen.MaTL);
            return View(truyen);
        }

        // GET: Admin/Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            var truyen = db.Truyens.Include(t => t.TheLoai).FirstOrDefault(t => t.MaTruyen == id);
            if (truyen == null)
            {
                return HttpNotFound();
            }

            return View(truyen);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var truyen = db.Truyens.Find(id);
            if (truyen == null)
            {
                return HttpNotFound();
            }

            // Kiểm tra xem sản phẩm có trong đơn hàng nào không
            var hasOrders = db.OrderDetails.Any(od => od.MaTruyen == id);
            if (hasOrders)
            {
                TempData["ErrorMessage"] = "Không thể xóa sản phẩm này vì đã có trong đơn hàng!";
                return RedirectToAction("Index");
            }

            db.Truyens.Remove(truyen);
            db.SaveChanges();

            TempData["SuccessMessage"] = "Xóa sản phẩm thành công!";
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
