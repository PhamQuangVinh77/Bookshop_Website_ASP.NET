using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class BillDetail
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid BillId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public virtual Bill Bill { get; set; }
        public virtual Book Book { get; set; }
    }
}
