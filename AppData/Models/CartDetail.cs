using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class CartDetail
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Book Book { get; set; }
    }
}
