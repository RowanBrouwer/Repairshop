using Repairshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repairshop.Services
{
    public interface DbAccesPoint
    {
        IEnumerable<RepairOrder> GetRepairOrders();
        IEnumerable<Customer> GetCustomers();
        Customer GetCustomerById(int Id);
        RepairOrder GetOrderById(int Id);
    }

    public class DbCommands : DbAccesPoint
    {
        public ApplicationDbContext db = new ApplicationDbContext();

        public Customer GetCustomerById(int Id)
        {
            return db.customers.FirstOrDefault(r => r.Id == Id);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return db.customers.OrderBy(r => r.user.FirstName);
        }

        public RepairOrder getOrderById(int Id)
        {
            return db.repairOrders.FirstOrDefault(r => r.Id == Id);
        }

        public RepairOrder GetOrderById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RepairOrder> GetRepairOrders()
        {
            return db.repairOrders.OrderBy(r => r.status);
        }
    }
}
