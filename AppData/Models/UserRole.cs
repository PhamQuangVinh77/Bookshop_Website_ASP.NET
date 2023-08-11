using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class UserRole
    {
        [Key]
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Status { get; set; }    
        public virtual ICollection<User> Users { get; set; }
    }
}
