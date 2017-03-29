namespace WizzAir.Models
{
    using System.Collections.Generic;

    public class OrderDTO
    {
        public class Detail
        {
            public int SpecialOfferID { get; set; }
            public string SpecialOffer { get; set; }
            public int Place { get; set; }
            public int WinPercent { get; set; }
            

        }
        public IEnumerable<Detail> Details { get; set; }
    }
}