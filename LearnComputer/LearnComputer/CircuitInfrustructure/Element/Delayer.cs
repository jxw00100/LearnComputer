using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class Delayer: Relay
    {
        public Delayer(Int32 delayMilliseconds) : base(delayMilliseconds, false)
        {
            if(delayMilliseconds <= 0) throw new ArgumentException("Delay time must be large than 0");
        }
    }
}
