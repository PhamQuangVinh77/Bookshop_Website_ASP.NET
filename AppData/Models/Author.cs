using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(50, ErrorMessage = "Tên tác giả không vượt quá 50 ký tự!")]
        public string AuthorName { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
