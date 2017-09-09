using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trabalho_Final_Lydia_Dm106.Models;

namespace Trabalho_Final_Lydia_Dm106.Controllers.Validators
{
    public class ProductsControllerValidator
    {
        private Trabalho_Final_Lydia_Dm106Context db = new Trabalho_Final_Lydia_Dm106Context();

        public bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }

        public bool ModelAlreadyExists(string model)
        {
            return db.Products.Where(p => p.model == model).Any();
        }

        public bool CodeAlreadyExists(string code)
        {
            return db.Products.Where(p => p.code == code).Any();
        }

        public bool IsModelInvalid(int Id, string model)
        {
            var productWithSameModel = db.Products.Where(p => p.model == model);

            if (!productWithSameModel.Any())
            {
                return false;
            }

            return productWithSameModel.First().Id != Id;
        }

        public bool IsCodeInvalid(int Id, string code)
        {
            var productWithSameCode = db.Products.Where(p => p.code == code);

            if (!productWithSameCode.Any())
            {
                return false;
            }

            return productWithSameCode.First().Id != Id;
        }

    }
}