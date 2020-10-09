using Repairshop.Models;
using Repairshop.Services;
using Repairshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Odbc;
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

        [Authorize(Roles = "Repairguy,Admin")]
        public ActionResult Details(int Id)
        {
            var model = db.GetOrderById(Id);
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "Repairguy,Admin")]
        public ActionResult DetailsEdit(int Id)
        {          
            DetailsEditViewModel editview = new DetailsEditViewModel
            {
                order = db.GetOrderById(Id)
            };
            return View(editview);
        }

        [HttpPost]
        [Authorize(Roles = "Repairguy, Admin")]
        public ActionResult DetailsEdit(DetailsEditViewModel editview)
        {
            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    var order = db.GetOrderById(editview.order.Id);
                    order.customer = editview.order.customer;
                    order.repairGuy = editview.order.repairGuy;
                    order.StartDate = editview.order.StartDate;
                    order.status = editview.order.status;
                    order.EndDate = editview.order.EndDate;
                    order.Description = editview.order.Description;
                    context.repairOrders.AddOrUpdate(order);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(editview);
        }
        [HttpGet]
        [Authorize]
        public ActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Customer, Admin")]
        public ActionResult CreateOrder(DetailsEditViewModel CreateView)
        {
            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    var order = new RepairOrder();
                    order.customer = CreateView.order.customer;
                    order.repairGuy = CreateView.order.repairGuy;
                    order.status = Status.Awaiting;
                    order.StartDate = DateTime.Now;
                    context.repairOrders.Add(order);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}