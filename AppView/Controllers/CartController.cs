using AppData.DBContext;
using AppData.Models;
using AppView.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        BookShopDbContext _dbContext;
        HttpClient _httpClient;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            _dbContext = new BookShopDbContext();
        }

        public IActionResult AddToCart(Guid id)
        {
            var book = _dbContext.books.ToList().Find(x => x.Id == id);
            var cartDetail = new CartDetail();
            cartDetail.Id = Guid.NewGuid();
            cartDetail.BookId = id;
            cartDetail.UserId = _dbContext.users.ToList().FirstOrDefault(x => x.Name == "Khách lẻ").UserId;
            cartDetail.Quantity = 1;

            if (HttpContext.Session.GetString("UserName") == null)
            {
                var carts = SessionServices.GetObjFromSession(HttpContext.Session, "Cart");
                if (carts.Count == 0)
                {
                    carts.Add(cartDetail); // Thêm trực tiếp sp vào nếu List trống
                    SessionServices.SetObjToSession(HttpContext.Session, "Cart", carts);
                }
                else
                {
                    if (SessionServices.CheckObjInList(id, carts))
                    { // Kiểm tra xem list lấy ra có chứa sản phẩm mình chọn hay chưa?
                        var cd = carts.FirstOrDefault(x => x.BookId == id);
                        if (cd.Quantity >= book.AvailableQuantity)
                        {
                            cd.Quantity = book.AvailableQuantity;
                            SessionServices.SetObjToSession(HttpContext.Session, "Cart", carts);
                        }
                        else
                        {
                            cd.Quantity++;
                            SessionServices.SetObjToSession(HttpContext.Session, "Cart", carts);
                        }
                    }
                    else
                    {
                        carts.Add(cartDetail); // Thêm trực tiếp sp vào nếu List chưa chứa sp đó
                        SessionServices.SetObjToSession(HttpContext.Session, "Cart", carts);
                    }
                }
            }
            else
            {
                var lstCartDetails = _dbContext.cartsDetails.Where(x => x.UserId.ToString() == HttpContext.Session.GetString("UserId")).ToList();
                if(lstCartDetails.Count == 0) 
                {
                    // Nếu giỏ hàng rỗng thì tạo giỏ mới và thêm trực tiếp vào giỏ
                    cartDetail.UserId = Guid.Parse(HttpContext.Session.GetString("UserId"));
                    _dbContext.cartsDetails.Add(cartDetail);
                    _dbContext.SaveChanges();
                }
                else 
                {
                    var cd = lstCartDetails.FirstOrDefault(x => x.BookId == id);
                    if (cd == null)
                    {// Nếu giỏ hàng đã có hàng nhưng sp chưa có trong đó thì cũng thêm trực tiếp sp
                        cartDetail.UserId = Guid.Parse(HttpContext.Session.GetString("UserId"));
                        _dbContext.cartsDetails.Add(cartDetail);
                        _dbContext.SaveChanges();
                    }
                    else
                    {
                        if(cd.Quantity >= book.AvailableQuantity)
                        {
                            cd.Quantity = book.AvailableQuantity;
                            _dbContext.SaveChanges();
                            // Nếu sl mua đã bằng với sl tồn trong kho thì sẽ k mua thêm dc nữa
                        }
                        else
                        {
                            cd.Quantity++;
                            _dbContext.SaveChanges();
                        }
                    }
                }
            }
            return RedirectToAction("ShowCart");
        }

        public IActionResult ShowCart()
        {
            ViewBag.ListBooks = _dbContext.books.ToList();
            List<CartDetail> cartDetails;
            if (HttpContext.Session.GetString("UserName") == null)
            {
                cartDetails = SessionServices.GetObjFromSession(HttpContext.Session, "Cart");
            }
            else
            {
                cartDetails = _dbContext.cartsDetails.Where(x => x.UserId == Guid.Parse(HttpContext.Session.GetString("UserId"))).ToList();
            }
            return View(cartDetails);
        }

        public IActionResult Add(Guid id, Guid idBook)
        {
            var book = _dbContext.books.ToList().Find(x => x.Id == idBook);
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var carts = SessionServices.GetObjFromSession(HttpContext.Session, "Cart");
                var cd = carts.Find(x => x.Id == id);
                if (cd.Quantity < book.AvailableQuantity)
                {
                    cd.Quantity++;
                }
                SessionServices.SetObjToSession(HttpContext.Session, "Cart", carts);
            }
            else
            {
                var cd = _dbContext.cartsDetails.FirstOrDefault(x => x.Id == id);
                if (cd.Quantity < book.AvailableQuantity)
                {
                    cd.Quantity++;
                }
                _dbContext.SaveChanges();

            }
            return RedirectToAction("ShowCart");
        }

        public IActionResult Subtract(Guid id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var carts = SessionServices.GetObjFromSession(HttpContext.Session, "Cart");
                var cd = carts.Find(x => x.Id == id);
                if (cd.Quantity > 1)
                {
                    cd.Quantity--;
                }
                SessionServices.SetObjToSession(HttpContext.Session, "Cart", carts);
            }
            else
            {
                var cd = _dbContext.cartsDetails.FirstOrDefault(x => x.Id == id);
                if (cd.Quantity > 1)
                {
                    cd.Quantity--;
                }
                _dbContext.SaveChanges();
            }
            return RedirectToAction("ShowCart");
        }

        public IActionResult Delete(Guid id)
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {
                var carts = SessionServices.GetObjFromSession(HttpContext.Session, "Cart");
                var cd = carts.Find(x => x.Id == id);
                carts.Remove(cd);
                SessionServices.SetObjToSession(HttpContext.Session, "Cart", carts);
            }
            else
            {
                CartDetail cartDetail = _dbContext.cartsDetails.FirstOrDefault(x => x.Id == id);
                _dbContext.cartsDetails.Remove(cartDetail);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("ShowCart");
        }
    }
}
