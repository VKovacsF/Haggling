using HagglingShared.Interfaces;
using HagglingShared.Models;
using UI;
using Customer;
using HagglingShared;
using System.Reflection;

namespace HagglingShared;

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
        this.ui = ui ?? throw new ArgumentNullException(nameof(ui));
        this.customer = customer ?? throw new ArgumentNullException(nameof(customer));
        this.vendor = vendor ?? throw new ArgumentNullException(nameof(vendor));

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

        if (product.AskingPrice < 0)
        {
            throw new ArgumentException(
                "Product asking price cannot be negative.",
                nameof(product));
        }

        ui.ShowStart();
        Thread.Sleep(1000);

        ui.ShowProductInfo(
            product,
            customer.GetInterestLevel(product),
            vendor.Patience);

        Thread.Sleep(1000);

        decimal currentPrice = product.AskingPrice;
        int round = 0;

        while (round < maxRounds)
        {
            decimal vendorPrice = vendor.MakeOffer(currentPrice);

            if (vendorPrice < 0)
            {
                throw new InvalidOperationException(
                    "Vendor returned a negative price.");
            }

            ui.ShowOffer(vendor.Name, vendorPrice);
            Thread.Sleep(1000);

            HaggleOffer customerOffer =
                customer.RespondToOffer(product, vendorPrice, round);

            if (customerOffer.Price < 0)
            {
                throw new InvalidOperationException(
                    "Customer returned a negative price.");
            }

            if (customerOffer.Outcome ==
                HaggleOutcome.CustomerWalkedAway)
            {
                ui.ShowRejected();

                return Finish(
                    product,
                    new HaggleResult(
                        HaggleOutcome.CustomerWalkedAway,
                        null,
                        round));
            }

            ui.ShowCounterOffer(customer.Name, customerOffer.Price);
            Thread.Sleep(1000);

            HaggleOffer vendorResponse =
                vendor.Respond(customerOffer);

            if (vendorResponse.Price < 0)
            {
                throw new InvalidOperationException(
                    "Vendor returned a negative counter price.");
            }

            if (vendorResponse.Outcome == HaggleOutcome.Deal)
            {
                ui.ShowAccepted();

                return Finish(
                    product,
                    new HaggleResult(
                        HaggleOutcome.Deal,
                        vendorResponse.Price,
                        round));
            }

            if (vendorResponse.Outcome ==
                HaggleOutcome.VendorRefused)
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