using Inventory_Management_System;
using System.Text;

namespace InventoryManagementSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var inventoryManager = new InventoryManager();

            // Main loop for the inventory management system
            while (true)
            {
                try
                {
                    // Display the main menu options
                    Console.WriteLine("\nInventory Management System");
                    Console.WriteLine("1. Add Product");
                    Console.WriteLine("2. Remove Product");
                    Console.WriteLine("3. Update Product");
                    Console.WriteLine("4. List Products");
                    Console.WriteLine("5. Get Total Value");
                    Console.WriteLine("6. Exit");

                    // Get the user's menu option
                    if (int.TryParse(Console.ReadLine(), out int option) && option >= 1 && option <= 6)
                    {
                        // Handle the user's menu option
                        switch (option)
                        {
                            case 1:
                                // Add a new product to the inventory with an automatically generated ID
                                AddProductWithAutoId(inventoryManager);
                                break;
                            case 2:
                                // Remove a product from the inventory
                                RemoveProduct(inventoryManager);
                                break;
                            case 3:
                                // Update a product in the inventory
                                UpdateProduct(inventoryManager);
                                break;
                            case 4:
                                // List all products in the inventory
                                inventoryManager.ListProducts();
                                break;
                            case 5:
                                // Display the total value of all products in the inventory
                                Console.WriteLine($"Total Value: {inventoryManager.GetTotalValue():C2}");
                                break;
                            case 6:
                                // Exit the program
                                return;
                        }
                    }
                    else
                    {
                        // Display an error message if the user enters an invalid option
                        Console.WriteLine("Invalid option. Please choose from 1 to 6.");
                    }
                }
                catch (Exception ex)
                {
                    // Catch and display any unexpected errors
                    Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Adds a new product to the inventory with an automatically generated ID.
        /// </summary>
        /// <param name="inventoryManager">The inventory manager instance.</param>
        private static void AddProductWithAutoId(InventoryManager inventoryManager)
        {
            // Get the next available product ID
            var nextProductId = GetNextProductId(inventoryManager);

            // Initialize product name variable
            string productName;

            // Loop until a valid product name is entered
            while (true)
            {
                Console.Write("Enter Product Name: ");
                productName = Console.ReadLine().Trim();

                // Check if the product name is not empty
                if (!string.IsNullOrWhiteSpace(productName))
                {
                    break;
                }

                Console.WriteLine("Error: Product name cannot be empty.");
            }

            // Initialize quantity variable
            int quantity;

            // Loop until a valid quantity is entered
            while (true)
            {
                Console.Write("Enter Quantity in Stock: ");
                if (int.TryParse(Console.ReadLine(), out quantity) && quantity >= 0)
                {
                    break;
                }

                Console.WriteLine("Error: Quantity must be a non-negative integer.");
            }

            // Initialize price variable
            double price;

            // Loop until a valid price is entered
            while (true)
            {
                Console.Write("Enter Price: ");
                if (double.TryParse(Console.ReadLine(), out price) && price >= 0)
                {
                    break;
                }

                Console.WriteLine("Error: Price must be a non-negative number.");
            }

            // Create a new product instance with the entered details
            var newProduct = new Product
            {
                ProductId = nextProductId,
                Name = productName,
                QuantityInStock = quantity,
                Price = price
            };

            // Add the new product to the inventory
            inventoryManager.AddProduct(newProduct);
        }

        /// <summary>
        /// Gets the next available product ID.
        /// </summary>
        /// <param name="inventoryManager">The inventory manager instance.</param>
        /// <returns>The next available product ID.</returns>
        private static int GetNextProductId(InventoryManager inventoryManager)
        {
            // Check if the inventory manager is not null
            if (inventoryManager == null)
            {
                throw new ArgumentNullException(nameof(inventoryManager));
            }

            // Check if there are any products in the inventory
            return inventoryManager.Product.Any()
                // If there are products, get the maximum product ID and increment it by 1
                ? inventoryManager.Product.Max(p => p.ProductId) + 1
                // If there are no products, start with product ID 1
                : 1;
        }


        /// <summary>
        /// Removes a product from the inventory.
        /// </summary>
        /// <param name="inventoryManager">The inventory manager instance.</param>
        private static void RemoveProduct(InventoryManager inventoryManager)
        {
            // Check if the inventory manager is null
            if (inventoryManager is null)
            {
                throw new ArgumentNullException(nameof(inventoryManager));
            }

            // Continuously prompt the user for a product ID until a valid one is entered
            while (true)
            {
                Console.Write("Enter Product ID: ");
                if (int.TryParse(Console.ReadLine(), out int productId))
                {
                    try
                    {
                        // Attempt to remove the product from the inventory
                        inventoryManager.RemoveProduct(productId);
                        break;
                    }
                    catch (InvalidOperationException ex)
                    {
                        // Display an error message if the product is not found
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    // Display an error message if the input is not a valid number
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
            }
        }

        /// <summary>
        /// Updates the quantity of a product in the inventory.
        /// </summary>
        /// <param name="inventoryManager">The inventory manager instance.</param>
        private static void UpdateProduct(InventoryManager inventoryManager)
        {
            // Initialize variables to store product ID and new quantity
            int productId;
            int newQuantity;

            // Prompt user to enter product ID to update
            while (true)
            {
                Console.Write("Enter Product ID to Update: ");
                // Attempt to parse user input as a positive integer
                if (int.TryParse(Console.ReadLine(), out productId) && productId > 0)
                {
                    // If successful, break out of loop
                    break;
                }
                // Display error message if input is invalid
                Console.WriteLine("Error: Product ID must be a positive integer.");
            }

            // Prompt user to enter new quantity
            while (true)
            {
                Console.Write("Enter New Quantity: ");
                // Attempt to parse user input as a non-negative integer
                if (int.TryParse(Console.ReadLine(), out newQuantity) && newQuantity >= 0)
                {
                    // If successful, break out of loop
                    break;
                }
                // Display error message if input is invalid
                Console.WriteLine("Error: Quantity cannot be negative.");
            }

            // Update product quantity using inventory manager
            inventoryManager.UpdateProduct(productId, newQuantity);
        }
    }
}