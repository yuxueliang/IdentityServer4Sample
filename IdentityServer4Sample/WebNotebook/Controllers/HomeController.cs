using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using WebNotebook.Models;

namespace WebNotebook.Controllers
{
    public class HomeController : Controller
    {
        public static Logger logger = LogManager.GetLogger("SimpleDemo");

        public IActionResult Index()
        {
            var log = $"客户端：{HttpContext.Connection.RemoteIpAddress} 访问了 Index 页面！";

            logger.Info(log);
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
