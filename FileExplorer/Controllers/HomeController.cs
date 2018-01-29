using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FileExplorer.Models;
using System.IO;

namespace FileExplorer.Controllers
{
    class Data
    {
        string Path{get;set;}
    }
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public JsonResult getPopulation([FromBody] string path = null)
        {
            DirectoryInfo di = null;
            if (path == null)
            {
                var os = System.Environment.OSVersion.Platform.ToString();
                if (os == "Unix")
                {
                    di = new DirectoryInfo("/Users/");
                }
                else
                {
                    di = new DirectoryInfo("c:\\");
                }
            }
            else
            {
                di = new DirectoryInfo(path);
            }

            // Get a reference to each file in that directory.
            var fiArr = di.GetFiles();
            var dirArr = di.GetDirectories();

            var files = new List<FileModel>();

            foreach (DirectoryInfo dir in dirArr)
            {
                files.Add(new FileModel
                {
                    Name = dir.FullName,
                    Date = dir.CreationTime.ToString("d")
                });
            }

            foreach (FileInfo file in fiArr)
            {
                files.Add(new FileModel
                {
                    Name = file.FullName,
                    Size = file.Length.ToString(),
                    Date = file.CreationTime.ToString("d")
                });
            }

     
            return Json(files);
        }
    }
}
