using Repairshop.HelperClasses;
using Repairshop.Models;
using Repairshop.Services;
using Repairshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Odbc;
using System.Deployment.Internal;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Repairshop.Controllers
{
    public class RepairOrdersController : Controller
    {
        DbAccesPoint db;

        public RepairOrdersController(DbAccesPoint db)
        {
            this.db = db;
        }

        [Authorize]
        public ActionResult Index()
        {
            IEnumerable<RepairOrder> model;
            if (User.IsInRole("Admin") || User.IsInRole("Repairguy"))
            {
                model = db.GetRepairOrders();
            }
            else if (User.IsInRole("Customer"))
            {
                model = db.GetRepairOrdersByUserName(User.Identity.Name);
            }
            else
            {
                model = db.GetRepairOrdersByUserName(User.Identity.Name);
            }

            return View(model);
        }

        [Authorize]
        public ActionResult Details(int Id)
        {
            var model = db.GetOrderById(Id);
            return View(model);
        }

        [HttpGet]
        public ActionResult DetailsEdit(int Id)
        {
            if ((User.IsInRole("Admin") || User.IsInRole("Repairguy")))
            {
                DetailsEditViewModel editview = new DetailsEditViewModel
                {
                    order = db.GetOrderById(Id)
                };
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DetailsEdit(DetailsEditViewModel viewmodel, int Id)
        {
            if ((User.IsInRole("Admin") || User.IsInRole("Repairguy")))
            {
                if (viewmodel != null)
                {
                    if (ModelState.IsValid)
                    {
                        bool admin = true;
                        var user = User;
                        bool accessed = false;
                        string switchcase = "Parts";
                        DbAccesPoint idb = db;
                        SaveClass.SaveChoice(viewmodel, accessed, Id, switchcase, idb, user, admin);
                        return RedirectToAction("Index");
                    }
                }
            }
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Customer, Admin")]
        public ActionResult CreateOrder(DetailsEditViewModel viewmodel)
        {

            if (viewmodel != null)
            {
                if (ModelState.IsValid)
                {
                    bool admin = false;
                    if (User.IsInRole("Admin"))
                    {
                        admin = true;
                    }
                    var user = User;
                    bool accessed = true;
                    int Id = 0;
                    string switchcase = "Parts";
                    DbAccesPoint idb = db;
                    SaveClass.SaveChoice(viewmodel, accessed, Id, switchcase, idb, user, admin);
                    return RedirectToAction("Index");
                    
                }
            }
              return View();
        }
    }
}
//using (var context = new ApplicationDbContext())
//{
//    if (ModelState.IsValid)
//    {
//        
//    }
//}