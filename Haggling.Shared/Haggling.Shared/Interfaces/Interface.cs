using System;
using System.Collections.Generic;
using System.Text;

namespace Haggling.Shared
{
    public interface ICustomer
    {
        string Name { get; }
        decimal Budget { get; }
        HaggleOffer? ConsiderProduct(Product product);
        HaggleOffer RespondToOffer(Product product, decimal vendorPrice, int round);
        void OnHaggleComplete(Product product, HaggleResult result);
        double GetInterestLevel(Product product);
    }

    public interface IUI
    {
        void ShowStart();
        void ShowOffer(string actor, decimal price);
        void ShowCounterOffer(string actor, decimal price);
        void ShowAccepted();
        void ShowRejected();
    }

    public interface IVendor
    {
        string Name { get; }
        double OwnCapital { get; }
        Product[] Inventory { get; }
        decimal MakeOffer(decimal currentPrice);
        HaggleOffer Respond(HaggleOffer offer);
        int Patience(ICustomer customer);
    }
}
