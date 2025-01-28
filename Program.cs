using System.Text;

namespace Inventory_Management_System
{
    public class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        static void Main(string[] args)
        {
            //Sets console encoding to UTF-8 to handle special characters.
            Console.OutputEncoding = Encoding.UTF8;
            InventoryManager inventory = new();

            while (true)
            {
                try
                {
                    Console.WriteLine("\nInventory Management System");
                    Console.WriteLine("1. Add Product");
                    Console.WriteLine("2. Remove Product");
                    Console.WriteLine("3. Update Product");
                    Console.WriteLine("4. List Products");
                    Console.WriteLine("5. Get Total Value");
                    Console.WriteLine("6. Exit");
                    Console.Write("Choose an option: ");
                    int option = Convert.ToInt32(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            AddProductWithAutoId(inventory);
                            break;
                        case 2:
                            RemoveProduct(inventory);
                            break;
                        case 3:
                            UpdateProduct(inventory);
                            break;
                        case 4:
                            inventory.ListProducts();
                            break;
                        case 5:
                            // Calculate and display the total value
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"Total Value: {inventory.GetTotalValue():C2}");
                            Console.ResetColor();
                            break;
                        case 6:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid option. Please choose from 1 to 6.");
                            Console.ResetColor();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    Console.ResetColor();
                }
            }
        }

        /// <summary>
        /// Prompts the user to add a new product to the inventory with an auto-generated Product ID.
        /// </summary>
        /// <param name="inventory">The inventory manager instance.</param>
        private static void AddProductWithAutoId(InventoryManager inventory)
        {
            try
            {
                // Generate the next available Product ID
                int nextProductId = GetNextProductId(inventory);

                Console.Write("Enter Product Name: ");
                string name = Console.ReadLine();
                // Check if the product name is empty or contains only whitespace
                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: \nProduct name cannot be empty.");
                    Console.ResetColor();
                    return;
                }

                Console.Write("Enter Quantity in Stock: ");
                int quantity = Convert.ToInt32(Console.ReadLine());
                // Check if the quantity is negative
                if (quantity < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: \nQuantity cannot be negative.");
                    Console.ResetColor();
                    return;
                }

                Console.Write("Enter Price: ");
                double price = Convert.ToDouble(Console.ReadLine());
                // Check if the price is negative
                if (price < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: \nPrice cannot be negative.");
                    Console.ResetColor();
                    return;
                }

                // Create the new product with the generated ID
                Product newProduct = new()
                {
                    ProductId = nextProductId,
                    Name = name,
                    QuantityInStock = quantity,
                    Price = price
                };

                // Call the existing AddProduct method to add the new product
                inventory.AddProduct(newProduct);
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter valid data for the product.");
                Console.ResetColor();
            }
            // Handle any other exceptions
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while adding the product: {ex.Message}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// This method generates the next unique Product ID by checking the existing IDs in the inventory. 
        /// If the inventory is empty, it returns 1; otherwise, it returns the highest existing ID plus 1.
        /// Ensuring the value is greater than 0 and to lessen the chance of duplicates and a much more user-friendly experience.
        /// </summary>
        /// <param name="inventory">The inventory manager instance.</param>
        /// <returns>The next Product ID.</returns>
        private static int GetNextProductId(InventoryManager inventory)
        {
            if (inventory.Products.Count == 0)
            {
                return 1;
            }
            return inventory.Products.Max(p => p.ProductId) + 1;
        }

        /// <summary>
        /// Prompts the user to remove a product from the inventory by its Product ID.
        /// </summary>
        /// <param name="inventory">The inventory manager instance.</param>
        private static void RemoveProduct(InventoryManager inventory)
        {
            try
            {
                Console.Write("Enter Product ID: ");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Green;
                inventory.RemoveProduct(id);
                Console.ResetColor();
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter a valid number.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while removing the product: {ex.Message}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Prompts the user to update the quantity of an existing product by its Product ID.
        /// </summary>
        /// <param name="inventory">The inventory manager instance.</param>
        private static void UpdateProduct(InventoryManager inventory)
        {
            try
            {
                Console.Write("Enter Product ID to Update: ");
                int id = Convert.ToInt32(Console.ReadLine());
                if (id <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: \nProduct ID must be a positive integer.");
                    Console.ResetColor();
                    return;
                }
                // Check if the product exists before updating
                if (!inventory.InventoryExists(id))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: \nProduct ID not found.");
                    Console.ResetColor();
                    return;
                }

                Console.Write("Enter New Quantity: ");
                int newQuantity = Convert.ToInt32(Console.ReadLine());
                if (newQuantity < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error: \nQuantity cannot be negative.");
                    Console.ResetColor();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Green;
                inventory.UpdateProduct(id, newQuantity);
                Console.ResetColor();
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter valid numeric values.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while updating the product: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}
