using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Trabalho_Final_Lydia_Dm106.Models
{
    public class Item
    {
        public int Id { get; set; }

        public int amount { get; set; }

        public int productId { get; set; }

        public int orderId { get; set; }

        public virtual Product Product { get; set; }
    }
}