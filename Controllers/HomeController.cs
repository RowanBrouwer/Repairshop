using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Repairshop.Models;
using Repairshop.Services;

namespace Repairshop.Controllers
{
    public class HomeController : Controller
    {
        DbAccesPoint db;

        public HomeController(DbAccesPoint db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit()
        {
            var model = db.GetUserByName(User.Identity.Name);
            return View(model);
        }

        //[HttpPost]
        //public ActionResult Edit()
        //{
        //    if (ModelState.IsValid)
        //    {

        //    }
        //    return View();
        //}
    }
}