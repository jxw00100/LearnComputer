using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class Wire
    {
        private const string DUPLICATE_ENDPOINTS_EXCETION = "Unable to connect 2 same endpoints.";
        private NeutralEndpoint[] _endpoints = new NeutralEndpoint[2];

        public Wire(Endpoint point1 = null, Endpoint point2 = null)
        {
            if (point1 != null && point2 != null && Object.ReferenceEquals(point1, point2))
                throw new ArgumentException(DUPLICATE_ENDPOINTS_EXCETION);

            _endpoints[0] = new NeutralEndpoint(point1);
            _endpoints[1] = new NeutralEndpoint(point2);

            _endpoints[0].Receive += SignalReceivedHandler;
            _endpoints[1].Receive += SignalReceivedHandler;
        }

        public void Connect(Endpoint point1, Endpoint point2)
        {
            if (point1 != null && point2 != null && Object.ReferenceEquals(point1, point2))
                throw new ArgumentException(DUPLICATE_ENDPOINTS_EXCETION);
            _endpoints[0].ConnectTo(point1);
            _endpoints[1].ConnectTo(point2);
        }

        private void SignalReceivedHandler(Endpoint sender, Byte signal)
        {
            if (Object.ReferenceEquals(sender, _endpoints[0].ConnectedPoint))
            {
                _endpoints[1].Produce(signal);
            }
            else if (Object.ReferenceEquals(sender, _endpoints[1].ConnectedPoint))
            {
                _endpoints[0].Produce(signal);
            }
        }

    }
}
