using Microsoft.Ajax.Utilities;
using Repairshop.Models;
using Repairshop.Services;
using Repairshop.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Repairshop.HelperClasses
{
    public static class SaveClass
    {
        public static void SaveChoice(dynamic viewmodel, bool accessed, int Id, string switchcase, DbAccesPoint db, dynamic user, bool admin)
        {
            switch (switchcase)
            {
                case "Parts": SavingParts(viewmodel, accessed, Id, db, user);
                        break;

                case "Orders": SavingOrders(viewmodel, accessed, Id, db, user, admin);
                        break;

                default:

                    break;
            }
        }
        public static void SavingParts(PartsEditViewModel viewmodel, bool accessed, int Id, DbAccesPoint db, dynamic user)
        {   
            using (var context = new ApplicationDbContext())
            {
                    var partA = (AmountPartsInStorage)null;
                    var Part = (part)null;
                    if (accessed == true && Id == 0)
                    {
                        partA = new AmountPartsInStorage();
                        Part = new part();
                    }
                    else
                    {
                        partA = db.getAmountById(Id);
                        Part = db.GetPartInfoByAmountId(Id);
                    }

                    partA.AmountInStorage = viewmodel.amountparts.AmountInStorage;
                    Part.Brand = viewmodel.amountparts.Part.Brand;
                    Part.Name = viewmodel.amountparts.Part.Name;
                    Part.Type = viewmodel.amountparts.Part.Type;
                    Part.Price = viewmodel.amountparts.Part.Price;
                    context.parts.AddOrUpdate(Part);
                    partA.Part = Part;
                    context.amountParts.AddOrUpdate(partA);
                    context.SaveChanges();           
            }
        }

        public static void SavingOrders(DetailsEditViewModel viewmodel, bool accessed, int Id, DbAccesPoint db, dynamic user, bool admin)
        {
            using (var context = new ApplicationDbContext())
            {
                var Order = (RepairOrder)null;
                var Cus = db.GetUserById(user);
                if (accessed == true && Id == 0)
                {
                    Order = new RepairOrder();
                    Order.customer = Cus;
                }
                else
                {
                    Cus = db.GetUserById(user);
                    Order.customer = Cus;
                }
      
                Order.repairGuy = viewmodel.order.repairGuy;
                Order.status = viewmodel.order.status;
                Order.StartDate = viewmodel.order.StartDate;
                Order.EndDate = Order.StartDate.AddDays(7);
                Order.parts = viewmodel.order.parts;
                Order.Description = viewmodel.order.Description;
                context.repairOrders.Add(Order);
                if (Order.parts != null)
                {
                    var amountchange = context.amountParts.Where(p => p.Id == Order.parts.Id).FirstOrDefault().AmountInStorage;
                    var change = Order.parts.AmountNeeded;
                    
                }
                context.SaveChanges();
            }
        }
    }
}