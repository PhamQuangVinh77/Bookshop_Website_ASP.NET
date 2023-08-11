using AppData.DBContext;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly BookShopDbContext _dbContext;

        public CartController(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<CartController>
        [HttpGet]
        public IEnumerable<Cart> GetAll()
        {
            return _dbContext.carts.ToList();
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public Cart GetById(Guid id)
        {
            return _dbContext.carts.ToList().FirstOrDefault(x => x.UserId == id);
        }

        // POST api/<CartController>
        [HttpPost]
        public string Post(Cart cart)
        {
            try
            {
                _dbContext.Add(cart);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Thêm thành công!";
        }


        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public string Delete(Guid id)
        {
            try
            {
                var obj = _dbContext.carts.ToList().FirstOrDefault(x => x.UserId == id);
                if (obj == null) return "Không tồn tại!";
                _dbContext.Remove(obj);
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
