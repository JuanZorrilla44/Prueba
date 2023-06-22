namespace Core.Entity
{
    public class ProductEntity
    {
        public int ProductId { get; set; }
        public string? NameProduct { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public string? NameCategory { get; set; }
        public int CategoryID { get; set; }
        public bool StatusProduct { get; set; }
    }
}
