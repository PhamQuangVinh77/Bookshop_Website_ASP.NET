using AppData.DBContext;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly BookShopDbContext _dbContext;

        public UserRoleController(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<UserRoleController>
        [HttpGet]
        public IEnumerable<UserRole> GetAll()
        {
            return _dbContext.userRoles.ToList();
        }

        // GET api/<UserRoleController>/5
        [HttpGet("{id}")]
        public UserRole GetById(Guid id)
        {
            return _dbContext.userRoles.ToList().FirstOrDefault(x => x.RoleId == id);
        }

        // POST api/<UserRoleController>
        [HttpPost]
        public string Post(UserRole role)
        {
            try
            {
                role.RoleId = Guid.NewGuid();
                _dbContext.Add(role);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Thêm thành công!";
        }

        // PUT api/<UserRoleController>/5
        [HttpPut("{id}")]
        public string Put(Guid id, UserRole role)
        {
            try
            {
                var obj = _dbContext.userRoles.ToList().FirstOrDefault(x => x.RoleId == id);
                if (obj == null) return "Không tồn tại!";
                obj.RoleName = role.RoleName;
                obj.Status = role.Status;
                _dbContext.Update(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Sửa thành công!";
        }

        // DELETE api/<UserRoleController>/5
        [HttpDelete("{id}")]
        public string Delete(Guid id)
        {
            try
            {
                var obj = _dbContext.userRoles.ToList().FirstOrDefault(x => x.RoleId == id);
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
