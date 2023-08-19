using AppData.DBContext;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookShopDbContext _dbContext;

        public BookController(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<BookController>
        [HttpGet]
        public IEnumerable<Book> GetAll()
        {
            return _dbContext.books.ToList();
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public Book GetById(Guid id)
        {
            return _dbContext.books.ToList().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<BookController>
        [HttpPost]
        public string Post(Book book)
        {
            try
            {
                book.Id = Guid.NewGuid();
                book.OpeningDate = DateTime.Now;
                book.NumberOfPurchase = 0;
                _dbContext.Add(book);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Thêm thành công!";
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public string Put(Guid id, Book book)
        {
            try
            {
                var obj = _dbContext.books.ToList().FirstOrDefault(x => x.Id == id);
                if (obj == null) return "Không tồn tại!";
                obj.Name = book.Name;
                obj.ImageLink = book.ImageLink;
                obj.Description = book.Description;
                obj.Price = book.Price;
                obj.AvailableQuantity = book.AvailableQuantity;
                obj.AuthorId = book.AuthorId;
                obj.CategoryId = book.CategoryId;
                obj.Status = book.Status;
                _dbContext.Update(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Sửa thành công!";
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public string Delete(Guid id)
        {
            try
            {
                var obj = _dbContext.books.ToList().FirstOrDefault(x => x.Id == id);
                if (obj == null) return "Không tồn tại!";
                obj.Status = false;
                _dbContext.Update(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Xóa thành công!";
        }
    }
}
