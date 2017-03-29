using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WizzAir.Models;

namespace WizzAir.Controllers
{
    //[Authorize]
    public class OrdersController : ApiController
    {
        private OrdersContext db = new OrdersContext();

        // GET api/Orders
        public IEnumerable<Order> GetOrders()
        {
            return db.Orders.Where(o => o.User == User.Identity.Name);
        }

        // GET api/Orders/5
        public OrderDTO GetOrder(int id)
        {
            Order order = db.Orders.Include("OrderDetails.SpecialOffer")
                .First(o => o.Id == id && o.User == User.Identity.Name);
            if (order == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return new OrderDTO()
            {
                Details = from d in order.OrderDetails
                          select new OrderDTO.Detail()
                          {
                              SpecialOfferID = d.SpecialOffer.Id,
                              SpecialOffer = d.SpecialOffer.Name,
                              Place = d.SpecialOffer.Place,
                              WinPercent = d.WinPercent
                              
                          }
            };
        }

        // POST api/Orders
        public HttpResponseMessage PostOrder(OrderDTO dto)
        {
            if (ModelState.IsValid)
            {
                var order = new Order()
                {
                    User = User.Identity.Name,
                    OrderDetails = (from item in dto.Details
                                    select new OrderDetail() { SpecialOfferId = item.SpecialOfferID, WinPercent = item.WinPercent }).ToList()
                };

                db.Orders.Add(order);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, order);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = order.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}