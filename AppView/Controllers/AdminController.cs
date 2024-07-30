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
        List<Author> lstAuthor;
        List<Category> lstCategory;
        public AdminController(ILogger<AccessController> logger)
        {
            _logger = logger;
            http = new HttpClient();
            _dbContext = new BookShopDbContext();
            lstAuthor = _dbContext.authors.ToList();
            lstCategory = _dbContext.categories.ToList();
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
            ViewBag.ListAuthor = lstAuthor;
            ViewBag.ListCategory = lstCategory;
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
            ViewBag.Authors = lstAuthor;
            ViewBag.Categories = lstCategory;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(Book book)
        {
            if (String.IsNullOrEmpty(book.Name) || String.IsNullOrEmpty(book.ImageLink) || String.IsNullOrEmpty(book.Price.ToString()) || String.IsNullOrEmpty(book.AvailableQuantity.ToString()))
            {
                ViewBag.Authors = lstAuthor;
                ViewBag.Categories = lstCategory;
                return View(book);
            }

            book.Id = Guid.NewGuid();
            book.OpeningDate = DateTime.Now;
            book.NumberOfPurchase = 0;
            book.Status = true;

            _dbContext.books.Add(book);
            _dbContext.SaveChanges();
            return RedirectToAction("BooksForAdmin");
        }

        public IActionResult EditBook(Guid id)
        {
            ViewBag.Authors = lstAuthor;
            ViewBag.Categories = lstCategory;
            Book b = _dbContext.books.FirstOrDefault(x => x.Id == id);
            return View(b);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(Book book)
        {
            if (String.IsNullOrEmpty(book.Name) || String.IsNullOrEmpty(book.ImageLink) || String.IsNullOrEmpty(book.Price.ToString()) || String.IsNullOrEmpty(book.AvailableQuantity.ToString()))
            {
                ViewBag.Authors = lstAuthor;
                ViewBag.Categories = lstCategory;
                return View(book);
            }
            var bookNeedEdited = _dbContext.books.FirstOrDefault(x => x.Id.Equals(book.Id));
            bookNeedEdited.Name = book.Name;
            bookNeedEdited.ImageLink = book.ImageLink;
            bookNeedEdited.Description = book.Description;
            bookNeedEdited.Price = book.Price;
            bookNeedEdited.AvailableQuantity = book.AvailableQuantity;
            bookNeedEdited.AuthorId = book.AuthorId;
            bookNeedEdited.CategoryId = book.CategoryId;
            if (bookNeedEdited.AvailableQuantity <= 0)
            {
                bookNeedEdited.Status = false;
            }
            else
            {
                bookNeedEdited.Status = true;
            }
            _dbContext.books.Update(bookNeedEdited);
            _dbContext.SaveChanges();
            return RedirectToAction("BooksForAdmin");
        }
    }
}
