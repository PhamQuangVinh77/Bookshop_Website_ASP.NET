using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "*Bắt buộc nhập")]
        [EmailAddress(ErrorMessage = "*Sai định dạng email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Bắt buộc nhập")]
        [MinLength(10, ErrorMessage = "Password không được ít hơn 10 ký tự!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "*Bắt buộc nhập")]
        [MaxLength(50, ErrorMessage = "Tên không quá 50 ký tự!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*Bắt buộc nhập")]
        [Phone(ErrorMessage = "Sai định dạng số điện thoại!")]
        public string PhoneNumber { get; set; }
        [ForeignKey("UserRole")]
        public Guid RoleId { get; set; }
        public bool Status { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        
    }
}
