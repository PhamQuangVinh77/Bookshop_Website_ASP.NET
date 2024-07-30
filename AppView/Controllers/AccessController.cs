using AppData.DBContext;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class AccessController : Controller
    {
        private readonly ILogger<AccessController> _logger;
        BookShopDbContext _dbContext;
        HttpClient http;

        public AccessController(ILogger<AccessController> logger)
        {
            _logger = logger;
            http = new HttpClient();
            _dbContext = new BookShopDbContext();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]

        public IActionResult Login(User user)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                var u = _dbContext.users.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password)).FirstOrDefault();
                if (u != null)
                {
                    HttpContext.Session.SetString("Email", u.Email);
                    var role = _dbContext.userRoles.FirstOrDefault(x => x.RoleId == u.RoleId);
                    HttpContext.Session.SetString("UserId", u.UserId.ToString());
                    HttpContext.Session.SetString("UserName", u.Name);
                    HttpContext.Session.SetString("UserRole", role.RoleName);
                    return RedirectToAction("Index", "Home");
                }

            }
            return BadRequest("Đăng nhập thất bại!");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserRole");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            var lstUsers = _dbContext.users.ToList();
            foreach (var x in lstUsers)
            {
                if (x.Email == user.Email)
                {
                    return BadRequest("Email đã được sử dụng!");
                }
            }

            if (String.IsNullOrEmpty(user.Email) || String.IsNullOrEmpty(user.Password) || String.IsNullOrEmpty(user.Name) || String.IsNullOrEmpty(user.PhoneNumber))
            {
                return View();
            }

            // Add new User
            user.UserId = Guid.NewGuid();
            user.RoleId = _dbContext.userRoles.FirstOrDefault(x => x.RoleName == "Customer").RoleId;
            user.Status = true;
            _dbContext.users.Add(user);

            // Add Cart for new User
            var cart = new Cart();
            cart.UserId = user.UserId;
            _dbContext.carts.Add(cart);
            _dbContext.SaveChanges();

            return RedirectToAction("Login");
        }
    }
}
