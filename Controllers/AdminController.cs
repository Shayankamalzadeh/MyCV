using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MyCV.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

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

        public IActionResult Layout()
        {
            return View();
        }

        public IActionResult XML()
        {
            return View();
        }
        [HttpPost]
        public IActionResult XML(string surname, string name, string birthyear,string years)
        {

            MemoryStream ms = new MemoryStream();
            XmlWriterSettings xws = new XmlWriterSettings();
            xws.OmitXmlDeclaration = true;
            xws.Indent = true;

            using (XmlWriter xw = XmlWriter.Create(ms, xws))
            {
                XDocument doc = new XDocument(
                 new XElement("Tidrans",
                  new XElement("surname", surname),
                  new XElement("name", name),
                  new XElement("birthyear", birthyear),
                   new XElement("years", years)
                 )
                );
                doc.WriteTo(xw);
            }
            ms.Position = 0;
            return File(ms, "text/xml", "Sample.xml");
            
        }

    }
}
