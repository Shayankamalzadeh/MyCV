using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
//using MongoDB.Driver;
using MyCV.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace MyCV.Controllers
{
    public class AdminController : Controller
    {

        private readonly ILogger<AdminController> _logger;
        private readonly IConfiguration configuration;
        //private readonly MongoClient Client;
        private IHostingEnvironment Environment;

        public AdminController(ILogger<AdminController> logger, IConfiguration configuration, IHostingEnvironment _environment)
        {
            _logger = logger;
            this.configuration = configuration;
            Environment = _environment;
            // Client = new MongoClient(configuration.GetConnectionString("MVConnectionString"));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string uname, string psw)
        {

            //var database = Client.GetDatabase("bkhovme9upbvyna").GetCollection<User>("User");
            //var result = database.Find(x => x.Username == uname && x.Password==psw).FirstOrDefault();
            //if (result == null)
            //{
            //    ViewBag.error = "Login failed";
            //    return View();
            //}
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

        public IActionResult Ip()
        {
            var guid = Guid.NewGuid();
            var z = (JObject)JToken.FromObject(HttpContext.Request.Headers);
            var fileName = guid.ToString() + "Headers " + ".json";
            CreateFile(z, fileName);


            string json = JsonConvert.SerializeObject(HttpContext.Request.Cookies, Newtonsoft.Json.Formatting.Indented);
            fileName = guid.ToString() + "Cookies " + ".json";
            CreateFile(json, fileName);


            return View();


        }
        private void CreateFile(string obj, string name)

        {
            string uploadDir = Path.Combine(Environment.WebRootPath, "myfile");
            string downloadDir = Path.Combine(Environment.WebRootPath, "Json");
            string[] files = System.IO.Directory.GetFiles(uploadDir);

            // Copy the files and overwrite destination files if they already exist.
            foreach (string s in files)
            {
                // Use static Path methods to extract only the file name from the path.
               var fileName = System.IO.Path.GetFileName(s);
               var destFile = System.IO.Path.Combine(downloadDir, name);
                System.IO.File.Copy(s, destFile, true);
            }

           
        }
        private void CreateFile(JObject obj, string name)

        {

            string uploadDir = Path.Combine(Environment.WebRootPath, "Json");

            string filePath = Path.Combine(uploadDir, name);

            System.IO.File.WriteAllText(filePath, obj.ToString());
        }
        [HttpPost]
        public IActionResult XML(string surname, string name, string birthyear, string years)
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

        static void IP(string[] args, TextWriter writer)
        {
            String StringHost;
            if (args.Length == 0)
            {
                // Getting IP of local machine...
                // First get the host name of the local machine...
                StringHost = System.Net.Dns.GetHostName();
                writer.WriteLine("Local machine host name is: " + StringHost);
                writer.WriteLine("");

            }
            else
            {
                StringHost = args[0];
            }

            // The using hostname, get the IP address List...
            IPHostEntry ipEntry =
                System.Net.Dns.GetHostEntry(StringHost);

            IPAddress[] address = ipEntry.AddressList;

            for (int i = 0; i < address.Length; i++)
            {
                writer.WriteLine("");
                writer.WriteLine("IP Address Type {0}: {1} ", i, address[i].ToString());
            }
        }

    }
}
