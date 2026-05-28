namespace DoAnWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using DoAnWeb.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<DoAnWeb.Models.DSTruyen>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DoAnWeb.Models.DSTruyen context)
        {
            // Tạo Roles
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            // Tạo Admin User
            if (!context.Users.Any(u => u.Email == "admin@sachwuy.vn"))
            {
                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var admin = new ApplicationUser
                {
                    UserName = "admin@sachwuy.vn",
                    Email = "admin@sachwuy.vn",
                    FullName = "Administrator",
                    PhoneNumber = "0974869355",
                    Address = "Hà Nội",
                    RegisterDate = DateTime.Now
                };

                userManager.Create(admin, "Admin@123");
                userManager.AddToRole(admin.Id, "Admin");
            }

            // Thêm dữ liệu thể loại (nếu chưa có)
            if (!context.TheLoais.Any())
            {
                context.TheLoais.AddRange(new List<TheLoai>
                {
                    new TheLoai { MaTL = "TL01", TenTL = "Manga", MoTa = "Truyện tranh Nhật Bản" },
                    new TheLoai { MaTL = "TL02", TenTL = "Manhwa", MoTa = "Truyện tranh Hàn Quốc" },
                    new TheLoai { MaTL = "TL03", TenTL = "Manhua", MoTa = "Truyện tranh Trung Quốc" },
                    new TheLoai { MaTL = "TL04", TenTL = "Comics", MoTa = "Truyện tranh phương Tây" },
                    new TheLoai { MaTL = "TL05", TenTL = "Webtoon", MoTa = "Truyện tranh phát hành trực tuyến" }
                });
            }

            // Thêm dữ liệu truyện từ file SQL - Thêm mới nếu chưa đủ 54 truyện
            if (context.Truyens.Count() < 54)
            {
                // Xóa dữ liệu cũ (xóa các bảng phụ thuộc trước)
                context.Database.ExecuteSqlCommand("DELETE FROM CartItems");
                context.Database.ExecuteSqlCommand("DELETE FROM OrderDetails");
                context.Database.ExecuteSqlCommand("DELETE FROM Truyens");
                context.Truyens.AddRange(new List<Truyen>
                {
                    // MANGA
                    new Truyen { MaTruyen = "TR001", TenTruyen = "One Piece - Tập 1", MaTL = "TL01", GiaBan = 25000, SoLuongTon = 100, imgUrl = "https://cdn1.fahasa.com/media/catalog/product/8/9/8935244865097.jpg" },
                    new Truyen { MaTruyen = "TR002", TenTruyen = "One Piece - Tập 2", MaTL = "TL01", GiaBan = 25000, SoLuongTon = 90, imgUrl = "https://bizweb.dktcdn.net/100/567/082/products/2-7c49f80cdc1c48b9a483d9d66fd684c4-master.jpg?v=1747326559600" },
                    new Truyen { MaTruyen = "TR003", TenTruyen = "One Piece - Tập 3", MaTL = "TL01", GiaBan = 25000, SoLuongTon = 80, imgUrl = "https://cdn1.fahasa.com/media/catalog/product/8/9/8935244865110.jpg" },
                    new Truyen { MaTruyen = "TR004", TenTruyen = "Naruto - Tập 1", MaTL = "TL01", GiaBan = 28000, SoLuongTon = 95, imgUrl = "https://cdn1.fahasa.com/media/catalog/product/n/a/naruto---tap-1---tb-2022_1.jpg" },
                    new Truyen { MaTruyen = "TR005", TenTruyen = "Naruto - Tập 2", MaTL = "TL01", GiaBan = 28000, SoLuongTon = 90, imgUrl = "https://cdn1.fahasa.com/media/catalog/product/n/a/naruto---tap-2_1.jpg" },
                    new Truyen { MaTruyen = "TR006", TenTruyen = "Dragon Ball - Tập 1", MaTL = "TL01", GiaBan = 30000, SoLuongTon = 120, imgUrl = "https://cdn1.fahasa.com/media/catalog/product/d/r/dragon_ball_7_vien_ngoc_rong_bia_tap_1_tb_2025.jpg" },
                    new Truyen { MaTruyen = "TR007", TenTruyen = "Dragon Ball - Tập 2", MaTL = "TL01", GiaBan = 30000, SoLuongTon = 115, imgUrl = "https://cdn1.fahasa.com/media/catalog/product/d/r/dragon-ball-full-color-tap-2.jpg" },
                    new Truyen { MaTruyen = "TR008", TenTruyen = "Dragon Ball - Tập 3", MaTL = "TL01", GiaBan = 30000, SoLuongTon = 110, imgUrl = "https://product.hstatic.net/1000376556/product/dragon_ball_full_color_tap_3_c51433d7bc764bbaac0d31b0955fb649_1024x1024.jpg" },
                    new Truyen { MaTruyen = "TR009", TenTruyen = "Demon Slayer - Tập 1", MaTL = "TL01", GiaBan = 35000, SoLuongTon = 100, imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFNpkPPwTM36Q24J--eTlUmYoRwIalZkJ3-Q&s" },
                    new Truyen { MaTruyen = "TR010", TenTruyen = "Demon Slayer - Tập 2", MaTL = "TL01", GiaBan = 35000, SoLuongTon = 95, imgUrl = "https://product.hstatic.net/1000376556/product/thanh_guom_diet_quy_2_7c95e7c4ac9c49e1908365fbc641127c_1024x1024.jpg" },
                    new Truyen { MaTruyen = "TR011", TenTruyen = "Bleach - Tập 1", MaTL = "TL01", GiaBan = 30000, SoLuongTon = 75, imgUrl = "https://bookbuy.vn/Res/Images/Product/su-mang-than-chet-tap-1-(tai-ban-2014)_35360_1.jpg" },
                    new Truyen { MaTruyen = "TR012", TenTruyen = "Tokyo Ghoul - Tập 1", MaTL = "TL01", GiaBan = 32000, SoLuongTon = 65, imgUrl = "https://cdn1.fahasa.com/media/flashmagazine/images/page_images/_1___tokyo_ghoul_1/2021_08_03_14_20_59_1-390x510.jpg" },
                    new Truyen { MaTruyen = "TR013", TenTruyen = "Tokyo Ghoul - Tập 2", MaTL = "TL01", GiaBan = 32000, SoLuongTon = 60, imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTkXBfoaf-jKQQzRnTMotO-eHeN-lElEGztdg&s" },
                    new Truyen { MaTruyen = "TR014", TenTruyen = "My Hero Academia - Tập 1", MaTL = "TL01", GiaBan = 32000, SoLuongTon = 80, imgUrl = "https://sachtiengviet.com/cdn/shop/products/87ed83fbce339ea5b05eb11f1b3e65de.jpg?v=1702245473" },
                    new Truyen { MaTruyen = "TR015", TenTruyen = "My Hero Academia - Tập 2", MaTL = "TL01", GiaBan = 32000, SoLuongTon = 75, imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTJ8ApSbHl6aRu7e3k7SMXdSOatt2VUFNpWlg&s" },

                    // MANHWA
                    new Truyen { MaTruyen = "TR016", TenTruyen = "Solo Leveling - Tập 1", MaTL = "TL02", GiaBan = 35000, SoLuongTon = 85, imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQy1TfB_xd1YrXzPXTWiUpA1fEwEo-5BGBjtQ&s" },
                    new Truyen { MaTruyen = "TR017", TenTruyen = "Solo Leveling - Tập 2", MaTL = "TL02", GiaBan = 35000, SoLuongTon = 80, imgUrl = "https://salt.tikicdn.com/ts/product/1a/4b/49/64d424642a5b222ee7d6bda7a3152255.jpg" },
                    new Truyen { MaTruyen = "TR018", TenTruyen = "The Beginning After The End - Tập 1", MaTL = "TL02", GiaBan = 38000, SoLuongTon = 70, imgUrl = "https://m.media-amazon.com/images/I/81eqlOf4owL._AC_UF1000,1000_QL80_.jpg" },
                    new Truyen { MaTruyen = "TR019", TenTruyen = "Omniscient Reader’s Viewpoint - Tập 1", MaTL = "TL02", GiaBan = 36000, SoLuongTon = 90, imgUrl = "https://m.media-amazon.com/images/I/81ZuZO5vr2L._SL1500_.jpg" },
                    new Truyen { MaTruyen = "TR020", TenTruyen = "Tower of God - Tập 1", MaTL = "TL02", GiaBan = 34000, SoLuongTon = 100, imgUrl = "https://upload.wikimedia.org/wikipedia/vi/7/77/Tower_of_God_Vol_1_Cover.jpg" },
                    new Truyen { MaTruyen = "TR021", TenTruyen = "Tower of God - Tập 2", MaTL = "TL02", GiaBan = 34000, SoLuongTon = 95, imgUrl = "https://upload.wikimedia.org/wikipedia/vi/7/77/Tower_of_God_Vol_1_Cover.jpg" },
                    new Truyen { MaTruyen = "TR022", TenTruyen = "Lookism - Tập 1", MaTL = "TL02", GiaBan = 30000, SoLuongTon = 80, imgUrl = "https://shop.delivered.co.kr/cdn/shop/products/lookismkmanhwabookvolume1dkshop.jpg?v=1679474219" },
                    new Truyen { MaTruyen = "TR023", TenTruyen = "Weak Hero - Tập 1", MaTL = "TL02", GiaBan = 31000, SoLuongTon = 85, imgUrl = "https://dilib.vn/img/news/2025/06/larger/12275-anh-hung-yeu-the-weakest-hero-1.jpg?v=7117" },
                    new Truyen { MaTruyen = "TR024", TenTruyen = "Dice - Tập 1", MaTL = "TL02", GiaBan = 30000, SoLuongTon = 100, imgUrl = "https://m.media-amazon.com/images/I/81gX+IvMMvL._AC_UF894,1000_QL80_.jpg" },
                    new Truyen { MaTruyen = "TR025", TenTruyen = "God of Highschool - Tập 1", MaTL = "TL02", GiaBan = 36000, SoLuongTon = 85, imgUrl = "https://images-na.ssl-images-amazon.com/images/I/91pyioyLTdL.jpg" },

                    // MANHUA
                    new Truyen { MaTruyen = "TR026", TenTruyen = "Battle Through the Heavens - Tập 1", MaTL = "TL03", GiaBan = 32000, SoLuongTon = 90, imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT4J6UBR3o2ItZUnffJdPR0hs8rYC0Q5-xqNQ&s" },
                    new Truyen { MaTruyen = "TR027", TenTruyen = "Battle Through the Heavens - Tập 2", MaTL = "TL03", GiaBan = 32000, SoLuongTon = 85, imgUrl = "https://m.media-amazon.com/images/I/71osp+RigCL._AC_UF1000,1000_QL80_.jpg" },
                    new Truyen { MaTruyen = "TR028", TenTruyen = "Feng Shen Ji - Tập 1", MaTL = "TL03", GiaBan = 30000, SoLuongTon = 80, imgUrl = "https://imgv2-1-f.scribdassets.com/img/document/842145441/298x396/81b02d6a8b/1742683537?v=1" },
                    new Truyen { MaTruyen = "TR029", TenTruyen = "Feng Shen Ji - Tập 2", MaTL = "TL03", GiaBan = 30000, SoLuongTon = 75, imgUrl = "https://m.media-amazon.com/images/S/compressed.photo.goodreads.com/books/1460356010i/19509852.jpg" },
                    new Truyen { MaTruyen = "TR030", TenTruyen = "Qi Yuan - Tập 1", MaTL = "TL03", GiaBan = 31000, SoLuongTon = 70, imgUrl = "https://preview.redd.it/though-i-am-an-inept-villainess-tale-of-the-butterfly-rat-v0-f1o1n66r38pe1.jpeg?width=640&crop=smart&auto=webp&s=0e856f32d9aae65cadfa34521eb7fbc55b7bb363" },
                    new Truyen { MaTruyen = "TR031", TenTruyen = "Tales of Demons and Gods - Tập 1", MaTL = "TL03", GiaBan = 33000, SoLuongTon = 95, imgUrl = "https://vidian.vn/img-thumbnail/img-thumbnail-1676453658767.jpg" },
                    new Truyen { MaTruyen = "TR032", TenTruyen = "Tales of Demons and Gods - Tập 2", MaTL = "TL03", GiaBan = 33000, SoLuongTon = 90, imgUrl = "https://vidian.vn/img-thumbnail/img-thumbnail-1676453658767.jpg" },
                    new Truyen { MaTruyen = "TR033", TenTruyen = "Soul Land - Tập 1", MaTL = "TL03", GiaBan = 34000, SoLuongTon = 85, imgUrl = "https://m.media-amazon.com/images/I/81A8W0C7X1S._UF1000,1000_QL80_.jpg" },
                    new Truyen { MaTruyen = "TR034", TenTruyen = "Soul Land - Tập 2", MaTL = "TL03", GiaBan = 34000, SoLuongTon = 80, imgUrl = "https://m.media-amazon.com/images/I/81A8W0C7X1S._UF1000,1000_QL80_.jpg" },
                    new Truyen { MaTruyen = "TR035", TenTruyen = "Doulou Dalu - Tập 1", MaTL = "TL03", GiaBan = 35000, SoLuongTon = 95, imgUrl = "https://play.google.com/books/publisher/content/images/frontcover/khQ3EQAAQBAJ?fife=w240-h345" },

                    // COMICS
                    new Truyen { MaTruyen = "TR036", TenTruyen = "Spiderman - Tập 1", MaTL = "TL04", GiaBan = 40000, SoLuongTon = 100, imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRR7tS98vWDv8nIyH0EwKRqviffLakgGwpT_g&s" },
                    new Truyen { MaTruyen = "TR037", TenTruyen = "Spiderman - Tập 2", MaTL = "TL04", GiaBan = 40000, SoLuongTon = 95, imgUrl = "https://salt.tikicdn.com/cache/w300/media/catalog/product/i/m/img384_6.jpg" },
                    new Truyen { MaTruyen = "TR038", TenTruyen = "Batman - Tập 1", MaTL = "TL04", GiaBan = 38000, SoLuongTon = 90, imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSE6pmr760KHpsnCDeYCGMwYMxspxF_TOKGpg&s" },
                    new Truyen { MaTruyen = "TR039", TenTruyen = "Batman - Tập 2", MaTL = "TL04", GiaBan = 38000, SoLuongTon = 85, imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSE6pmr760KHpsnCDeYCGMwYMxspxF_TOKGpg&s" },
                    new Truyen { MaTruyen = "TR040", TenTruyen = "Superman - Tập 1", MaTL = "TL04", GiaBan = 39000, SoLuongTon = 80, imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTSMKjWTObugjA4H5ZTB8aZgENbi8GLBGrAxg&s" },
                    new Truyen { MaTruyen = "TR041", TenTruyen = "Superman - Tập 2", MaTL = "TL04", GiaBan = 39000, SoLuongTon = 75, imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSkO1CstJpRA8Z4Jg2uQJ-hQ6JSB4UVlBKWrQ&s" },
                    new Truyen { MaTruyen = "TR042", TenTruyen = "Avengers - Tập 1", MaTL = "TL04", GiaBan = 42000, SoLuongTon = 100, imgUrl = "https://bookbuy.vn/Res/Images/Product/avengers-de-che-ultron_40182_1.jpg" },
                    new Truyen { MaTruyen = "TR043", TenTruyen = "Avengers - Tập 2", MaTL = "TL04", GiaBan = 42000, SoLuongTon = 95, imgUrl = "https://bookbuy.vn/Res/Images/Product/avengers-de-che-ultron_40182_1.jpg" },
                    new Truyen { MaTruyen = "TR044", TenTruyen = "X-Men - Tập 1", MaTL = "TL04", GiaBan = 41000, SoLuongTon = 90, imgUrl = "https://kenh14cdn.com/Images/Uploaded/Share/2012/01/26/120126kpxmen01.jpg" },
                    new Truyen { MaTruyen = "TR045", TenTruyen = "X-Men - Tập 2", MaTL = "TL04", GiaBan = 41000, SoLuongTon = 85, imgUrl = "https://kenh14cdn.com/Images/Uploaded/Share/2012/01/26/8c0120126kpxmen03.jpg" },

                    // WEBTOON
                    new Truyen { MaTruyen = "TR046", TenTruyen = "Lore Olympus - Tập 1", MaTL = "TL05", GiaBan = 25000, SoLuongTon = 100, imgUrl = "https://contentcafe2.btol.com/ContentCafe/Jacket.aspx?UserID=ContentCafeClient&Password=Client&Return=T&Type=M&Value=9780593356074" },
                    new Truyen { MaTruyen = "TR047", TenTruyen = "Lore Olympus - Tập 2", MaTL = "TL05", GiaBan = 25000, SoLuongTon = 95, imgUrl = "https://i5.walmartimages.com/seo/Lore-Olympus-Volume-Two-Paperback_379f4923-908f-44e5-9403-e553cc74e1a8.4f9b28961de555bca30146f7924591df.jpeg" },
                    new Truyen { MaTruyen = "TR048", TenTruyen = "Lets Play - Tập 1", MaTL = "TL05", GiaBan = 26000, SoLuongTon = 90, imgUrl = "https://preview.redd.it/lets-play-anime-key-visual-v0-x2gbtous2ukf1.jpeg?width=1080&crop=smart&auto=webp&s=0d1d07bf8332428e295730efce11550755718614" },
                    new Truyen { MaTruyen = "TR049", TenTruyen = "Lets Play - Tập 2", MaTL = "TL05", GiaBan = 26000, SoLuongTon = 85, imgUrl = "https://preview.redd.it/lets-play-anime-key-visual-v0-x2gbtous2ukf1.jpeg?width=1080&crop=smart&auto=webp&s=0d1d07bf8332428e295730efce11550755718614" },
                    new Truyen { MaTruyen = "TR050", TenTruyen = "Unordinary - Tập 1", MaTL = "TL05", GiaBan = 27000, SoLuongTon = 100, imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_EnlrNt8NNQr4zb0ILO2f6nwpSxCMawXKjg&s" },
                    new Truyen { MaTruyen = "TR051", TenTruyen = "Unordinary - Tập 2", MaTL = "TL05", GiaBan = 27000, SoLuongTon = 95, imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_EnlrNt8NNQr4zb0ILO2f6nwpSxCMawXKjg&s" },
                    new Truyen { MaTruyen = "TR052", TenTruyen = "True Beauty - Tập 1", MaTL = "TL05", GiaBan = 28000, SoLuongTon = 90, imgUrl = "https://i.vietgiaitri.com/2024/7/6/true-beauty-nhan-mua-gach-da-khi-duoc-chuyen-the-phien-ban-nhat-8f5-7205592.webp" },
                    new Truyen { MaTruyen = "TR053", TenTruyen = "True Beauty - Tập 2", MaTL = "TL05", GiaBan = 28000, SoLuongTon = 85, imgUrl = "https://i.vietgiaitri.com/2024/7/6/true-beauty-nhan-mua-gach-da-khi-duoc-chuyen-the-phien-ban-nhat-8f5-7205592.webp" },
                    new Truyen { MaTruyen = "TR054", TenTruyen = "Sweet Home - Tập 1", MaTL = "TL05", GiaBan = 29000, SoLuongTon = 80, imgUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTY1JXCCxbbRJW1iNYXhPHaVklPiTiAuJKFRg&s" },
                });
            }

            // Cập nhật lại ảnh bị hỏng cho truyện Dice (TR024)
            context.Database.ExecuteSqlCommand("UPDATE Truyens SET imgUrl = 'https://m.media-amazon.com/images/I/81gX+IvMMvL._AC_UF894,1000_QL80_.jpg' WHERE MaTruyen = 'TR024'");

            context.SaveChanges();
        }
    }
}
