using BusinessObjects.EmailSendModule;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces.EmailSendModule
{
    public interface IEmailSendDataService
    {
         List<Customer> ListCustomers();

         List<Order> ListOrders();
    }
}
