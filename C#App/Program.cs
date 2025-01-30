using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Libary.eCom.Models;
using Libary.eCom.Services;


namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product?> inventory = InventoryServiceProxy.Current.Products;
            CartServiceProxy cart = new CartServiceProxy();
            char choice;
            do {
                Console.WriteLine("P. Edit Products \nS. Edit Shoppingcart");
                string? input = Console.ReadLine();
                choice = Char.ToUpper(input[0]);
                if (choice == 'P')
                {
                    printList();
                    input = Console.ReadLine();
                    choice = Char.ToUpper(input[0]);
                    switch (choice)
                    {
                        case 'C':
                            Console.WriteLine("Enter the Name then Price then Count of the new product");
                            InventoryServiceProxy.Current.Add(new Product
                            (
                                Console.ReadLine() ?? "ERROR",
                                double.Parse(Console.ReadLine() ?? "0.0"),
                                int.Parse(Console.ReadLine() ?? "0")

                            ));
                            break;
                        case 'U':
                            Console.WriteLine("Which product would you like to update?");
                            int selection = int.Parse(Console.ReadLine() ?? "-1");
                            

                            if (selection == -1)
                            {
                                break;
                            }
                            printInvUpdate();

                            selection = int.Parse(Console.ReadLine() ?? "-1");
                            if (selection == -1)
                            {
                                Console.WriteLine("Not a valid option");
                                break;
                            }

                            switch (selection)
                            {
                                case 1:
                                    Console.WriteLine("What is the new name for this product?");
                                    string newVal = Console.ReadLine() ?? "ERROR";
                                    InventoryServiceProxy.Current.AddOrUpdate(selection, 1, newVal);

                                    break;

                                case 2:
                                    Console.WriteLine("What is the new price");
                                    newVal = Console.ReadLine() ?? "-1.0";

                                    InventoryServiceProxy.Current.AddOrUpdate(selection, 2, newVal);

                                    break;

                                case 3:
                                    Console.WriteLine("What is the new count of the products?");
                                    newVal = Console.ReadLine() ?? "-1";

                                    InventoryServiceProxy.Current.AddOrUpdate(selection, 3, newVal);

                                    break;
                            }
                            break;
                        case 'R':
                            inventory.ForEach(Console.WriteLine);
                            break;
                        case 'D':
                            Console.WriteLine("Which product would you like to delete?");
                            selection = int.Parse(Console.ReadLine() ?? "-1");
                            InventoryServiceProxy.Current.Delete(selection);
                            break;
                        case 'Q':
                            quit(cart);
                            break;
                    }
                }
                else if(choice == 'S')
                {
                    printCartUpdate();
                    input = Console.ReadLine();
                    choice = Char.ToUpper(input[0]);
                    switch (choice)
                    {
                        case 'A':
                            Console.WriteLine("What is the ID of the item you would like to add to the cart?");
                            int Id = int.Parse(Console.ReadLine() ?? "-1");
                            Console.WriteLine("How many would you like to buy?");
                            int num = int.Parse(Console.ReadLine() ?? "1");
                            cart.add(Id, num);
                            break;
                        case 'U':
                            Console.WriteLine("What is the Id of the item you want to change?");
                            Id = int.Parse(Console.ReadLine() ?? "-1");
                            Console.WriteLine("How many would you to remove?");
                            num = int.Parse(Console.ReadLine() ?? "1");
                            cart.RemoveOrDelete(Id,num);
                            break;
                        case 'R':
                            cart.shoppingCart.ForEach(Console.WriteLine);
                            break;
                        case 'Q':
                            quit(cart);
                            break;
                    }
                }
                 
            } while(choice != 'Q');
              
        }


        static void printList()
        {
            Console.WriteLine("C. Create new inventory item");
            Console.WriteLine("R. Read all inventory items");
            Console.WriteLine("U. Update an inventory item");
            Console.WriteLine("D. Delete an inventory item");
            Console.WriteLine("Q. Quit");
        }

        static void printInvUpdate()
        {
            Console.WriteLine("What would you like to change about this product");
            Console.WriteLine("1. Name");
            Console.WriteLine("2. Price");
            Console.WriteLine("3. Count");
        }

        static void printCartUpdate()
        {
            Console.WriteLine("A. Add to cart");
            Console.WriteLine("U. Remove from card");
            Console.WriteLine("R. Print Cart");
            Console.WriteLine("Q. Check out");
        }

        static void quit(CartServiceProxy cart)
        {
            List<double> totals = cart.checkOut();
            cart.shoppingCart.ForEach(Console.WriteLine);
            Console.WriteLine($"Subtotal = {totals[0]}\nTax = {totals[1]}\nTotal = {totals[0] + totals[1]}");
        }
    }


}