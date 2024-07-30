using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppData.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "*Bắt buộc nhập!")]
        [MaxLength(100, ErrorMessage = "*Tên SP không quá 100 ký tự!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*Bắt buộc nhập!")]
        public string ImageLink { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime OpeningDate { get; set; }
        public int NumberOfPurchase { get; set; }
        [ForeignKey("Author")]
        public Guid AuthorId { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        public bool Status { get; set; }
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<BillDetail> BillDetails { get; set; }

        public virtual ICollection<CartDetail> CartDetails { get; set; }
    }
}
