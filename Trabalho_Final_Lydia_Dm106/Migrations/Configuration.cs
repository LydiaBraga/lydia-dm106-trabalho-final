namespace Trabalho_Final_Lydia_Dm106.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Trabalho_Final_Lydia_Dm106.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Trabalho_Final_Lydia_Dm106.Models.Trabalho_Final_Lydia_Dm106Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Trabalho_Final_Lydia_Dm106.Models.Trabalho_Final_Lydia_Dm106Context context)
        {
            context.Products.AddOrUpdate(
                p => p.Id, new Product
                {
                    Id = 1,
                    name = "produto 1",
                    description = "descrição do produto 1",
                    color = "azul",
                    model = "modelo 1",
                    code = "CODPROD1",
                    price = 15,
                    weight = 1,
                    height = 20,
                    width = 15,
                    length = 15,
                    diameter = 20,
                    url = "http://url.test.com"
                },
                new Product
                {
                    Id = 2,
                    name = "produto 2",
                    description = "descrição do produto 2",
                    color = "rosa",
                    model = "modelo 2",
                    code = "CODPROD2",
                    price = 10,
                    weight = 1,
                    height = 15,
                    width = 30,
                    length = 15,
                    diameter = 20,
                    url = "http://url.test.com"
                },
                new Product
                {
                    Id = 3,
                    name = "produto 3",
                    description = "produto 3 teste",
                    color = "amarelo",
                    model = "modelo 3",
                    code = "CODPROD3",
                    price = 50,
                    weight = 0.50M,
                    height = 10,
                    width = 20,
                    length = 30,
                    diameter = 35,
                    url = "http://url.test.com"
                },
                new Product
                {
                    Id = 4,
                    name = "produto 4",
                    description = "produto 4 teste",
                    color = "lilás",
                    model = "modelo 4",
                    code = "CODPROD4",
                    price = 35,
                    weight = 0.75M,
                    height = 12,
                    width = 15,
                    length = 30,
                    diameter = 30,
                    url = "http://url.test.com"
                },
                new Product
                {
                    Id = 5,
                    name = "produto 5",
                    description = "produto 5 teste",
                    color = "verde",
                    model = "modelo 5",
                    code = "CODPROD5",
                    price = 47,
                    weight = 0.35M,
                    height = 20,
                    width = 20,
                    length = 40,
                    diameter = 45,
                    url = "http://url.test.com"
                }
            );
        }
    }
}
