using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class TheLoaiController : Controller
    {
        // GET: TheLoai
        public ActionResult Index()
        {
            DSTruyen db = new DSTruyen();
            List<TheLoai> theloai = db.TheLoais.ToList();
            return View(theloai);
        }
    }
}