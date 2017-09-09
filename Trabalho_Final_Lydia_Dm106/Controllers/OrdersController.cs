using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http;
using System.Web.Http.Description;
using Trabalho_Final_Lydia_Dm106.br.com.correios.ws;
using Trabalho_Final_Lydia_Dm106.Controllers.Calculators;
using Trabalho_Final_Lydia_Dm106.Controllers.Validators;
using Trabalho_Final_Lydia_Dm106.Models;
using Trabalho_Final_Lydia_Dm106.RestClient;

namespace Trabalho_Final_Lydia_Dm106.Controllers
{
    [Authorize]
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private Trabalho_Final_Lydia_Dm106Context db = new Trabalho_Final_Lydia_Dm106Context();
        private OrdersControllerValidator ordersControllerValidator = new OrdersControllerValidator();
        private OrdersControllerCalculator ordersControllerCalculator = new OrdersControllerCalculator();

        // GET: api/Orders
        [Authorize(Roles ="ADMIN")]
        public List<Order> GetOrders()
        {
            return db.Orders.Include(order => order.OrderItems).ToList();
        }

        [HttpGet]
        public IHttpActionResult GetOrders(string userEmail)
        {
            if (!ordersControllerValidator.IsUserIdentityValid(User, userEmail))
            {
                return Unauthorized();
            }

            return Ok(db.Orders.Where(o => o.userEmail == userEmail).Include(order => order.OrderItems).ToList());
        }

        // GET: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult GetOrder(int id)
        {
            Order order = db.Orders.Find(id);

            if (order == null)
            {
                return NotFound();
            }

            if (!ordersControllerValidator.IsUserAuthorized(User, order))
            {
                return Unauthorized();
            }

            return Ok(order);
        }

        // PUT: api/Orders/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("close")]
        public IHttpActionResult closeOrder(int id)
        {
            if (!ordersControllerValidator.OrderExists(id))
            {
                return NotFound();
            }

            Order order = db.Orders.Find(id);

            if (!ordersControllerValidator.IsUserAuthorized(User, order))
            {
                return Unauthorized();
            }

            if (!ordersControllerValidator.IsFreightCalculated(order))
            {
                string errorMessage = "Calcule o frete antes de fechar o pedido!";

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage));
            }

            order.status = "fechado";
            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ordersControllerValidator.OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("freight")]
        public IHttpActionResult calculateFreight(int id)
        {
            if (!ordersControllerValidator.OrderExists(id))
            {
                return NotFound();
            }

            Order order = db.Orders.Find(id);

            if (!ordersControllerValidator.IsUserAuthorized(User, order))
            {
                return Unauthorized();
            }

            if (!order.OrderItems.Any())
            {
                string errorMessage = "Pedido sem items!";

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage));
            }

            if (order.status != "novo")
            {
                string errorMessage = "Status do pedido é diferente de novo!";

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage));
            }

            CrmRestClient crmRestClient = new CrmRestClient();
            Customer customer = crmRestClient.GetCustomerByEmail(order.userEmail);

            if (customer == null)
            {
                string errorMessage = "Erro ao acessar serviço de CRM!";

                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, errorMessage));
            }

            decimal totalFreight = 0;
            int daysToDelivery = 0;
           
            cResultado result = ordersControllerCalculator.calculateFreightAndDelivery(order.OrderItems.ToList(), customer.zip, 0);

            if (result.Servicos[0].Erro.Equals("0"))
            {
                totalFreight = decimal.Parse(result.Servicos[0].Valor); 
                daysToDelivery = int.Parse(result.Servicos[0].PrazoEntrega);
            }
            else
            {
                return BadRequest("Erro ao acessar serviços do correio: " + result.Servicos[0].MsgErro);
            }
            
            order.freightPrice = totalFreight;
            order.deliveryDate = DateTime.Now.AddDays(daysToDelivery);
            db.Entry(order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ordersControllerValidator.OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(order);
        }

        // POST: api/Orders
        [ResponseType(typeof(Order))]
        public IHttpActionResult PostOrder(Order order)
        {
            if (order == null)
            {
                return BadRequest("Pedido incompleto!");
            }

            order.status = "novo";
            order.userEmail = User.Identity.Name;
            order.date = DateTime.Now;

            db.Orders.Add(order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [ResponseType(typeof(Order))]
        public IHttpActionResult DeleteOrder(int id)
        {
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }

            if (!ordersControllerValidator.IsUserAuthorized(User, order))
            {
                return Unauthorized();
            }

            db.Orders.Remove(order);
            db.SaveChanges();

            return Ok(order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}