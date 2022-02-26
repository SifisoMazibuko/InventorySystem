using ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventorySystem.Models
{
    public class ProductViewModel
    {
        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string BatchNumber { get; set; }
        public string Supplier { get; set; }
        public double CostPrice { get; set; }
        public List<User> Users { get; set; }
    }
}
