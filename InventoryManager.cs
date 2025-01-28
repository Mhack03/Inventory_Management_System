﻿namespace Inventory_Management_System
{
    /// <summary>
    /// Manages an inventory of products, providing methods to add, remove, update, and list products.
    /// </summary>
    public class InventoryManager
    {
        public List<Product> Products = [];

        /// <summary>
        /// Adds a new product to the inventory.
        /// With a single parameter of type Product
        /// </summary>
        /// <param name="product">The product to be added.</param>
        public void AddProduct(Product product)
        {
            // Validate the product
            if (product == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid product. Cannot add a null product.");
                Console.ResetColor();
                return;
            }
            // Validate the product ID is positive
            if (product.ProductId <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid product ID. Please enter a valid number.");
                Console.ResetColor();
                return;
            }
            // Check if a product with the same ID already exists
            if (Products.Any(p => p.ProductId == product.ProductId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error: Product with the same ID already exists.");
                Console.ResetColor();
                return;
            }
            // Add the new product
            Products.Add(product);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"New {product.Name} has been added successfully.");
            Console.ResetColor();
        }

        /// <summary>
        /// Removes a product from the inventory based on the given product ID.
        /// </summary>
        /// <param name="productId">The ID of the product to remove.</param>
        public void RemoveProduct(int productId)
        {
            // Validate the product ID
            var product = Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                Products.Remove(product);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Product with ID {productId} has been removed successfully.");
                Console.ResetColor();
            }
            // Product not found
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product with the specified ID not found.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Updates the quantity of an existing product.
        /// </summary>
        /// <param name="productId">The ID of the product to update.</param>
        /// <param name="newQuantity">The new quantity to be set.</param>
        public void UpdateProduct(int productId, int newQuantity)
        {
            // Validate the new quantity value is not negative
            if (newQuantity < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Quantity cannot be negative.");
                Console.ResetColor();
                return;
            }
            // Update the quantity
            var product = Products.FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                product.QuantityInStock = newQuantity;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Product with ID {productId} has been updated successfully.");
                Console.ResetColor();
            }
            // Product not found
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Product not found.");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Lists all products in the inventory.
        /// </summary>
        public void ListProducts()
        {
            // List the products in the inventory or display a message if the inventory is empty
            if (Products.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Inventory is empty.");
                Console.ResetColor();
                return;
            }
            // Display the table header for easy reading
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| Product ID | Name                                               | Quantity In Stock | Price               |");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------");

            // List the products in the inventory 
            foreach (var product in Products)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"| {product.ProductId,-10} | {product.Name,-50} | {product.QuantityInStock,-17} | {product.Price.ToString("N2"),-19} |");
                Console.ResetColor();
                Console.WriteLine("-------------------------------------------------------------------------------------------------------------");
            }
        }

        /// <summary>
        /// Calculates the total value of all products in the inventory.
        /// </summary>
        /// <returns>The total value of all products.</returns>
        public double GetTotalValue()
        {
            double totalValue = 0;
            foreach (var product in Products)
            {
                totalValue += product.QuantityInStock * product.Price;
            }
            return totalValue;
        }

        /// <summary>
        /// Checks if a product with the specified ID exists in the inventory.
        /// </summary>
        /// <param name="id">The product ID to check.</param>
        /// <returns>True if the product exists, otherwise false.</returns>
        public bool InventoryExists(int id)
        {
            return Products.Any(p => p.ProductId == id);
        }
    }
}
