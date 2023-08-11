using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required, EmailAddress(ErrorMessage = "Sai định dạng email!")]
        public string Email { get; set; }
        [Required, MinLength(10, ErrorMessage = "Password không được ít hơn 10 ký tự!")]
        public string Password { get; set; }
        [Required, Phone(ErrorMessage = "Sai định dạng số điện thoại!")]
        public string PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
        public bool Status { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<Bill> Bills { get; set; }
        
    }
}
