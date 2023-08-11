using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class Cart
    {
        [Key]
        public Guid UserId { get; set; }
        public virtual ICollection<CartDetail> Details { get; set; }
        public virtual User User { get; set; }
    }
}
