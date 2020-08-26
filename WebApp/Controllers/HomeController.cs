using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;
using WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
       private readonly ILogger<HomeController> _logger;
        private readonly WebAppDbContext _context;

        public HomeController(ILogger<HomeController> logger, WebAppDbContext context)
        {
            _logger = logger;
            _context = context;
        }
     
        public async Task<IActionResult> Index()
        {
            var post = _context.Posts.Where(p => p.CategoryId == 1)
             .Include(p => p.Category)
             .Include(p => p.District)
             .AsNoTracking();
            return View(await post.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
