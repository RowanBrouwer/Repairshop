using Repairshop.Models;
using Repairshop.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Repairshop.Controllers
{
    public class RepairOrdersController : Controller
    {
        DbAccesPoint db;

        public RepairOrdersController(DbAccesPoint db)
        {
            this.db = db;
        }
        
        public ActionResult Index()
        {
            IEnumerable<RepairOrder> model = db.GetRepairOrders();
            return View(model);
        }

        public ActionResult Details(int Id)
        {
            var model = db.GetOrderById(Id);
            return View(model);
        }
    }
}