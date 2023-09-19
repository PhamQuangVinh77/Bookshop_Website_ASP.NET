using AppData.DBContext;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;

namespace AppView.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AccessController> _logger;
        BookShopDbContext _dbContext;
        HttpClient http;
        public AdminController(ILogger<AccessController> logger)
        {
            _logger = logger;
            http = new HttpClient();
            _dbContext = new BookShopDbContext();
        }

		[HttpGet]
		public async Task<IActionResult> BillsForAdmin()
		{
            ViewBag.ListUser = _dbContext.users.ToList();
			var api = "https://localhost:7287/api/Bill";
			var res = await http.GetFromJsonAsync<List<Bill>>(api);
			return View(res);
		}

        [HttpGet]
        public async Task<IActionResult> BooksForAdmin()
        {
            ViewBag.ListAuthor = _dbContext.authors.ToList();
            ViewBag.ListCategory = _dbContext.categories.ToList();
            var api = "https://localhost:7287/api/Book";
            var res = await http.GetFromJsonAsync<List<Book>>(api);
            return View(res);
        }

        public IActionResult BillConfirm(Guid id)
        {
            var bill = _dbContext.bills.ToList().FirstOrDefault(x => x.Id == id);
            bill.Status = 2;
            _dbContext.bills.Update(bill);
            _dbContext.SaveChanges();
            return RedirectToAction("BillsForAdmin");
        }

        public async Task<IActionResult> BillDetailsForAdmin(Guid id)
        {
            var api = "https://localhost:7287/api/BillDetail";
            var res = await http.GetFromJsonAsync<List<BillDetail>>(api);
            List<BillDetail> lstBillDetails = res.Where(x => x.BillId == id).ToList();
            ViewBag.CodeAdmin = _dbContext.bills.FirstOrDefault(x => x.Id == id).BillCode;
            ViewBag.TotalPriceAdmin = _dbContext.bills.FirstOrDefault(x => x.Id == id).TotalPrice;
            ViewBag.BooksAdmin = _dbContext.books.ToList();
            return View(lstBillDetails);
        }

        public IActionResult DeleteBillForAdmin(Guid id)
        {
            var bill = _dbContext.bills.ToList().FirstOrDefault(x => x.Id == id);
            bill.Status = 0;

            var billDetails = _dbContext.billsDetails.ToList().Where(x => x.BillId == id);
            foreach (var item in billDetails)
            {
                var book = _dbContext.books.ToList().FirstOrDefault(x => x.Id == item.BookId);
                book.AvailableQuantity += item.Quantity;
                book.NumberOfPurchase -= item.Quantity;
                if (book.Status == false)
                {
                    book.Status = true;
                }
                _dbContext.books.Update(book);
            }

            _dbContext.SaveChanges();
            return RedirectToAction("BillsForAdmin");
        }

        public IActionResult DeleteBook(Guid id)
        {
            var book = _dbContext.books.FirstOrDefault(x => x.Id == id);
            book.Status = false;
            book.Description = "Ngừng kinh doanh";
            _dbContext.SaveChanges();
            return RedirectToAction("BooksForAdmin");
        }

        public IActionResult CreateBook()
        {
            List<Author> lstAuthor = _dbContext.authors.ToList();
            List<Category> lstCategory = _dbContext.categories.ToList();

            ViewBag.Authors = lstAuthor;
            ViewBag.Categories = lstCategory;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(Book book)
        {
            book.Id = Guid.NewGuid();
            book.OpeningDate = DateTime.Now;
            book.NumberOfPurchase = 0;
            book.Status = true;

            _dbContext.books.Add(book);
            _dbContext.SaveChanges();
            return RedirectToAction("BooksForAdmin"); ;
        }

        public IActionResult EditBook(Guid id)
        {
            Book b = _dbContext.books.First(x => x.Id == id);
            return View(b);
        }
    }
}
