using Autofac.Core.Lifetime;
using Microsoft.AspNet.Identity;
using Repairshop.HelperClasses;
using Repairshop.Models;
using Repairshop.Services;
using Repairshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
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
                return View(editview);
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
                        using (var context = new ApplicationDbContext())
                        {
                            RepairOrder order = new RepairOrder();
                            PartsNeeded needed = new PartsNeeded();

                            needed.PartNeeded = db.GetPartInfoByAmountId(viewmodel.order.parts.PartNeeded.Id);
                            needed.AmountNeeded = viewmodel.order.parts.AmountNeeded;
                            needed.inStorage = (AmountPartsInStorage)context.amountParts.Where(a => a.Part.Id == needed.PartNeeded.Id);
                            context.partsNeeded.AddOrUpdate(needed);

                            order.parts = needed;

                            order.repairGuy = viewmodel.order.repairGuy;
                            order.customer = viewmodel.order.customer;

                            order.status = viewmodel.order.status;
                            order.StartDate = viewmodel.order.StartDate;
                            order.EndDate = order.StartDate.AddDays(7);

                            order.Description = viewmodel.order.Description;

                            context.repairOrders.AddOrUpdate(order);
                            context.SaveChanges();

                            return RedirectToAction("Index");
                        }
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult CreateOrder()
        {
            IEnumerable<RepairGuy> reps = db.GetRepairGuys();
            IEnumerable<AmountPartsInStorage> storage = db.GetAllPartsInStorage();
            IEnumerable<part> parts = db.GetParts();
            ViewBag.reps = reps;
            ViewBag.storage = storage;
            ViewBag.parts = parts;
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(DetailsEditViewModel viewmodel)
        {
            using (var context = new ApplicationDbContext())
            {
                RepairOrder order = new RepairOrder();
                PartsNeeded needed = new PartsNeeded();
                var currentuser = User;
                
                if (context.customers.Where(c => c.user == currentuser) == User)
                {
                    Customer AdminCustomer = new Customer();
                    AdminCustomer.user = context.Users.FirstOrDefault(c => c.UserName == currentuser.Identity.Name);
                    order.repairGuy = db.GetRepairGuyByUser(User.Identity.Name);
                    context.customers.AddOrUpdate(AdminCustomer);
                }

                viewmodel.order.customer = db.GetCustomerByUser(User.Identity.Name);
                viewmodel.order.EndDate = viewmodel.order.StartDate.AddDays(7);

                if (ModelState.IsValid)
                {
                    if (User.IsInRole("Customer"))
                    {
                        

                        needed.PartNeeded = db.GetPartInfoByAmountId(viewmodel.order.parts.PartNeeded.Id);
                        needed.AmountNeeded = viewmodel.order.parts.AmountNeeded;
                        needed.inStorage = (AmountPartsInStorage)context.amountParts.Where(a => a.Part.Id == needed.PartNeeded.Id);
                        context.partsNeeded.AddOrUpdate(needed);

                        order.parts = needed;

                        order.repairGuy = viewmodel.order.repairGuy;

                        
                        order.status = Status.Awaiting;
                        order.StartDate = viewmodel.order.StartDate;
                        order.EndDate = viewmodel.order.EndDate;

                        order.Description = viewmodel.order.Description;

                        context.repairOrders.AddOrUpdate(order);
                        context.SaveChanges();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        needed.PartNeeded = db.GetPartInfoByAmountId(viewmodel.order.parts.PartNeeded.Id);
                        needed.AmountNeeded = viewmodel.order.parts.AmountNeeded;
                        needed.inStorage = (AmountPartsInStorage)context.amountParts.Where(a => a.Part.Id == needed.PartNeeded.Id);
                        context.partsNeeded.AddOrUpdate(needed);

                        order.parts = needed;

                        order.status = Status.Awaiting;
                        order.StartDate = viewmodel.order.StartDate;
                        order.EndDate = viewmodel.order.EndDate;

                        order.Description = viewmodel.order.Description;

                        context.repairOrders.AddOrUpdate(order);
                        context.SaveChanges();

                        return RedirectToAction("Index");
                    }
                }
                return View();
            }
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
        public ActionResult Delete(DeleteViewModel DeleteView, int Id)
        {
            using (var context = new ApplicationDbContext())
            {
                var model = db.GetOrderById(Id);

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
    }
}