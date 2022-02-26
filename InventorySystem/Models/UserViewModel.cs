using ApplicationCore.Entities;

namespace InventorySystem.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Product Product { get; set; }
    }
}
