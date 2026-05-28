using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    [Authorize] // Yêu cầu đăng nhập cho tất cả actions
    public class CartController : Controller
    {
        private DSTruyen db = new DSTruyen();

        // GET: Cart
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var cartItems = db.CartItems
                .Include(c => c.Truyen)
                .Include(c => c.Truyen.TheLoai)
                .Where(c => c.UserId == userId)
                .ToList();

            ViewBag.Total = cartItems.Sum(c => c.Quantity * c.Truyen.GiaBan);
            return View(cartItems);
        }

        // POST: Cart/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddToCart(string maTruyen, int quantity = 1)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var truyen = db.Truyens.Find(maTruyen);

                if (truyen == null)
                {
                    return Json(new { success = false, message = "Sản phẩm không tồn tại!" });
                }

                if (truyen.SoLuongTon < quantity)
                {
                    return Json(new { success = false, message = "Số lượng trong kho không đủ!" });
                }

                // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
                var existingItem = db.CartItems
                    .FirstOrDefault(c => c.UserId == userId && c.MaTruyen == maTruyen);

                if (existingItem != null)
                {
                    // Cập nhật số lượng
                    existingItem.Quantity += quantity;
                }
                else
                {
                    // Thêm mới
                    var cartItem = new CartItem
                    {
                        UserId = userId,
                        MaTruyen = maTruyen,
                        Quantity = quantity,
                        DateAdded = DateTime.Now
                    };
                    db.CartItems.Add(cartItem);
                }

                db.SaveChanges();
                return Json(new { success = true, message = "Đã thêm vào giỏ hàng!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // POST: Cart/UpdateQuantity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateQuantity(int cartItemId, int quantity)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var cartItem = db.CartItems
                    .Include(c => c.Truyen)
                    .FirstOrDefault(c => c.CartItemId == cartItemId && c.UserId == userId);

                if (cartItem == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm trong giỏ hàng!" });
                }

                if (quantity <= 0)
                {
                    return Json(new { success = false, message = "Số lượng phải lớn hơn 0!" });
                }

                if (cartItem.Truyen.SoLuongTon < quantity)
                {
                    return Json(new { success = false, message = "Số lượng trong kho không đủ!" });
                }

                cartItem.Quantity = quantity;
                db.SaveChanges();

                var subtotal = cartItem.Quantity * cartItem.Truyen.GiaBan;
                var total = db.CartItems
                    .Include(c => c.Truyen)
                    .Where(c => c.UserId == userId)
                    .Sum(c => (decimal?)(c.Quantity * c.Truyen.GiaBan)) ?? 0;

                return Json(new { success = true, subtotal = subtotal, total = total });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // POST: Cart/RemoveItem
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RemoveItem(int cartItemId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var cartItem = db.CartItems
                    .FirstOrDefault(c => c.CartItemId == cartItemId && c.UserId == userId);

                if (cartItem == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy sản phẩm trong giỏ hàng!" });
                }

                db.CartItems.Remove(cartItem);
                db.SaveChanges();

                var total = db.CartItems
                    .Include(c => c.Truyen)
                    .Where(c => c.UserId == userId)
                    .Sum(c => (decimal?)(c.Quantity * c.Truyen.GiaBan)) ?? 0;

                return Json(new { success = true, total = total });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra: " + ex.Message });
            }
        }

        // GET: Cart/GetCartCount
        public JsonResult GetCartCount()
        {
            var userId = User.Identity.GetUserId();
            var count = db.CartItems.Where(c => c.UserId == userId).Sum(c => (int?)c.Quantity) ?? 0;
            return Json(count, JsonRequestBehavior.AllowGet);
        }

        // GET: Cart/Checkout
        public ActionResult Checkout()
        {
            var userId = User.Identity.GetUserId();
            var cartItems = db.CartItems
                .Include(c => c.Truyen)
                .Where(c => c.UserId == userId)
                .ToList();

            if (!cartItems.Any())
            {
                TempData["Message"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index");
            }

            ViewBag.Total = cartItems.Sum(c => c.Quantity * c.Truyen.GiaBan);
            return View(cartItems);
        }

        // POST: Cart/Checkout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(string receiverName, string receiverPhone, string shippingAddress, string note, string paymentMethod)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                var cartItems = db.CartItems
                    .Include(c => c.Truyen)
                    .Where(c => c.UserId == userId)
                    .ToList();

                if (!cartItems.Any())
                {
                    TempData["Error"] = "Giỏ hàng của bạn đang trống!";
                    return RedirectToAction("Index");
                }

                // Kiểm tra số lượng tồn kho
                foreach (var item in cartItems)
                {
                    if (item.Truyen.SoLuongTon < item.Quantity)
                    {
                        TempData["Error"] = $"Sản phẩm '{item.Truyen.TenTruyen}' không đủ số lượng trong kho!";
                        return RedirectToAction("Checkout");
                    }
                }

                // Tạo đơn hàng
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    TotalAmount = cartItems.Sum(c => c.Quantity * c.Truyen.GiaBan),
                    Status = "Pending",
                    ReceiverName = receiverName,
                    ReceiverPhone = receiverPhone,
                    ShippingAddress = shippingAddress,
                    Note = note,
                    PaymentMethod = paymentMethod
                };

                db.Orders.Add(order);
                db.SaveChanges();

                // Tạo chi tiết đơn hàng và cập nhật tồn kho
                foreach (var item in cartItems)
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.OrderId,
                        MaTruyen = item.MaTruyen,
                        Quantity = item.Quantity,
                        Price = item.Truyen.GiaBan
                    };
                    db.OrderDetails.Add(orderDetail);

                    // Giảm số lượng tồn kho
                    item.Truyen.SoLuongTon -= item.Quantity;
                }

                // Xóa giỏ hàng
                db.CartItems.RemoveRange(cartItems);
                db.SaveChanges();

                TempData["OrderId"] = order.OrderId;
                return RedirectToAction("OrderConfirmation");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra khi đặt hàng: " + ex.Message;
                return RedirectToAction("Checkout");
            }
        }

        // GET: Cart/OrderConfirmation
        public ActionResult OrderConfirmation()
        {
            if (TempData["OrderId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var orderId = (int)TempData["OrderId"];
            var order = db.Orders
                .Include(o => o.OrderDetails.Select(od => od.Truyen))
                .FirstOrDefault(o => o.OrderId == orderId);

            return View(order);
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
