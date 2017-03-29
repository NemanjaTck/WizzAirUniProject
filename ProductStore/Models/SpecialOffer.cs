namespace WizzAir.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SpecialOffer
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Place { get; set; }
        public int Points { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public decimal AvgPoints { get; set; }
    }
}
