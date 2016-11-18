using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class NeutralEndpoint: InputEndpoint, IOutputEndpoint
    {
        public NeutralEndpoint():base(null)
        {}

        public NeutralEndpoint(Endpoint connectTo)
            : base(connectTo)
        {}

        public void Produce(Byte signal)
        {
            if (signal != 0 && signal != 1)
                throw new ArgumentException(String.Format(INVALID_SIGNAL_EXCEPTION_FORMAT, signal));

            var connectedPoint = ConnectedPoint as InputEndpoint;
            if (connectedPoint != null)
            {
                connectedPoint.Transmit(signal);
            }
        }
    }
}
