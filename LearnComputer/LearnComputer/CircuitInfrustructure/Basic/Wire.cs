using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class Wire
    {
        private const string DUPLICATE_ENDPOINTS_EXCETION = "Unable to connect 2 same endpoints.";
        private INeutralEndpointCollection<INeutralEndpoint> _endpoints;

        public Wire(IEndpoint point1 = null, IEndpoint point2 = null)
        {
            if (point1 != null && point2 != null && Object.ReferenceEquals(point1, point2))
                throw new ArgumentException(DUPLICATE_ENDPOINTS_EXCETION);

            _endpoints = new NeutralEndpointCollection<NeutralEndpoint>(2);
            _endpoints.RegisterReceiveHandler(SignalReceivedHandler);
            Connect(point1, point2);
        }

        public void Connect(IEndpoint point1, IEndpoint point2)
        {
            if (point1 != null && point2 != null && Object.ReferenceEquals(point1, point2))
                throw new ArgumentException(DUPLICATE_ENDPOINTS_EXCETION);
            _endpoints.Connect(new IEndpoint[] {point1, point2});
        }

        private void SignalReceivedHandler(IEndpoint sender, Int32 signal)
        {
            if (Object.ReferenceEquals(sender, _endpoints[0].ConnectedPoint))
            {
                _endpoints.ProduceAt(1, signal);
            }
            else if (Object.ReferenceEquals(sender, _endpoints[1].ConnectedPoint))
            {
                _endpoints.ProduceAt(0, signal);
            }
        }

    }
}
