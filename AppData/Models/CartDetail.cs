using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class CartDetail
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Cart")]
        public Guid UserId { get; set; }
        [ForeignKey("Book")]
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Book Book { get; set; }
    }
}
