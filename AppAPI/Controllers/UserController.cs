using AppData.DBContext;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BookShopDbContext _dbContext;

        public UserController(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _dbContext.users.ToList();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User GetById(Guid id)
        {
            return _dbContext.users.ToList().FirstOrDefault(x => x.UserId == id);
        }

        // POST api/<UserController>
        [HttpPost]
        public string Post(User user)
        {
            try
            {
                user.UserId = Guid.NewGuid();
                _dbContext.users.Add(user);

                var cart = new Cart();
                cart.UserId = user.UserId;
                _dbContext.carts.Add(cart);

                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Thêm thành công!";
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public string Put(Guid id, User user)
        {
            try
            {
                var obj = _dbContext.users.ToList().FirstOrDefault(x => x.UserId == id);
                if (obj == null) return "Không tồn tại!";
                obj.Name = user.Name;
                obj.Email = user.Email;
                obj.Password = user.Password;
                obj.PhoneNumber = user.PhoneNumber;
                obj.RoleId = user.RoleId;
                obj.Status = user.Status;
                _dbContext.Update(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Sửa thành công!";
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public string Delete(Guid id)
        {
            try
            {
                var obj = _dbContext.users.ToList().FirstOrDefault(x => x.UserId == id);
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
