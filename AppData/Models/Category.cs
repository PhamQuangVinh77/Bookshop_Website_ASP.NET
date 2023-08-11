﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
