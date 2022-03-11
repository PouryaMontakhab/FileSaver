using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSaver.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;
        private string storagePath = "";

        public HomeController(ILogger<HomeController> logger,IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
            storagePath = $"{_env.ContentRootPath}\\Sotrage";
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Save(string title ,string body)
        {
            if (!Directory.Exists(storagePath))
                Directory.CreateDirectory(storagePath);

            using (FileStream fs = System.IO.File.Create(storagePath + $"\\{title}.txt"))
            {
                var content = new UTF8Encoding(true).GetBytes(body);
                fs.Write(content, 0, content.Length);
            }

            return RedirectToAction("Index");
        }

        [Route("storage/{name}")]
        public IActionResult Storage(string name)
        {
            var filePath = $"{storagePath}\\{name}.txt";
            if (!IsExists(filePath)) return Content("file not found");
            return Content(System.IO.File.ReadAllText(filePath));
        }
        private bool IsExists(string path)
            => System.IO.File.Exists(path);
        //this line has been added in hotfix branch
    }
}
