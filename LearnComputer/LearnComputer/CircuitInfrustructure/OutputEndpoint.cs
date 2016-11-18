using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class OutputEndpoint: Endpoint
    {
        public OutputEndpoint():base(null)
        {}

        public OutputEndpoint(Endpoint connectTo)
            : base(connectTo)
        {}
        public void Produce(Byte signal)
        {
            if (signal != 0 && signal != 1)
                throw new ArgumentException(String.Format(INVALID_SIGNAL_EXCEPTION_FORMAT, signal));

            var connectedPoint = ConnectedPoint as IInputEndpoint;
            if (connectedPoint != null)
            {
                connectedPoint.Transmit(signal);
            }
        }
    }
}
