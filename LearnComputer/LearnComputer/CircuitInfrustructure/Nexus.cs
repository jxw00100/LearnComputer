using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class Nexus
    {
        private const string DUPLICATE_ENDPOINTS_EXCETION = "Unable to connect 2 same endpoints.";
        private NeutralEndpoint[] _endpoints;

        public Nexus(Int32 endpointCount, params Endpoint[] connectedPoints)
        {
            _endpoints = new NeutralEndpoint[endpointCount];
            InitializeEndpoints(connectedPoints);
        }

        private void InitializeEndpoints(Endpoint[] connectedPoints)
        {
            for (var i = 0; i < _endpoints.Length; i++)
            {
                if (i < connectedPoints.Length)
                {
                    _endpoints[i] = new NeutralEndpoint(connectedPoints[i]);
                }
                else
                {
                    _endpoints[i] = new NeutralEndpoint();
                }

                _endpoints[i].Receive += SignalReceivedHandler;
            }
        }

        public Int32 EndpointsCount()
        {
            return _endpoints.Length;
        }

        public void ConnectAt(Endpoint endpoint, Int32 atIndex)
        {
            _endpoints[atIndex].ConnectTo(endpoint);
        }

        private void SignalReceivedHandler(Endpoint sender, Byte signal)
        {
            foreach (NeutralEndpoint points in _endpoints)
            {
                if (!Object.ReferenceEquals(sender, points.ConnectedPoint))
                {
                    points.Produce(signal);
                }
            }
        }
    }
}
