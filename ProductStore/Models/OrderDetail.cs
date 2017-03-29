namespace WizzAir.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int WinPercent { get; set; }
        
        public int OrderId { get; set; }
        public int SpecialOfferId { get; set; }

        // Navigation properties
        public SpecialOffer SpecialOffer { get; set; }
        public Order Order { get; set; }
    }
}