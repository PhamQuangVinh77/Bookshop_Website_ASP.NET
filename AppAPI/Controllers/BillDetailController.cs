using AppData.DBContext;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillDetailController : ControllerBase
    {
        private readonly BookShopDbContext _dbContext;

        public BillDetailController(BookShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<BillDetailController>
        [HttpGet]
        public IEnumerable<BillDetail> GetAll()
        {
            return _dbContext.billsDetails.ToList();
        }

        // GET api/<BillDetailController>/5
        [HttpGet("{id}")]
        public BillDetail GetById(Guid id)
        {
            return _dbContext.billsDetails.ToList().FirstOrDefault(x => x.Id == id);
        }

        // POST api/<BillDetailController>
        [HttpPost]
        public string Post(BillDetail bd)
        {
            try
            {
                bd.Id = Guid.NewGuid();
                _dbContext.Add(bd);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Thêm thành công!";
        }

        // PUT api/<BillDetailController>/5
        [HttpPut("{id}")]
        public string Put(Guid id, BillDetail bd)
        {
            try
            {
                var obj = _dbContext.billsDetails.ToList().FirstOrDefault(x => x.Id == id);
                if (obj == null) return "Không tồn tại!";
                obj.BookId = bd.BookId;
                obj.BillId = bd.BillId;
                obj.Quantity = bd.Quantity;
                obj.Price = bd.Price;
                _dbContext.Update(obj);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ("Lỗi: " + ex.Message);
            }
            return "Sửa thành công!";
        }

        // DELETE api/<BillDetailController>/5
        [HttpDelete("{id}")]
        public string Delete(Guid id)
        {
            try
            {
                var obj = _dbContext.billsDetails.ToList().FirstOrDefault(x => x.Id == id);
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
