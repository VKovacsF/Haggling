using System;
using System.Collections.Generic;
using System.Text;

namespace HagglingShared.Models
{
    /// <summary>
    /// Represents a haggle offer.
    /// </summary>
    public class HaggleOffer
    {
        /// <summary>
        /// Gets the offered price.
        /// </summary>
        public decimal Price { get; }

        /// <summary>
        /// Gets the current outcome of this offer.
        /// </summary>
        public HaggleOutcome Outcome { get; }

        /// <summary>
        /// Creates a haggle offer.
        /// </summary>
        public HaggleOffer(
            decimal price,
            HaggleOutcome outcome = HaggleOutcome.Ongoing)
        {
            if (price < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(price));
            }

            Price = price;
            Outcome = outcome;
        }
    }

    /// <summary>
    /// Represents the result of a negotiation.
    /// </summary>
    public class HaggleResult
    {
        /// <summary>
        /// Gets the outcome.
        /// </summary>
        public HaggleOutcome Outcome { get; }

        /// <summary>
        /// Gets the final price.
        /// </summary>
        public decimal? FinalPrice { get; }

        /// <summary>
        /// Gets the number of rounds.
        /// </summary>
        public int RoundsPlayed { get; }

        /// <summary>
        /// Creates a haggle result.
        /// </summary>
        public HaggleResult(
            HaggleOutcome outcome,
            decimal? finalPrice,
            int roundsPlayed)
        {
            if (roundsPlayed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(roundsPlayed));
            }

            Outcome = outcome;
            FinalPrice = finalPrice;
            RoundsPlayed = roundsPlayed;
        }
    }

    /// <summary>
    /// Represents the result of haggling.
    /// </summary>
    public enum HaggleOutcome
    {
        Ongoing,
        Deal,
        CustomerWalkedAway,
        VendorRefused,
        MaxRoundsReached
    }
}