using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Tên SP không quá 100 ký tự!")]
        public string Name { get; set; }
        [MaxLength(1000, ErrorMessage = "Mô tả quá dài!")]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }

        public bool Status { get; set; }
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<BillDetail> BillDetails { get; set;}

        public virtual ICollection<CartDetail> CartDetails { get; set;}
    }
}
