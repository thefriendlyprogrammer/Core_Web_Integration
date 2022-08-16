using CoreAPIIntigration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace CoreAPIIntigration.Controllers
{
    public class HomeController : Controller
    {
        Uri ApiUrl = new Uri("http://localhost:2473/api/Home/");

        HttpClient Cliet;

        public HomeController()
        {
            Cliet = new HttpClient();
            Cliet.BaseAddress = ApiUrl;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(SoftwareEnginnermdl mdlobj)
        {
            String objData = JsonConvert.SerializeObject(mdlobj);

            StringContent content = new StringContent(objData, Encoding.UTF8, "application/json");

            HttpResponseMessage hrm = Cliet.PostAsync(Cliet.BaseAddress + "Post/Create", content).Result;

            if (hrm.IsSuccessStatusCode)
            {
                return RedirectToAction("Read");
            }

            return View();
        }
        public ActionResult Read()
        {
            List<SoftwareEnginnermdl> mdlobj = new List<SoftwareEnginnermdl>();

            HttpResponseMessage hrm = Cliet.GetAsync(Cliet.BaseAddress + "Get/Read").Result;

            if (hrm.IsSuccessStatusCode)
            {
                String JsonData = hrm.Content.ReadAsStringAsync().Result;

                mdlobj = JsonConvert.DeserializeObject<List<SoftwareEnginnermdl>>(JsonData);
            }

            return View(mdlobj);
        }

        [HttpGet]
        public ActionResult Update(int ID)
        {
            SoftwareEnginnermdl mdlobj = new SoftwareEnginnermdl();

            HttpResponseMessage hrm = Cliet.GetAsync(Cliet.BaseAddress + "Get/Update" + "?ID=" + ID.ToString()).Result;

            if (hrm.IsSuccessStatusCode)
            {
                String JsonData = hrm.Content.ReadAsStringAsync().Result;

                mdlobj = JsonConvert.DeserializeObject<SoftwareEnginnermdl>(JsonData);
            }

            return View("Create", mdlobj);
        }

        [HttpGet]
        public ActionResult Delete(int ID)
        {
            HttpResponseMessage hrm = Cliet.GetAsync(Cliet.BaseAddress + "Get/Delete" + "?ID=" + ID.ToString()).Result;

            if (hrm.IsSuccessStatusCode)
            {
                return RedirectToAction("Read");
            }
            return View("Index");
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
