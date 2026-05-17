using HagglingShared;
using HagglingShared.Interfaces;
using HagglingShared.Models;
using OnlyUI;
using Customer;
using System.Reflection;

string path = Path.Combine(
    AppContext.BaseDirectory,
    "Haggling.dll");

var asm = Assembly.LoadFrom(path);

var vendorType = asm.GetTypes()
    .First(t =>
        typeof(IVendor).IsAssignableFrom(t)
        && !t.IsInterface);

string name = "Vendor";

decimal ownCapital = 1000m;

List<Product> inventory =
[
    new Product
    {
        Id = Guid.NewGuid(),
        Name = "Apple",
        Category = ProductCategory.Fruit,
        Rarity = Rarity.Common,
        AskingPrice = 100m,
        Perishable = true
    }
];

IUI ui = new SpectreUI();

ICustomer customer =
    CustomerFactory.CreateRandom(500m);

IVendor vendor =
    (IVendor)Activator.CreateInstance(
        vendorType,
        name,
        ownCapital,
        inventory)!;

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