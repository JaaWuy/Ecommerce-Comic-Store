using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAnWeb.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailId { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        [StringLength(5)]
        public string MaTruyen { get; set; }

        [Required]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Đơn giá")]
        public decimal Price { get; set; }

        [Display(Name = "Thành tiền")]
        public decimal SubTotal 
        { 
            get { return Quantity * Price; } 
        }

        // Navigation properties
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        [ForeignKey("MaTruyen")]
        public virtual Truyen Truyen { get; set; }
    }
}
