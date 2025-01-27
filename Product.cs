namespace Inventory_Management_System
{
    public class Product
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public int QuantityInStock { get; set; }
        public double Price { get; set; }
    }
}
