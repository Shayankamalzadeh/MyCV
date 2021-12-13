using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MyCV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCV.Controllers
{
    public class AdminController : Controller
    {

        private readonly ILogger<AdminController> _logger;
        private readonly IConfiguration configuration;
        private readonly MongoClient Client;

        public AdminController(ILogger<AdminController> logger, IConfiguration configuration)
        {
            _logger = logger;
            this.configuration = configuration;
            Client = new MongoClient(configuration.GetConnectionString("MVConnectionString"));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string uname, string psw)
        {
       
            var database = Client.GetDatabase("bkhovme9upbvyna").GetCollection<User>("User");
            var result = database.Find(x => x.Username == uname && x.Password==psw).FirstOrDefault();
            if (result == null)
            {
                ViewBag.error = "Login failed";
                return View();
            }
             return RedirectToAction("Dashboard");
        }

        public IActionResult Dashboard()
        {
            return View();
        }

    }
}
