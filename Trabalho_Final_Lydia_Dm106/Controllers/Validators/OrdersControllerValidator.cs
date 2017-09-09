using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using Trabalho_Final_Lydia_Dm106.Models;

namespace Trabalho_Final_Lydia_Dm106.Controllers.Validators
{
    public class OrdersControllerValidator
    {
        private Trabalho_Final_Lydia_Dm106Context db = new Trabalho_Final_Lydia_Dm106Context();

        public bool OrderExists(int id)
        {
            return db.Orders.Count(e => e.Id == id) > 0;
        }

        public bool IsUserAuthorized(IPrincipal user, Order order)
        {
            return ((user.Identity.Name.Equals(order.userEmail)) || (user.IsInRole("ADMIN")));
        }

        public bool IsUserIdentityValid(IPrincipal user, string userEmail)
        {
            return ((user.Identity.Name.Equals(userEmail)) || (user.IsInRole("ADMIN")));
        }

        public bool IsFreightCalculated(Order order)
        {
            return order.freightPrice != 0 && order.deliveryDate != null;
        }

    }
}