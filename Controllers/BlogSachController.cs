using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class BlogSachController : Controller
    {
        // GET: BlogSach
        public ActionResult Index()
        {
            DSTruyen db = new DSTruyen();
            List<TheLoai> tl = db.TheLoais.ToList();
            return View(tl);
        }
    }
}