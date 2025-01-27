// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");4


using System;
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
            var lastId = 1;


            char choice;
            do {
                printList();
                string? input = Console.ReadLine();
                choice = Char.ToUpper(input[0]);

                switch (choice)
                {
                    case 'C':
                        inventory.Add(new Product { Id = lastId++, Name = Console.ReadLine() });
                        break;
                    case 'U':
                        Console.WriteLine("Which product would you like to update?");
                        int selection = int.Parse(Console.ReadLine() ?? "-1");
                        var selectedProduct = inventory.FirstOrDefault(p => p.Id == selection);
                        if( selectedProduct != null)
                            selectedProduct.Name = Console.ReadLine() ?? "ERROR";
                        break;
                    case 'R':
                        inventory.ForEach(Console.WriteLine);
                        break;
                    case 'D':
                        Console.WriteLine("Which product would you like to delete?");
                        selection = int.Parse(Console.ReadLine() ?? "-1");
                        selectedProduct = inventory.FirstOrDefault(p => p.Id == selection);
                        inventory.Remove(selectedProduct);
                        break;
                    case 'Q':
                        break;
                }

            } while(choice != 'Q');
        }


        static void printList()
        {
            Console.WriteLine("C. Create new inventory item");
            Console.WriteLine("U. Update an inventory item");
            Console.WriteLine("R. Read all inventory items");
            Console.WriteLine("D. Delete an inventory item");
        }
    }


}