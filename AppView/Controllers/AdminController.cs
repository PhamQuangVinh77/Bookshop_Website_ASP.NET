using AppData.DBContext;
using Microsoft.AspNetCore.Mvc;

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
    }
}
