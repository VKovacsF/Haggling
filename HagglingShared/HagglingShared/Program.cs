using HagglingShared;
using HagglingShared.Interfaces;
using HagglingShared.Models;
using UI;
using Customer;
using System.Reflection;
using Customer.Customer;
using System;
using System.Runtime.Serialization;
class Program
{
    static void Main(string[] args)
    {

        string path = Path.Combine(AppContext.BaseDirectory,"Haggling.dll");

        string name = "Vendor";

        double ownCapital = 1000;

        Product p1 = new Product();
        p1.Id = Guid.NewGuid();
        p1.Name = "Apple";
        p1.Category = ProductCategory.Fruit;
        p1.Rarity = Rarity.Common;
        p1.AskingPrice = 100;
        p1.Perishable = true;

        InventoryItem inv1 = new InventoryItem(p1, 100, 5);

        InventoryItem[] inventory = {inv1};


        IUI ui = new SpectreUI();

        ICustomer customer =
            CustomerFactory.CreateRandom(500m);

        IVendor vendor = new Vendor(name, ownCapital, inventory);

        Broker broker =
            new Broker(ui, customer, vendor);

        Product product =
            vendor.Inventory.First();

        HaggleResult result =
            broker.StartNegotiation(product);

        Console.WriteLine();

        Console.WriteLine(
            $"Outcome: {result.Outcome}");

        Console.WriteLine(
            $"Final price: {result.FinalPrice}");
    }
}