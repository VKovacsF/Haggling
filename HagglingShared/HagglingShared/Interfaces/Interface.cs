using HagglingShared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HagglingShared.Interfaces
{
    /// <summary>
    /// Represents a customer participating in haggling.
    /// </summary>
    public interface ICustomer
    {
        string Name { get; }

        /// <summary>
        /// Gets the customer's interest level for a product.
        /// </summary>
        double GetInterestLevel(Product product);

        HaggleOffer RespondToOffer(Product product, decimal vendorPrice, int round);

        void OnHaggleComplete(Product product, HaggleResult result);
    }

    /// <summary>
    /// Represents a vendor participating in haggling.
    /// </summary>
    public interface IVendor
    {
        string Name { get; }

        List<Product> Inventory { get; }

        int Patience { get; }

        decimal MakeOffer(decimal currentPrice);

        HaggleOffer Respond(HaggleOffer customerOffer);
    }

    /// <summary>
    /// Represents the user interface.
    /// </summary>
    public interface IUI
    {
        void ShowStart();

        void ShowProductInfo(Product product, double interestLevel, int patience);

        void ShowOffer(string vendorName, decimal price);

        void ShowCounterOffer(string customerName, decimal price);

        void ShowAccepted();

        void ShowRejected();
    }
}