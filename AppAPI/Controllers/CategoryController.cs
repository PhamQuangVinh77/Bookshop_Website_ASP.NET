using AppData.DBContext;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly BookShopDbContext _dbContext;

        public CategoryController(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return _dbContext.categories.ToList();
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public Category GetById(Guid id)
        {
            return _dbContext.categories.ToList().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public string Post(Category category)
        {
            try
            {
                category.Id = Guid.NewGuid();
                _dbContext.Add(category);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Thêm thành công!";
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public string Put(Guid id, Category category)
        {
            try
            {
                var obj = _dbContext.categories.ToList().FirstOrDefault(x => x.Id == id);
                if (obj == null) return "Không tồn tại!";
                obj.CategoryName = category.CategoryName;
                obj.Status = category.Status;
                _dbContext.Update(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Sửa thành công!";
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public string Delete(Guid id)
        {
            try
            {
                var obj = _dbContext.categories.ToList().FirstOrDefault(x => x.Id == id);
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
