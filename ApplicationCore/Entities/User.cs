namespace ApplicationCore.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public IEnumerable<Product> Product { get; set; }
    }
}
