using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using StackExchange.Redis;
using WebNotebook.Models;

namespace WebNotebook.Controllers
{
    public class HomeController : Controller
    {
      
        public static Logger logger = LogManager.GetLogger("SimpleDemo");
        public static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("redis.webnotebook.com:6379");

        /// <summary>
        /// 读取mongodb数据数据
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var db = redis.GetDatabase();

            var num = db.StringIncrement("count");

            ViewData["num"] = num;

            return View();
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
