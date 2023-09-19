using AppData.DBContext;
using AppData.Models;
using AppView.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Diagnostics;
using System.Reflection.Metadata;
using static System.Net.WebRequestMethods;

namespace AppView.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var api = "https://localhost:7287/api/Book";
            var res = await _httpClient.GetFromJsonAsync<List<Book>>(api);

            var lstBanChay = res.Where(x => x.Status == true).ToList();
            lstBanChay.Sort((a,b) => b.NumberOfPurchase.CompareTo(a.NumberOfPurchase));
            ViewBag.BanChay = lstBanChay;

            var lstSanPhamMoi = res.Where(x => x.Status == true).ToList();
            lstSanPhamMoi.Sort((a,b) => b.OpeningDate.CompareTo(a.OpeningDate));
            ViewBag.SpMoi = lstSanPhamMoi;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ShowAllBooks()
        {
            var api = "https://localhost:7287/api/Book";
            var res = await _httpClient.GetFromJsonAsync<List<Book>>(api);
            return View(res);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var api = "https://localhost:7287/api/Book/" + id.ToString();
            var res = await _httpClient.GetFromJsonAsync<Book>(api);
            return View(res);
        }

        public async Task<IActionResult> Search(string name)
        {
            var api = "https://localhost:7287/api/Book";
            var res = await _httpClient.GetFromJsonAsync<List<Book>>(api);
            var lstBooks = res.Where(x => x.Name.ToLower().StartsWith(name.ToLower())).ToList();
            return View(lstBooks);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}