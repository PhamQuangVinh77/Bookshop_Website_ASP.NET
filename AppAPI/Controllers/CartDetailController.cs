using AppData.DBContext;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartDetailController : ControllerBase
    {
        private readonly BookShopDbContext _dbContext;

        public CartDetailController(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<CartDetailController>
        [HttpGet]
        public IEnumerable<CartDetail> GetAll()
        {
            return _dbContext.cartsDetails.ToList();
        }

        // GET api/<CartDetailController>/5
        [HttpGet("{id}")]
        public CartDetail GetById(Guid id)
        {
            return _dbContext.cartsDetails.ToList().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<CartDetailController>
        [HttpPost]
        public string Post(CartDetail cd)
        {
            try
            {
                cd.Id = Guid.NewGuid();
                _dbContext.Add(cd);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Thêm thành công!";
        }

        // PUT api/<CartDetailController>/5
        [HttpPut("{id}")]
        public string Put(Guid id, CartDetail cd)
        {
            try
            {
                var obj = _dbContext.cartsDetails.ToList().FirstOrDefault(x => x.Id == id);
                if (obj == null) return "Không tồn tại!";
                obj.UserId = cd.UserId;
                obj.BookId = cd.BookId;
                obj.Quantity = cd.Quantity;
                _dbContext.Update(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Sửa thành công!";
        }

        // DELETE api/<CartDetailController>/5
        [HttpDelete("{id}")]
        public string Delete(Guid id)
        {
            try
            {
                var obj = _dbContext.cartsDetails.ToList().FirstOrDefault(x => x.Id == id);
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
