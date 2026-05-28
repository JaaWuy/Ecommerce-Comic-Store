using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnWeb.Models
{
    public class Truyen
    {
        [Key]
        [StringLength(5)]
        public string MaTruyen { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên truyện")]
        [StringLength(100)]
        [Display(Name = "Tên truyện")]
        public string TenTruyen { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "Thể loại")]
        public string MaTL { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá bán")]
        [Display(Name = "Giá bán")]

        [Range(0, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn 0")]
        public decimal GiaBan { get; set; }

        [Required]
        [Display(Name = "Số lượng tồn")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải lớn hơn hoặc bằng 0")]
        public int SoLuongTon { get; set; }

        [StringLength(255)]
        [Display(Name = "Hình ảnh")]
        public string imgUrl { get; set; }

        // Navigation properties
        [ForeignKey("MaTL")]
        public virtual TheLoai TheLoai { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}