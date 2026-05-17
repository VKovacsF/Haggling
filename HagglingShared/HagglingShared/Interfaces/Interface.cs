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
        /// <summary>
        /// Gets the customer name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Responds to a vendor offer.
        /// </summary>
        HaggleOffer RespondToOffer(
            Product product,
            decimal vendorPrice,
            int round);

        /// <summary>
        /// Called after haggling has completed.
        /// </summary>
        void OnHaggleComplete(
            Product product,
            HaggleResult result);
    }
    /// <summary>
    /// Represents a vendor participating in haggling.
    /// </summary>
    public interface IVendor
    {
        /// <summary>
        /// Gets the vendor name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the vendor inventory.
        /// </summary>
        List<Product> Inventory { get; }

        /// <summary>
        /// Creates an initial offer.
        /// </summary>
        decimal MakeOffer(decimal currentPrice);

        /// <summary>
        /// Responds to a customer counter offer.
        /// </summary>
        HaggleOffer Respond(HaggleOffer customerOffer);
    }

    /// <summary>
    /// Represents the user interface.
    /// </summary>
    public interface IUI
    {
        /// <summary>
        /// Shows the start of haggling.
        /// </summary>
        void ShowStart();

        /// <summary>
        /// Shows a vendor offer.
        /// </summary>
        void ShowOffer(
            string vendorName,
            decimal price);

        /// <summary>
        /// Shows a customer counter offer.
        /// </summary>
        void ShowCounterOffer(
            string customerName,
            decimal price);

        /// <summary>
        /// Shows that the deal was accepted.
        /// </summary>
        void ShowAccepted();

        /// <summary>
        /// Shows that the deal was rejected.
        /// </summary>
        void ShowRejected();
    }
}
