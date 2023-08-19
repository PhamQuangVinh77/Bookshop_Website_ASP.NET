using AppData.DBContext;
using AppData.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

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
            if(HttpContext.Session.GetString("Email") == null)
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
            if(HttpContext.Session.GetString("Email") == null)
            {
                var u = _dbContext.users.Where(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password)).FirstOrDefault();
                if(u != null)
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

        public async Task<IActionResult> Register(User user)
        {
            var lstUsers = _dbContext.users.ToList();
            int count1 = lstUsers.Count;
            foreach(var x in lstUsers)
            {
                if(x.Email == user.Email)
                {
                    return BadRequest("Email đã được sử dụng!");
                }
            }

            user.UserId = Guid.NewGuid();   
            user.Status = true;
            user.RoleId = _dbContext.userRoles.FirstOrDefault(x => x.RoleName == "Customer").RoleId;
            _dbContext.users.Add(user);
            _dbContext.SaveChanges();

            var cart = new Cart();
            _dbContext.carts.Add(cart);
            _dbContext.SaveChanges();

            if(_dbContext.users.ToList().Count > count1)
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }
    }
}
