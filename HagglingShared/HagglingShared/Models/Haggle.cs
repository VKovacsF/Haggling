using System;
using System.Collections.Generic;
using System.Text;

namespace HagglingShared.Models
{
        public record HaggleOffer(decimal Price, bool IsWalkingAway);

        public record HaggleResult(HaggleOutcome Outcome, decimal? FinalPrice, int RoundsPlayed);

        public enum HaggleOutcome
        {
            Deal,
            VendorRefused,
            CustomerWalkedAway,
            MaxRoundsReached
        }
    }
