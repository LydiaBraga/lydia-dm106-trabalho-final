using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Trabalho_Final_Lydia_Dm106.Models
{
    public class Trabalho_Final_Lydia_Dm106Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Trabalho_Final_Lydia_Dm106Context() : base("name=Trabalho_Final_Lydia_Dm106Context")
        {
        }

        public System.Data.Entity.DbSet<Trabalho_Final_Lydia_Dm106.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<Trabalho_Final_Lydia_Dm106.Models.Order> Orders { get; set; }
    }
}
