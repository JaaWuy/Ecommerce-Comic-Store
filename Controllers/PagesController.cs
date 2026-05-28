using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace DoAnWeb.Controllers
{
    public class PagesController : Controller
    {
        // Email nhận thông tin liên hệ
        private const string RECEIVER_EMAIL = "sachwuy@gmail.com";
        
        // GET: Pages/About
        public ActionResult About()
        {
            ViewBag.Title = "Giới Thiệu";
            ViewBag.PageHeading = "Giới Thiệu Về Chúng Tôi";
            ViewBag.PageSubheading = "Nhà sách uy tín - Sách chính hãng - Giao hàng nhanh chóng";
            ViewBag.CompanyName = "Nhà Sách Của Huy - SachWuy.vn";
            ViewBag.CompanyDescription = "SachWuy.vn là một trong những cửa hàng sách trực tuyến hàng đầu tại Việt Nam, chuyên cung cấp các tác phẩm Manga, Manhwa, Manhua, Comics và Webtoon chất lượng cao.";
            ViewBag.MissionTitle = "Sứ Mệnh Của Chúng Tôi";
            ViewBag.MissionDescription = "Mục tiêu của chúng tôi là mang những câu chuyện tuyệt vời từ khắp nơi trên thế giới đến tay các độc giả yêu thích truyện tranh tại Việt Nam. Chúng tôi cam kết cung cấp những sản phẩm chính hãng, giá cả hợp lý và dịch vụ khách hàng tuyệt vời.";
            ViewBag.WhyTitle = "Tại Sao Chọn Chúng Tôi?";
            ViewBag.Benefit1 = "Sản phẩm chính hãng 100% - Tất cả các sản phẩm đều được nhập khẩu từ nhà xuất bản chính thức";
            ViewBag.Benefit2 = "Giá cả cạnh tranh - Chúng tôi luôn cố gắng cung cấp giá tốt nhất trên thị trường";
            ViewBag.Benefit3 = "Giao hàng nhanh - Giao hàng tận nơi trong 2-3 ngày làm việc";
            ViewBag.Benefit4 = "Hỗ trợ khách hàng tuyệt vời - Đội ngũ nhân viên nhiệt tình, sẵn sàng giúp đỡ";
            ViewBag.Benefit5 = "Thu mua sách cũ - Chúng tôi thu mua sách cũ với giá hợp lý";
            ViewBag.CategoriesTitle = "Các Thể Loại Chúng Tôi Cung Cấp";
            ViewBag.ContactButtonText = "Gửi Thông Điệp";

            return View();
        }

        // GET: Pages/Contact
        public ActionResult Contact()
        {
            ViewBag.Title = "Liên Hệ";
            ViewBag.PageHeading = "Liên Hệ Với Chúng Tôi";
            ViewBag.PageSubheading = "Chúng tôi luôn sẵn sàng lắng nghe ý kiến của bạn";
            ViewBag.FormTitle = "Gửi Thông Điệp Cho Chúng Tôi";
            ViewBag.NameLabel = "Họ và Tên";
            ViewBag.NamePlaceholder = "Nhập họ và tên của bạn";
            ViewBag.EmailLabel = "Email";
            ViewBag.EmailPlaceholder = "Nhập địa chỉ email của bạn";
            ViewBag.PhoneLabel = "Số Điện Thoại";
            ViewBag.PhonePlaceholder = "Nhập số điện thoại (không bắt buộc)";
            ViewBag.MessageLabel = "Nội Dung Tin Nhắn";
            ViewBag.MessagePlaceholder = "Nhập nội dung tin nhắn của bạn...";
            ViewBag.SubmitButton = "Gửi Tin Nhắn";
            ViewBag.InfoTitle = "Thông Tin Liên Hệ";
            ViewBag.PhoneTitle = "Điện Thoại";
            ViewBag.PhoneNumber = "0974 869 355";
            ViewBag.PhoneSupport = "Hỗ trợ từ 8:00 - 21:00 hàng ngày";
            ViewBag.EmailTitle = "Email";
            ViewBag.EmailAddress = "info@sachwuy.vn";
            ViewBag.EmailResponse = "Phản hồi trong 24 giờ";
            ViewBag.AddressTitle = "Địa Chỉ";
            ViewBag.CompanyAddress = "Nhà Sách Của Huy";
            ViewBag.StreetAddress = "123 Đường Trần Hưng Đạo";
            ViewBag.CityAddress = "Quận 1, TP. Hồ Chí Minh";
            ViewBag.HoursTitle = "Giờ Làm Việc";
            ViewBag.MonToSat = "Thứ 2 - Thứ 7: 8:00 - 21:00";
            ViewBag.Sunday = "Chủ Nhật: 9:00 - 20:00";
            ViewBag.Holiday = "Ngày Lễ: 10:00 - 18:00";
            ViewBag.FollowTitle = "Theo Dõi Chúng Tôi";
            ViewBag.FAQTitle = "Các Câu Hỏi Thường Gặp (FAQ)";
            ViewBag.FAQ1Q = "Thời gian giao hàng mất bao lâu?";
            ViewBag.FAQ1A = "Chúng tôi giao hàng tận nơi trong 2-3 ngày làm việc. Đối với các đơn hàng đặc biệt, thời gian giao hàng có thể kéo dài hơn.";
            ViewBag.FAQ2Q = "Bạn có chấp nhận hoàn trả sản phẩm không?";
            ViewBag.FAQ2A = "Có, chúng tôi chấp nhận hoàn trả sản phẩm trong vòng 7 ngày nếu sản phẩm bị lỗi hoặc không đúng với mô tả.";
            ViewBag.FAQ3Q = "Phương thức thanh toán nào được chấp nhận?";
            ViewBag.FAQ3A = "Chúng tôi chấp nhận thanh toán qua: Chuyển khoản ngân hàng, Ví điện tử (Momo, ZaloPay), và Thanh toán khi nhận hàng (COD).";
            ViewBag.FAQ4Q = "Bạn có thu mua sách cũ không?";
            ViewBag.FAQ4A = "Có, chúng tôi thu mua sách cũ với giá hợp lý. Hãy liên hệ 0974 869 355 để biết thêm chi tiết.";

            return View();
        }

        // POST: Pages/Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(string name, string email, string phone, string message)
        {
            try
            {
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(message))
                {
                    // Gửi email đến chủ shop
                    SendContactEmail(name, email, phone, message);
                    ViewBag.SuccessMessage = "Cảm ơn bạn đã liên hệ! Tin nhắn của bạn đã được gửi thành công. Chúng tôi sẽ phản hồi trong thời gian sớm nhất.";
                    ModelState.Clear();
                }
                else
                {
                    ViewBag.ErrorMessage = "Vui lòng điền đầy đủ thông tin bắt buộc (Họ tên, Email, Nội dung tin nhắn).";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Có lỗi xảy ra khi gửi tin nhắn. Vui lòng thử lại sau hoặc liên hệ qua số điện thoại 0974 869 355.";
                System.Diagnostics.Debug.WriteLine("Email Error: " + ex.Message);
            }

            // Thiết lập lại các ViewBag cho View
            ViewBag.Title = "Liên Hệ";
            ViewBag.PageHeading = "Liên Hệ Với Chúng Tôi";
            ViewBag.PageSubheading = "Chúng tôi luôn sẵn sàng lắng nghe ý kiến của bạn";
            ViewBag.FormTitle = "Gửi Thông Điệp Cho Chúng Tôi";
            ViewBag.NameLabel = "Họ và Tên";
            ViewBag.NamePlaceholder = "Nhập họ và tên của bạn";
            ViewBag.EmailLabel = "Email";
            ViewBag.EmailPlaceholder = "Nhập địa chỉ email của bạn";
            ViewBag.PhoneLabel = "Số Điện Thoại";
            ViewBag.PhonePlaceholder = "Nhập số điện thoại (không bắt buộc)";
            ViewBag.MessageLabel = "Nội Dung Tin Nhắn";
            ViewBag.MessagePlaceholder = "Nhập nội dung tin nhắn của bạn...";
            ViewBag.SubmitButton = "Gửi Tin Nhắn";
            ViewBag.InfoTitle = "Thông Tin Liên Hệ";
            ViewBag.PhoneTitle = "Điện Thoại";
            ViewBag.PhoneNumber = "0974 869 355";
            ViewBag.PhoneSupport = "Hỗ trợ từ 8:00 - 21:00 hàng ngày";
            ViewBag.EmailTitle = "Email";
            ViewBag.EmailAddress = "info@sachwuy.vn";
            ViewBag.EmailResponse = "Phản hồi trong 24 giờ";
            ViewBag.AddressTitle = "Địa Chỉ";
            ViewBag.CompanyAddress = "Nhà Sách Của Huy";
            ViewBag.StreetAddress = "123 Đường Trần Hưng Đạo";
            ViewBag.CityAddress = "Quận 1, TP. Hồ Chí Minh";
            ViewBag.HoursTitle = "Giờ Làm Việc";
            ViewBag.MonToSat = "Thứ 2 - Thứ 7: 8:00 - 21:00";
            ViewBag.Sunday = "Chủ Nhật: 9:00 - 20:00";
            ViewBag.Holiday = "Ngày Lễ: 10:00 - 18:00";
            ViewBag.FollowTitle = "Theo Dõi Chúng Tôi";
            ViewBag.FAQTitle = "Các Câu Hỏi Thường Gặp (FAQ)";
            ViewBag.FAQ1Q = "Thời gian giao hàng mất bao lâu?";
            ViewBag.FAQ1A = "Chúng tôi giao hàng tận nơi trong 2-3 ngày làm việc. Đối với các đơn hàng đặc biệt, thời gian giao hàng có thể kéo dài hơn.";
            ViewBag.FAQ2Q = "Bạn có chấp nhận hoàn trả sản phẩm không?";
            ViewBag.FAQ2A = "Có, chúng tôi chấp nhận hoàn trả sản phẩm trong vòng 7 ngày nếu sản phẩm bị lỗi hoặc không đúng với mô tả.";
            ViewBag.FAQ3Q = "Phương thức thanh toán nào được chấp nhận?";
            ViewBag.FAQ3A = "Chúng tôi chấp nhận thanh toán qua: Chuyển khoản ngân hàng, Ví điện tử (Momo, ZaloPay), và Thanh toán khi nhận hàng (COD).";
            ViewBag.FAQ4Q = "Bạn có thu mua sách cũ không?";
            ViewBag.FAQ4A = "Có, chúng tôi thu mua sách cũ với giá hợp lý. Hãy liên hệ 0974 869 355 để biết thêm chi tiết.";

            return View();
        }

        /// <summary>
        /// Gửi email liên hệ đến chủ shop
        /// </summary>
        private void SendContactEmail(string name, string email, string phone, string message)
        {
            // Cấu hình SMTP - Sử dụng Gmail SMTP
            // LƯU Ý: Để gửi email qua Gmail, bạn cần:
            // 1. Bật "Cho phép ứng dụng kém an toàn" hoặc
            // 2. Tạo App Password nếu bật 2FA: https://myaccount.google.com/apppasswords
            
            string smtpHost = System.Configuration.ConfigurationManager.AppSettings["SmtpHost"] ?? "smtp.gmail.com";
            int smtpPort = int.Parse(System.Configuration.ConfigurationManager.AppSettings["SmtpPort"] ?? "587");
            string smtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"] ?? "your-email@gmail.com";
            string smtpPass = System.Configuration.ConfigurationManager.AppSettings["SmtpPass"] ?? "your-app-password";
            bool enableSsl = bool.Parse(System.Configuration.ConfigurationManager.AppSettings["SmtpEnableSsl"] ?? "true");

            using (var smtpClient = new SmtpClient(smtpHost, smtpPort))
            {
                smtpClient.EnableSsl = enableSsl;
                smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPass);

                // Tạo nội dung email
                string subject = $"[SachWuy.vn] Tin nhắn mới từ {name}";
                string body = $@"
<html>
<body style='font-family: Arial, sans-serif;'>
    <h2 style='color: #8B5CF6;'>📬 Tin nhắn mới từ trang Liên Hệ</h2>
    <hr style='border: 1px solid #E5E7EB;'/>
    
    <table style='width: 100%; border-collapse: collapse;'>
        <tr>
            <td style='padding: 10px; background: #F3F4F6; font-weight: bold; width: 150px;'>👤 Họ và Tên:</td>
            <td style='padding: 10px;'>{name}</td>
        </tr>
        <tr>
            <td style='padding: 10px; background: #F3F4F6; font-weight: bold;'>📧 Email:</td>
            <td style='padding: 10px;'><a href='mailto:{email}'>{email}</a></td>
        </tr>
        <tr>
            <td style='padding: 10px; background: #F3F4F6; font-weight: bold;'>📞 Số điện thoại:</td>
            <td style='padding: 10px;'>{(string.IsNullOrEmpty(phone) ? "Không cung cấp" : phone)}</td>
        </tr>
        <tr>
            <td style='padding: 10px; background: #F3F4F6; font-weight: bold;'>📝 Nội dung:</td>
            <td style='padding: 10px;'>{message.Replace("\n", "<br/>")}</td>
        </tr>
        <tr>
            <td style='padding: 10px; background: #F3F4F6; font-weight: bold;'>🕐 Thời gian:</td>
            <td style='padding: 10px;'>{DateTime.Now:dd/MM/yyyy HH:mm:ss}</td>
        </tr>
    </table>
    
    <hr style='border: 1px solid #E5E7EB; margin-top: 20px;'/>
    <p style='color: #6B7280; font-size: 12px;'>Email này được gửi tự động từ hệ thống SachWuy.vn</p>
</body>
</html>";

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUser, "SachWuy.vn - Liên Hệ"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(RECEIVER_EMAIL);
                
                // Thêm Reply-To để có thể trả lời trực tiếp cho khách hàng
                mailMessage.ReplyToList.Add(new MailAddress(email, name));

                smtpClient.Send(mailMessage);
            }
        }
    }
}
