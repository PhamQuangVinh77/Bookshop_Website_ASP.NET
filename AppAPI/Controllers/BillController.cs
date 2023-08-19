using AppData.DBContext;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly BookShopDbContext _dbContext;

        public BillController(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<BillController>
        [HttpGet]
        public IEnumerable<Bill> GetAll()
        {
            return _dbContext.bills.ToList();
        }

        // GET api/<BillController>/5
        [HttpGet("{id}")]
        public Bill GetById(Guid id)
        {
            return _dbContext.bills.ToList().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<BillController>
        [HttpPost]
        public string Post(Bill bill)
        {
            try
            {
                bill.Id = Guid.NewGuid();
                bill.BillCode = "HD" + Utilities.Utility.GenerateRandomString(4);
                _dbContext.Add(bill);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Thêm thành công!";
        }

        // PUT api/<BillController>/5
        [HttpPut("{id}")]
        public string Put(Guid id, Bill bill)
        {
            try
            {
                var obj = _dbContext.bills.ToList().FirstOrDefault(x => x.Id == id);
                if (obj == null) return "Không tồn tại!";
                obj.Status = bill.Status;
                _dbContext.Update(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Sửa thành công!";
        }

        // DELETE api/<BillController>/5
        [HttpDelete("{id}")]
        public string Delete(Guid id)
        {
            try
            {
                var obj = _dbContext.bills.ToList().FirstOrDefault(x => x.Id == id);
                if (obj == null) return "Không tồn tại!";
                obj.Status = 0;
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
