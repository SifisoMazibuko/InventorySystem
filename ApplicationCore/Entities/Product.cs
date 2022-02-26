
using System.ComponentModel.DataAnnotations.Schema;


namespace ApplicationCore.Entities
{
    public class Product : BaseEntity
    {
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string BatchNumber { get; set; }
        public string Supplier { get; set; }
        public double CostPrice { get; set; }
        public User User { get; set; }

    }
}
