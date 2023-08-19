using AppData.DBContext;
using AppData.Models;
using AppView.Services;
using AppView.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace AppView.Controllers
{
    public class BillController : Controller
    {
        private readonly ILogger<BillController> _logger;
        BookShopDbContext _dbContext;
        HttpClient _httpClient;
        public BillController(ILogger<BillController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _dbContext = new BookShopDbContext();
        }

        [HttpPost("/Bill/Purchase")]
        [ValidateAntiForgeryToken]
        //Một thuộc tính trong ASP.NET Core có tác dụng
        //như một biện pháp bảo mật khi post dữ liệu lên
        public IActionResult Purchase([Bind("Id, BookId, Quantity, UserId")] CartDetail cartDetail)
        {
            Bill bill = new Bill();
            bill.Id = Guid.NewGuid();
            bill.BillCode = "HD" + Utility.GenerateRandomString(4);
            bill.UserId = cartDetail.UserId;
            bill.CreateDate = DateTime.Now;
            bill.Status = 1;
            bill.TotalPrice = 0;
            _dbContext.bills.Add(bill);

            var cartDetails = new List<CartDetail>();
            if (HttpContext.Session.GetString("UserName") == null)
            {
                cartDetails = SessionServices.GetObjFromSession(HttpContext.Session, "Cart");
            }
            else
            {
                cartDetails = _dbContext.cartsDetails.Where(x => x.UserId.ToString() ==
                HttpContext.Session.GetString("UserId")).ToList();
            }

            foreach (var item in cartDetails)
            {
                BillDetail bd = new BillDetail();
                bd.Id = Guid.NewGuid();
                bd.BookId = item.BookId;
                bd.BillId = bill.Id;
                bd.Quantity = item.Quantity;
                bd.Price = _dbContext.books.ToList().FirstOrDefault(x => x.Id == item.BookId).Price;
                _dbContext.billsDetails.Add(bd);

                bill.TotalPrice += bd.Price * bd.Quantity;

                var sp = _dbContext.books.ToList().FirstOrDefault(x => x.Id == item.BookId);
                sp.AvailableQuantity -= item.Quantity;
                sp.NumberOfPurchase += item.Quantity;
                if (sp.AvailableQuantity <= 0)
                {
                    sp.Status = false;
                }
                _dbContext.books.Update(sp);
            }
            _dbContext.SaveChanges();

            if (HttpContext.Session.GetString("UserName") == null)
            {
                HttpContext.Session.Remove("Cart");

                string billsInCookie = "";
                billsInCookie = Request.Cookies["BillCookie"];
                if (billsInCookie == "" || billsInCookie == null)
                {
                    billsInCookie = bill.BillCode;
                }
                else
                {
                    billsInCookie = billsInCookie + "-" + bill.BillCode;
                }
                CookieOptions options = new CookieOptions();
                // Gộp danh sách mã HD thành 1 string dạng "BillCode1-BillCode2-..."
                Response.Cookies.Append("BillCookie", billsInCookie, options);
                // Lưu string chứa danh sách mã HD trên vào cookie
            }
            else
            {
                 var lstCD = _dbContext.cartsDetails.Where(x => x.UserId.ToString() ==
                HttpContext.Session.GetString("UserId")).ToList();
                foreach (var item in lstCD)
                {
                    _dbContext.cartsDetails.Remove(item);
                    _dbContext.SaveChanges();
                }
            }

            return RedirectToAction("ShowAllBills");
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllBills()
        {
            List<Bill> showBills = new List<Bill>();

            if (HttpContext.Session.GetString("UserName") == null)
            {
                string billsInCookie = Request.Cookies["BillCookie"];
                if (billsInCookie != null && billsInCookie != "")
                {
                    string[] lstBills = billsInCookie.Split("-"); // Lấy ra từng mã hóa đơn ở cookie

                    var api = "https://localhost:7287/api/Bill";
                    var res = await _httpClient.GetFromJsonAsync<List<Bill>>(api); // Lấy danh sách hóa đơn từ API

                    foreach (var item in lstBills)
                    {
                        var billInAPI = res.FirstOrDefault(x => x.BillCode == item);
                        showBills.Add(billInAPI);
                    }
                }
            }
            else
            {
                showBills = _dbContext.bills.Where(x => x.UserId.ToString() == HttpContext.Session.GetString("UserId")).ToList();
            }
            return View(showBills);
        }

        public IActionResult Delete(Guid id)
        {
            var bill = _dbContext.bills.ToList().FirstOrDefault(x => x.Id == id);
            bill.Status = 0;
            
            var billDetails = _dbContext.billsDetails.ToList().Where(x => x.BillId == id);
            foreach(var item in billDetails)
            {
                var book = _dbContext.books.ToList().FirstOrDefault(x => x.Id == item.BookId);
                book.AvailableQuantity += item.Quantity;
                book.NumberOfPurchase -= item.Quantity; 
                if(book.Status == false)
                {
                    book.Status = true;
                }
                _dbContext.books.Update(book);
            }

            _dbContext.SaveChanges();
            return RedirectToAction("ShowAllBills");
        }

        public IActionResult GoodsReceived(Guid id)
        {
            var bill = _dbContext.bills.ToList().FirstOrDefault(x => x.Id == id);
            bill.Status = 3;
            _dbContext.bills.Update(bill);
            _dbContext.SaveChanges();
            return RedirectToAction("ShowAllBills");
        }
        public async Task<IActionResult> BillDetails(Guid id)
        {
            var api = "https://localhost:7287/api/BillDetail";
            var res = await _httpClient.GetFromJsonAsync<List<BillDetail>>(api);
            List<BillDetail> lstBillDetails = res.Where(x => x.BillId == id).ToList();
            ViewBag.Code = _dbContext.bills.FirstOrDefault(x => x.Id == id).BillCode;
            ViewBag.TotalPrice = _dbContext.bills.FirstOrDefault(x => x.Id == id).TotalPrice;
            ViewBag.Books = _dbContext.books.ToList();
            return View(lstBillDetails);
        }
    }
}
