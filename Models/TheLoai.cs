using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DoAnWeb.Models
{
    public class TheLoai
    {
        [Key]
        [StringLength(5)]
        public string MaTL { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên thể loại")]
        [StringLength(50)]
        [Display(Name = "Tên thể loại")]
        public string TenTL { get; set; }

        [StringLength(255)]
        [Display(Name = "Mô tả")]
        public string MoTa { get; set; }

        // Navigation property
        public virtual ICollection<Truyen> Truyens { get; set; }
    }
}