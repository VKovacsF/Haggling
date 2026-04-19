using System;
using System.Collections.Generic;
using System.Text;

namespace Haggling.Shared
{
    public class Negotiation
    {
        public bool IsFinished { get; private set; }
        public int MaxRounds { get; }

        private int _currentRound = 0;

        public Negotiation(int maxRounds = 10) => MaxRounds = maxRounds;

        public void Start() { IsFinished = false; _currentRound = 0; }

        public (HaggleOffer offer, int round) RunRound(HaggleOffer incoming)
        {
            _currentRound++;
            if (_currentRound >= MaxRounds) IsFinished = true;
            return (incoming, _currentRound);
        }
    }
}
