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
        private readonly DbAccesPoint db;

        public RepairOrdersController()
        {
            
        }
        
        public ActionResult Index()
        {
            var model = db.GetRepairOrders();
            return View(model);
        }

        public ActionResult Details(int Id)
        {
            var model = db.GetOrderById(Id);
            return View(model);
        }
    }
}