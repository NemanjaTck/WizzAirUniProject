namespace WizzAir.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using WizzAir.Models;

    public class SpecialOffersController : ApiController
    {
        private OrdersContext db = new OrdersContext();

        // Project specialOffers to specialOffer DTOs.
        private IQueryable<SpecialOfferDTO> MapSpecialOffers()
        {
            return from p in db.SpecialOffers
                   select new SpecialOfferDTO() { Id = p.Id, Name = p.Name, Place = p.Place };
        }

        public IEnumerable<SpecialOfferDTO> GetSpecialOffers()
        {
            return MapSpecialOffers().AsEnumerable();
        }

        public SpecialOfferDTO GetSpecialOffer(int id)
        {
            var specialOffer = (from p in MapSpecialOffers()
                           where p.Id == 1
                           select p).FirstOrDefault();
            if (specialOffer == null)
            {
                throw new HttpResponseException(
                    Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return specialOffer;
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}