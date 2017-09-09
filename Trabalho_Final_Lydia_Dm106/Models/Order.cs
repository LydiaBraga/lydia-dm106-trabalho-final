using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_Final_Lydia_Dm106.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderItems = new HashSet<Item>();
        }

        public int Id { get; set; }

        public string userEmail { get; set; }

        public DateTime date { get; set; }

        public DateTime? deliveryDate { get; set; }

        public string status { get; set; }

        public decimal totalPrice {get; set;}

        public decimal totalWeight { get; set; }

        public decimal freightPrice { get; set; }

        public virtual ICollection<Item> OrderItems { get; set; }

    }
}