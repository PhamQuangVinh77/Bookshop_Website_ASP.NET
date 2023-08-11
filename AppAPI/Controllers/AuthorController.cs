using AppData.DBContext;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly BookShopDbContext _dbContext;

        public AuthorController(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/<AuthorController>
        [HttpGet]
        public IEnumerable<Author> GetAll()
        {
            return _dbContext.authors.ToList();
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public Author GetById(Guid id)
        {
            return _dbContext.authors.ToList().FirstOrDefault(i => i.Id == id);
        }

        // POST api/<AuthorController>
        [HttpPost]
        public string Post(Author author)
        {
            try
            {
                author.Id = Guid.NewGuid();
                _dbContext.Add(author);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi" + ex.Message);
            }
            return "Thêm thành công!";
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public string Put(Guid id, Author author)
        {
            try
            {
                var obj = _dbContext.authors.ToList().FirstOrDefault(x => x.Id == id);
                if(obj == null) {return "Không tồn tại!"};
                obj.AuthorName = author.AuthorName;
                obj.Status = author.Status;
                _dbContext.Update(obj);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                return ("Lỗi" + ex.Message);
            }
            return "Sửa thành công!";
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public string Delete(Guid id)
        {
            try
            {
                var obj = _dbContext.authors.ToList().FirstOrDefault(x => x.Id == id);
                if (obj == null) return "Không tồn tại";
                _dbContext.Remove(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Xóa thành công";
        }
    }
}
