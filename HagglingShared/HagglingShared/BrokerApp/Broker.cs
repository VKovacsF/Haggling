using HagglingShared.Interfaces;
using HagglingShared.Models;
using OnlyUI;
using Customer; 
using Haggling;

var asm = Assembly.LoadFrom("Haggling.dll");

var vendorType = asm.GetTypes().First(t => typeof(IVendor).IsAssignableFrom(t) && !t.IsInterface);

IUI       ui       = new SpectreUI();
ICustomer customer = CustomerFactory.CreateRandom(budget: 500m);
IVendor   vendor   =  (IVendor)Activator.CreateInstance(vendorType, name, ownCapital, inventory)!;

var broker = new Broker(ui, customer, vendor);

var product = vendor.Inventory.First();
var result  = broker.StartNegotiation(product);

Console.WriteLine($"Outcome: {result.Outcome}, Final price: {result.FinalPrice}");

public class Broker(IUI ui, ICustomer customer, IVendor vendor, int maxRounds = 10)
{
    public HaggleResult StartNegotiation(Product product)
    {
        ui.ShowStart();

        decimal currentPrice = product.AskingPrice;
        int round = 0;

        while (round < maxRounds)
        {

            decimal vendorPrice = vendor.MakeOffer(currentPrice);
            ui.ShowOffer(vendor.Name, vendorPrice);


            HaggleOffer customerOffer = customer.RespondToOffer(product, vendorPrice, round);

            if (customerOffer.IsWalkingAway)
            {
                ui.ShowRejected();
                return Finish(product, new HaggleResult(HaggleOutcome.CustomerWalkedAway, null, round));
            }

            ui.ShowCounterOffer(customer.Name, customerOffer.Price);

            HaggleOffer vendorResponse = vendor.Respond(customerOffer);

            if (!vendorResponse.IsWalkingAway && vendorResponse.Price <= customerOffer.Price)
            {
                ui.ShowAccepted();
                return Finish(product, new HaggleResult(HaggleOutcome.Deal, vendorResponse.Price, round));
            }

            if (vendorResponse.IsWalkingAway)
            {
                ui.ShowRejected();
                return Finish(product, new HaggleResult(HaggleOutcome.VendorRefused, null, round));
            }

            currentPrice = vendorResponse.Price;
            round++;
        }

        return Finish(product, new HaggleResult(HaggleOutcome.MaxRoundsReached, null, round));
    }

    private HaggleResult Finish(Product product, HaggleResult result)
    {
        customer.OnHaggleComplete(product, result);
        return result;
    }
}