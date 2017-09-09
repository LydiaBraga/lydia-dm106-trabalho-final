using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Trabalho_Final_Lydia_Dm106.br.com.correios.ws;
using Trabalho_Final_Lydia_Dm106.Models;

namespace Trabalho_Final_Lydia_Dm106.Controllers.Calculators
{
    public class OrdersControllerCalculator
    {
        public cResultado calculateFreightAndDelivery(List<Item> items, string destiny, decimal value)
        {
            CalcPrecoPrazoWS correios = new CalcPrecoPrazoWS();

            string totalWeight = calculateTotalWeight(items).ToString();
            decimal totalLength = calculateTotalLength(items);
            decimal totalHeight = calculateTotalHeight(items);
            decimal totalWidth = calculateTotalWidth(items);
            decimal totalDiameter = calculateTotalDiameter(items);

            return correios.CalcPrecoPrazo("", "", "40010", "01311200", destiny, totalWeight, 1, totalLength, totalHeight, totalWidth, totalDiameter, "N", value, "S");
        }

        private decimal calculateTotalWeight(List<Item> items)
        {
            return items.Sum(item => item.amount * item.Product.weight);
        }

        private decimal calculateTotalLength(List<Item> items)
        {
            // Considerando o comprimento do maior produto
            return items.Max(item => item.Product.length);
        }

        private decimal calculateTotalHeight(List<Item> items)
        {
            // Considerando todos os produtos empilhados
            return items.Sum(item => item.amount * item.Product.height);
        }

        private decimal calculateTotalWidth(List<Item> items)
        {
            // Considerando a largura do maior produto
            return items.Max(item => item.Product.width);
        }

        private decimal calculateTotalDiameter(List<Item> items)
        {
            // Considerando o diâmetro do maior produto
            return items.Max(item => item.Product.diameter);
        }
    }
}