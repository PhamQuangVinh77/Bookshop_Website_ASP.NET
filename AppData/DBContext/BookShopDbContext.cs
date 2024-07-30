using AppData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.DBContext
{
    public class BookShopDbContext : DbContext
    {
        public BookShopDbContext()
        {
        }
        public BookShopDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Author> authors { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Bill> bills { get; set; }
        public DbSet<BillDetail> billsDetails { get; set; }
        public DbSet<Book> books { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartDetail> cartsDetails { get; set; }
        public DbSet<User> users { get; set; }  
        public DbSet<UserRole> userRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-8LST6U5\SQLEXPRESS;Initial Catalog=DATABOOKSTORE;Integrated Security=True;Trust Server Certificate=True"));
        }
    }
}
