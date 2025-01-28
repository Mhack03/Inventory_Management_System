using Inventory_Management_System;

namespace InventoryManagementSystem
{
    /// <summary>
    /// Represents a manager for inventory products.
    /// </summary>
    public class InventoryManager
    {
        /// <summary>
        /// The list of products in the inventory.
        /// </summary>
        public List<Product> Product = new List<Product>();

        /// <summary>
        /// Adds a new product to the inventory.
        /// </summary>
        /// <param name="product">The product to add.</param>
        /// <exception cref="ArgumentNullException">If the product is null.</exception>
        /// <exception cref="ArgumentException">If the product ID is not a positive integer.</exception>
        /// <exception cref="InvalidOperationException">If a product with the same ID already exists.</exception>
        public void AddProduct(Product product)
        {
            // Check if the product is null
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null");
            }

            // Check if the product ID is valid
            if (product.ProductId <= 0)
            {
                throw new ArgumentException("Product ID must be a positive integer", nameof(product.ProductId));
            }

            // Check if a product with the same ID already exists
            if (Product.Any(p => p.ProductId == product.ProductId))
            {
                throw new InvalidOperationException("Product with the same ID already exists");
            }

            // Add the product to the inventory
            Product.Add(product);
        }

        /// <summary>
        /// Removes a product from the inventory by its ID.
        /// </summary>
        /// <param name="productId">The ID of the product to remove.</param>
        /// <exception cref="InvalidOperationException">If the product is not found.</exception>
        public void RemoveProduct(int productId)
        {
            // Find the product to remove
            var product = Product.FirstOrDefault(p => p.ProductId == productId);

            // Check if the product exists
            if (product != null)
            {
                // Remove the product from the inventory
                Product.Remove(product);
            }
            else
            {
                // Throw an exception if the product is not found
                throw new InvalidOperationException("Product not found");
            }
        }
        /// <summary>
        /// Updates the quantity of a product in the inventory.
        /// </summary>
        /// <param name="productId">The ID of the product to update.</param>
        /// <param name="newQuantity">The new quantity for the product.</param>
        /// <exception cref="ArgumentException">If the new quantity is less than zero.</exception>
        /// <exception cref="InvalidOperationException">If the product with the given ID is not found.</exception>
        public void UpdateProduct(int productId, int newQuantity)
        {
            // Validate the new quantity to ensure it's not negative
            if (newQuantity < 0)
            {
                throw new ArgumentException("Quantity cannot be negative", nameof(newQuantity));
            }

            // Find the product to update by its ID
            var product = Product.FirstOrDefault(p => p.ProductId == productId);

            // Check if the product exists in the inventory
            if (product is null)
            {
                throw new InvalidOperationException("Product not found");
            }

            // Update the quantity of the product
            product.QuantityInStock = newQuantity;
        }

        /// <summary>
        /// Lists all products in the inventory.
        /// </summary>
        public void ListProducts()
        {
            // Check if the inventory is empty
            if (Product.Count == 0)
            {
                Console.WriteLine("Inventory is empty");
                return;
            }

            // Print the product list header
            Console.WriteLine(new string('-', 109));
            Console.WriteLine("| Product ID | Name                                               | Quantity In Stock | Price               |");
            Console.WriteLine(new string('-', 109));

            // Print each product in the inventory
            foreach (var product in Product)
            {
                Console.WriteLine($"| {product.ProductId,-10} | {product.Name,-50} | {product.QuantityInStock,-17} | {product.Price.ToString("N2"),-19} |");
                Console.WriteLine(new string('-', 109));
            }
        }

        /// <summary>
        /// Gets the total value of all products in the inventory.
        /// </summary>
        /// <returns>The total value of all products.</returns>
        public double GetTotalValue()
        {
            // Calculate the total value of all products
            return Product.Sum(p => p.QuantityInStock * p.Price);
        }

        /// <summary>
        /// Checks if a product with the specified ID exists in the inventory.
        /// </summary>
        /// <param name="id">The ID of the product to check.</param>
        /// <returns>True if the product exists, false otherwise.</returns>
        public bool ProductExists(int id) => Product.Any(p => p.ProductId == id);
    }
}