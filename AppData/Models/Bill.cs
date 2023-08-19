using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class Bill
    {
        public Guid Id { get; set; }
        public string BillCode { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserId { get; set; }
        public int TotalPrice { get; set; }
        public int Status { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<BillDetail> Details { get; set; }
    }
}
