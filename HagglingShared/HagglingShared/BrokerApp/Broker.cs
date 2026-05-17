using HagglingShared.Interfaces;
using HagglingShared.Models;
using OnlyUI;
using Customer;
using HagglingShared;
using System.Reflection;
/// <summary>
/// Controls the haggling process.
/// </summary>
public class Broker
{
    private readonly IUI ui;
    private readonly ICustomer customer;
    private readonly IVendor vendor;
    private readonly int maxRounds;

    /// <summary>
    /// Creates a broker.
    /// </summary>
    public Broker(
        IUI ui,
        ICustomer customer,
        IVendor vendor,
        int maxRounds = 10)
    {
        this.ui =
            ui ?? throw new ArgumentNullException(nameof(ui));

        this.customer =
            customer ?? throw new ArgumentNullException(nameof(customer));

        this.vendor =
            vendor ?? throw new ArgumentNullException(nameof(vendor));

        if (maxRounds <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxRounds));
        }

        this.maxRounds = maxRounds;
    }

    /// <summary>
    /// Starts negotiation for a product.
    /// </summary>
    public HaggleResult StartNegotiation(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        ui.ShowStart();

        Thread.Sleep(1000);

        decimal currentPrice = product.AskingPrice;

        int round = 0;

        while (round < maxRounds)
        {
            decimal vendorPrice =
                vendor.MakeOffer(currentPrice);

            ui.ShowOffer(vendor.Name, vendorPrice);

            Thread.Sleep(1000);

            HaggleOffer customerOffer =
                customer.RespondToOffer(
                    product,
                    vendorPrice,
                    round);

            if (customerOffer.IsWalkingAway)
            {
                ui.ShowRejected();

                return Finish(
                    product,
                    new HaggleResult(
                        HaggleOutcome.CustomerWalkedAway,
                        null,
                        round));
            }

            ui.ShowCounterOffer(
                customer.Name,
                customerOffer.Price);

            Thread.Sleep(1000);

            HaggleOffer vendorResponse =
                vendor.Respond(customerOffer);

            if (!vendorResponse.IsWalkingAway &&
                vendorResponse.Price <= customerOffer.Price)
            {
                ui.ShowAccepted();

                return Finish(
                    product,
                    new HaggleResult(
                        HaggleOutcome.Deal,
                        vendorResponse.Price,
                        round));
            }

            if (vendorResponse.IsWalkingAway)
            {
                ui.ShowRejected();

                return Finish(
                    product,
                    new HaggleResult(
                        HaggleOutcome.VendorRefused,
                        null,
                        round));
            }

            currentPrice = vendorResponse.Price;

            round++;
        }

        return Finish(
            product,
            new HaggleResult(
                HaggleOutcome.MaxRoundsReached,
                null,
                round));
    }

    private HaggleResult Finish(
        Product product,
        HaggleResult result)
    {
        customer.OnHaggleComplete(product, result);

        return result;
    }
}