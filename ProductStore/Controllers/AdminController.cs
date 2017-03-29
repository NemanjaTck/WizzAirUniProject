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
    //[Authorize(Roles = "Administrator")]
    public class AdminController : ApiController
    {
        private OrdersContext db = new OrdersContext();

        // GET api/Admin
        public IEnumerable<SpecialOffer> GetSpecialOffers()
        {
            return db.SpecialOffers.AsEnumerable();
        }

        // GET api/Admin/5
        public SpecialOffer GetSpecialOffer(int id)
        {
            SpecialOffer specialOffer = db.SpecialOffers.Find(id);
            if (specialOffer == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return specialOffer;
        }

        // PUT api/Admin/5
        public HttpResponseMessage PutSpecialOffer(int id, SpecialOffer specialOffer)
        {
            if (ModelState.IsValid && id == specialOffer.Id)
            {
                db.Entry(specialOffer).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Admin
        public HttpResponseMessage PostSpecialOffer(SpecialOffer specialOffer)
        {
            if (ModelState.IsValid)
            {
                db.SpecialOffers.Add(specialOffer);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, specialOffer);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = specialOffer.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Admin/5
        public HttpResponseMessage DeleteSpecialOffer(int id)
        {
            SpecialOffer specialOffer = db.SpecialOffers.Find(id);
            if (specialOffer == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.SpecialOffers.Remove(specialOffer);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, specialOffer);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}