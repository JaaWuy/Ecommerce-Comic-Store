using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Data.Entity;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers.Api
{
    public class ProductsController : ApiController
    {
        private DSTruyen db = new DSTruyen();

        // GET: api/Products
        [HttpGet]
        [Route("api/products")]
        public IHttpActionResult GetProducts(string category = null, string search = null, int page = 1, int pageSize = 10)
        {
            try
            {
                var query = db.Truyens.Include(t => t.TheLoai).AsQueryable();

                // Lọc theo thể loại
                if (!string.IsNullOrEmpty(category))
                {
                    query = query.Where(t => t.MaTL == category);
                }

                // Tìm kiếm
                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(t => t.TenTruyen.Contains(search));
                }

                // Tổng số sản phẩm
                var total = query.Count();

                // Phân trang
                var products = query
                    .OrderBy(t => t.TenTruyen)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(t => new
                    {
                        t.MaTruyen,
                        t.TenTruyen,
                        t.GiaBan,
                        t.SoLuongTon,
                        t.imgUrl,
                        TheLoai = new
                        {
                            t.TheLoai.MaTL,
                            t.TheLoai.TenTL
                        }
                    })
                    .ToList();

                return Ok(new
                {
                    success = true,
                    data = products,
                    pagination = new
                    {
                        page,
                        pageSize,
                        total,
                        totalPages = (int)Math.Ceiling((double)total / pageSize)
                    }
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        // GET: api/Products/TR001
        [HttpGet]
        [Route("api/products/{id}")]
        public IHttpActionResult GetProduct(string id)
        {
            try
            {
                var product = db.Truyens
                    .Include(t => t.TheLoai)
                    .Where(t => t.MaTruyen == id)
                    .Select(t => new
                    {
                        t.MaTruyen,
                        t.TenTruyen,
                        t.GiaBan,
                        t.SoLuongTon,
                        t.imgUrl,
                        TheLoai = new
                        {
                            t.TheLoai.MaTL,
                            t.TheLoai.TenTL,
                            t.TheLoai.MoTa
                        }
                    })
                    .FirstOrDefault();

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(new
                {
                    success = true,
                    data = product
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
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
