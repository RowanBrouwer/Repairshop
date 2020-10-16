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
                return View(editview);
            }
            return RedirectToAction("Details");
        }

        [HttpPost]
        public ActionResult DetailsEdit(DetailsEditViewModel editview)
        {
            if ((User.IsInRole("Admin") || User.IsInRole("Repairguy")))
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
                    return View(editview);
                }
            }
            return RedirectToAction("Details");
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
                    order.EndDate = order.StartDate.AddDays(7);
                    order.parts = CreateView.order.parts;
                    order.Description = CreateView.order.Description;
                    context.repairOrders.Add(order);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
         
        [HttpGet]
        [Authorize]
        public ActionResult Delete(int Id)
        {
            DeleteViewModel deleteViewModel = new DeleteViewModel
            {
                order = db.GetOrderById(Id)
            };
            return View(deleteViewModel);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(DeleteViewModel DeleteView)
        {
            using (var context = new ApplicationDbContext())
            {
                if (ModelState.IsValid)
                {
                    var model = db.GetOrderById(DeleteView.order.Id);
                    model.customer = DeleteView.order.customer;
                    model.repairGuy = DeleteView.order.repairGuy;
                    model.StartDate = DeleteView.order.StartDate;
                    model.status = DeleteView.order.status;
                    model.EndDate = DeleteView.order.EndDate;
                    model.Description = DeleteView.order.Description;
                    context.repairOrders.Remove(model);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}