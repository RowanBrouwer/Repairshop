using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        public ActionResult Index(/*int Id*/)
        {
            /*var model = db.GetCustomerById(Id);
            if (model == null)
            {
                return HttpNotFound();
            }
            */
            return View(/*model*/);
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
        public ActionResult Edit(/*int Id*/)
        {
            /*var model = db.GetCustomerById(Id);
            if (model == null)
            {
                return HttpNotFound();
            }
            */
            return View(/*model*/);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}