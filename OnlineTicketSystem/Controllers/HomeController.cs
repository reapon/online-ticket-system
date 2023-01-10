using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineTicketSystem.Data;
using OnlineTicketSystem.Models;
using OnlineTicketSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineTicketSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this.context = context;
            this.userManager = userManager;
        }

        public  IActionResult Index()
        {
      
            ViewData["CategoryID"] = new SelectList(context.Categories, "CategoryName", "CategoryName");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Confirm()
        {
            return View();
        }

        //public IActionResult Test()
        //{
        //    var group = context.Tickets.Include(x => x.Group).Select(x=> new { x.Group.GroupName, x.GroupID}).Distinct().ToList();
        //    var list = new List<GroupTicketCount>();

        //    foreach (var item in group)
        //    {
        //        var x = context.Tickets.Where(x => x.GroupID == item.GroupID).Count();

        //        list.Add(new GroupTicketCount { GroupName = item.GroupName, TicketCount= x });

        //    }
        //    return View(list);
        //}

        //[HttpPost]
        //public JsonResult CategoryTest(string[] category)
        //{
        //    string[] cat = category;
        //    foreach(var c in cat)
        //    {

        //        if (!context.Categories.Any(o => o.CategoryName==c))
        //        {
        //            var cate = new Category()
        //            {
        //                CategoryName = c
        //            };

        //            context.Add(cate);
        //            context.SaveChanges();
        //        }

        //    }


        //    return Json(new { res = true });


        //}




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
