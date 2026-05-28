CREATE DATABASE QL_CUAHANGTRUYEN
GO
USE QL_CUAHANGTRUYEN
GO

CREATE TABLE TheLoai (
    MaTL CHAR(5) PRIMARY KEY,
    TenTL NVARCHAR(50) NOT NULL,
    MoTa NVARCHAR(255)
);

CREATE TABLE Truyen (
    MaTruyen CHAR(5) PRIMARY KEY,          
    TenTruyen NVARCHAR(100) NOT NULL,      
    MaTL CHAR(5) NOT NULL,                 
    GiaBan DECIMAL(10,2) CHECK (GiaBan > 0),   
    SoLuongTon INT CHECK (SoLuongTon >= 0),    
    imgUrl NVARCHAR(255) NULL,             
    FOREIGN KEY (MaTL) REFERENCES TheLoai(MaTL) 
);


INSERT INTO TheLoai (MaTL, TenTL, MoTa) VALUES
('TL01', N'Manga', N'Truyện tranh Nhật Bản'),
('TL02', N'Manhwa', N'Truyện tranh Hàn Quốc'),
('TL03', N'Manhua', N'Truyện tranh Trung Quốc'),
('TL04', N'Comics', N'Truyện tranh phương Tây'),
('TL05', N'Webtoon', N'Truyện tranh phát hành trực tuyến');

-- =========================
-- MANGA 
-- =========================
INSERT INTO Truyen VALUES
('TR001', N'One Piece - Tập 1', 'TL01', 25000, 100, 'https://cdn1.fahasa.com/media/catalog/product/8/9/8935244865097.jpg'),
('TR002', N'One Piece - Tập 2', 'TL01', 25000, 90, 'https://bizweb.dktcdn.net/100/567/082/products/2-7c49f80cdc1c48b9a483d9d66fd684c4-master.jpg?v=1747326559600'),
('TR003', N'One Piece - Tập 3', 'TL01', 25000, 80, 'https://cdn1.fahasa.com/media/catalog/product/8/9/8935244865110.jpg'),
('TR004', N'Naruto - Tập 1', 'TL01', 28000, 95, 'https://cdn1.fahasa.com/media/catalog/product/n/a/naruto---tap-1---tb-2022_1.jpg'),
('TR005', N'Naruto - Tập 2', 'TL01', 28000, 90, 'https://cdn1.fahasa.com/media/catalog/product/n/a/naruto---tap-2_1.jpg'),
('TR006', N'Dragon Ball - Tập 1', 'TL01', 30000, 120, 'https://cdn1.fahasa.com/media/catalog/product/d/r/dragon_ball_7_vien_ngoc_rong_bia_tap_1_tb_2025.jpg'),
('TR007', N'Dragon Ball - Tập 2', 'TL01', 30000, 115, 'https://cdn1.fahasa.com/media/catalog/product/d/r/dragon-ball-full-color-tap-2.jpg'),
('TR008', N'Dragon Ball - Tập 3', 'TL01', 30000, 110, 'https://product.hstatic.net/1000376556/product/dragon_ball_full_color_tap_3_c51433d7bc764bbaac0d31b0955fb649_1024x1024.jpg'),
('TR009', N'Demon Slayer - Tập 1', 'TL01', 35000, 100, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQFNpkPPwTM36Q24J--eTlUmYoRwIalZkJ3-Q&s'),
('TR010', N'Demon Slayer - Tập 2', 'TL01', 35000, 95, 'https://product.hstatic.net/1000376556/product/thanh_guom_diet_quy_2_7c95e7c4ac9c49e1908365fbc641127c_1024x1024.jpg'),
('TR011', N'Bleach - Tập 1', 'TL01', 30000, 75, 'https://bookbuy.vn/Res/Images/Product/su-mang-than-chet-tap-1-(tai-ban-2014)_35360_1.jpg'),
('TR012', N'Tokyo Ghoul - Tập 1', 'TL01', 32000, 65, 'https://cdn1.fahasa.com/media/flashmagazine/images/page_images/_1___tokyo_ghoul_1/2021_08_03_14_20_59_1-390x510.jpg'),
('TR013', N'Tokyo Ghoul - Tập 2', 'TL01', 32000, 60, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTkXBfoaf-jKQQzRnTMotO-eHeN-lElEGztdg&s'),
('TR014', N'My Hero Academia - Tập 1', 'TL01', 32000, 80, 'https://sachtiengviet.com/cdn/shop/products/87ed83fbce339ea5b05eb11f1b3e65de.jpg?v=1702245473'),
('TR015', N'My Hero Academia - Tập 2', 'TL01', 32000, 75, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTJ8ApSbHl6aRu7e3k7SMXdSOatt2VUFNpWlg&s');

-- =========================
-- MANHWA 
-- =========================
INSERT INTO Truyen VALUES
('TR016', N'Solo Leveling - Tập 1', 'TL02', 35000, 85, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQy1TfB_xd1YrXzPXTWiUpA1fEwEo-5BGBjtQ&s'),
('TR017', N'Solo Leveling - Tập 2', 'TL02', 35000, 80, 'https://salt.tikicdn.com/ts/product/1a/4b/49/64d424642a5b222ee7d6bda7a3152255.jpg'),
('TR018', N'The Beginning After The End - Tập 1', 'TL02', 38000, 70, 'https://m.media-amazon.com/images/I/81eqlOf4owL._AC_UF1000,1000_QL80_.jpg'),
('TR019', N'Omniscient Reader’s Viewpoint - Tập 1', 'TL02', 36000, 90, 'https://m.media-amazon.com/images/I/81ZuZO5vr2L._SL1500_.jpg'),
('TR020', N'Tower of God - Tập 1', 'TL02', 34000, 100, 'https://upload.wikimedia.org/wikipedia/vi/7/77/Tower_of_God_Vol_1_Cover.jpg'),
('TR021', N'Tower of God - Tập 2', 'TL02', 34000, 95, 'https://upload.wikimedia.org/wikipedia/vi/7/77/Tower_of_God_Vol_1_Cover.jpg'),
('TR022', N'Lookism - Tập 1', 'TL02', 30000, 80, 'https://shop.delivered.co.kr/cdn/shop/products/lookismkmanhwabookvolume1dkshop.jpg?v=1679474219'),
('TR023', N'Weak Hero - Tập 1', 'TL02', 31000, 85, 'https://dilib.vn/img/news/2025/06/larger/12275-anh-hung-yeu-the-weakest-hero-1.jpg?v=7117'),
('TR024', N'Dice - Tập 1', 'TL02', 30000, 100, 'https://truyenqq.com.vn/media/book/dice-I1Pz.jpg'),
('TR025', N'God of Highschool - Tập 1', 'TL02', 36000, 85, 'https://images-na.ssl-images-amazon.com/images/I/91pyioyLTdL.jpg');

-- =========================
-- MANHUA 
-- =========================
INSERT INTO Truyen VALUES
('TR026', N'Battle Through the Heavens - Tập 1', 'TL03', 32000, 90, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT4J6UBR3o2ItZUnffJdPR0hs8rYC0Q5-xqNQ&s'),
('TR027', N'Battle Through the Heavens - Tập 2', 'TL03', 32000, 85, 'https://m.media-amazon.com/images/I/71osp+RigCL._AC_UF1000,1000_QL80_.jpg'),
('TR028', N'Feng Shen Ji - Tập 1', 'TL03', 30000, 80, 'https://imgv2-1-f.scribdassets.com/img/document/842145441/298x396/81b02d6a8b/1742683537?v=1'),
('TR029', N'Feng Shen Ji - Tập 2', 'TL03', 30000, 75, 'https://m.media-amazon.com/images/S/compressed.photo.goodreads.com/books/1460356010i/19509852.jpg'),
('TR030', N'Qi Yuan - Tập 1', 'TL03', 31000, 70, 'https://preview.redd.it/though-i-am-an-inept-villainess-tale-of-the-butterfly-rat-v0-f1o1n66r38pe1.jpeg?width=640&crop=smart&auto=webp&s=0e856f32d9aae65cadfa34521eb7fbc55b7bb363'),
('TR031', N'Tales of Demons and Gods - Tập 1', 'TL03', 33000, 95, 'https://vidian.vn/img-thumbnail/img-thumbnail-1676453658767.jpg'),
('TR032', N'Tales of Demons and Gods - Tập 2', 'TL03', 33000, 90, 'https://vidian.vn/img-thumbnail/img-thumbnail-1676453658767.jpg'),
('TR033', N'Soul Land - Tập 1', 'TL03', 34000, 85, 'https://m.media-amazon.com/images/I/81A8W0C7X1S._UF1000,1000_QL80_.jpg'),
('TR034', N'Soul Land - Tập 2', 'TL03', 34000, 80, 'https://m.media-amazon.com/images/I/81A8W0C7X1S._UF1000,1000_QL80_.jpg'),
('TR035', N'Doulou Dalu - Tập 1', 'TL03', 35000, 95, 'https://play.google.com/books/publisher/content/images/frontcover/khQ3EQAAQBAJ?fife=w240-h345');

-- =========================
-- COMICS
-- =========================
INSERT INTO Truyen VALUES
('TR036', N'Spiderman - Tập 1', 'TL04', 40000, 100, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRR7tS98vWDv8nIyH0EwKRqviffLakgGwpT_g&s'),
('TR037', N'Spiderman - Tập 2', 'TL04', 40000, 95, 'https://salt.tikicdn.com/cache/w300/media/catalog/product/i/m/img384_6.jpg'),
('TR038', N'Batman - Tập 1', 'TL04', 38000, 90, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSE6pmr760KHpsnCDeYCGMwYMxspxF_TOKGpg&s'),
('TR039', N'Batman - Tập 2', 'TL04', 38000, 85, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSE6pmr760KHpsnCDeYCGMwYMxspxF_TOKGpg&s'),
('TR040', N'Superman - Tập 1', 'TL04', 39000, 80, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTSMKjWTObugjA4H5ZTB8aZgENbi8GLBGrAxg&s'),
('TR041', N'Superman - Tập 2', 'TL04', 39000, 75, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSkO1CstJpRA8Z4Jg2uQJ-hQ6JSB4UVlBKWrQ&s'),
('TR042', N'Avengers - Tập 1', 'TL04', 42000, 100, 'https://bookbuy.vn/Res/Images/Product/avengers-de-che-ultron_40182_1.jpg'),
('TR043', N'Avengers - Tập 2', 'TL04', 42000, 95, 'https://bookbuy.vn/Res/Images/Product/avengers-de-che-ultron_40182_1.jpg'),
('TR044', N'X-Men - Tập 1', 'TL04', 41000, 90, 'https://kenh14cdn.com/Images/Uploaded/Share/2012/01/26/120126kpxmen01.jpg'),
('TR045', N'X-Men - Tập 2', 'TL04', 41000, 85, 'https://kenh14cdn.com/Images/Uploaded/Share/2012/01/26/8c0120126kpxmen03.jpg');

-- =========================
-- WEBTOON
-- =========================
INSERT INTO Truyen VALUES
('TR046', N'Lore Olympus - Tập 1', 'TL05', 25000, 100, 'https://contentcafe2.btol.com/ContentCafe/Jacket.aspx?UserID=ContentCafeClient&Password=Client&Return=T&Type=M&Value=9780593356074'),
('TR047', N'Lore Olympus - Tập 2', 'TL05', 25000, 95, 'https://i5.walmartimages.com/seo/Lore-Olympus-Volume-Two-Paperback_379f4923-908f-44e5-9403-e553cc74e1a8.4f9b28961de555bca30146f7924591df.jpeg'),
('TR048', N'Lets Play - Tập 1', 'TL05', 26000, 90, 'https://preview.redd.it/lets-play-anime-key-visual-v0-x2gbtous2ukf1.jpeg?width=1080&crop=smart&auto=webp&s=0d1d07bf8332428e295730efce11550755718614'),
('TR049', N'Lets Play - Tập 2', 'TL05', 26000, 85, 'https://preview.redd.it/lets-play-anime-key-visual-v0-x2gbtous2ukf1.jpeg?width=1080&crop=smart&auto=webp&s=0d1d07bf8332428e295730efce11550755718614'),
('TR050', N'Unordinary - Tập 1', 'TL05', 27000, 100, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_EnlrNt8NNQr4zb0ILO2f6nwpSxCMawXKjg&s'),
('TR051', N'Unordinary - Tập 2', 'TL05', 27000, 95, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_EnlrNt8NNQr4zb0ILO2f6nwpSxCMawXKjg&s'),
('TR052', N'True Beauty - Tập 1', 'TL05', 28000, 90, 'https://i.vietgiaitri.com/2024/7/6/true-beauty-nhan-mua-gach-da-khi-duoc-chuyen-the-phien-ban-nhat-8f5-7205592.webp'),
('TR053', N'True Beauty - Tập 2', 'TL05', 28000, 85, 'https://i.vietgiaitri.com/2024/7/6/true-beauty-nhan-mua-gach-da-khi-duoc-chuyen-the-phien-ban-nhat-8f5-7205592.webp'),
('TR054', N'Sweet Home - Tập 1', 'TL05', 29000, 80, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTY1JXCCxbbRJW1iNYXhPHaVklPiTiAuJKFRg&s');

