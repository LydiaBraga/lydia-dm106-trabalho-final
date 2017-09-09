using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Trabalho_Final_Lydia_Dm106.RestClient
{
    public class CrmRestClient
    {
        private HttpClient client;

        public CrmRestClient()
        {
            client = new HttpClient();

            client.BaseAddress = new Uri("http://siecolacrm.azurewebsites.net/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            byte[] str1Byte = System.Text.Encoding.UTF8.GetBytes(string.Format("{0}:{1}", "crmwebapi", "crmwebapi"));
            String plaintext = Convert.ToBase64String(str1Byte);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", plaintext);
        }

        public Customer GetCustomerByEmail(string email)
        {
            HttpResponseMessage response = client.GetAsync("customers/byemail?email=" + email).Result;

            if (response.IsSuccessStatusCode)
            {
                Customer customer = (Customer)response.Content.ReadAsAsync<Customer>().Result;
                return customer;
            }

            return null;
        }
    }
}
