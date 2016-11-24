using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnComputer.CircuitInfrustructure
{
    public class Nexus
    {
        private const string DUPLICATE_ENDPOINTS_EXCETION = "Unable to connect 2 same endpoints.";
        private NeutralEndpoint[] _endpoints;
        private Dictionary<NeutralEndpoint, Int32> _inputStatus = new Dictionary<NeutralEndpoint, Int32>();
        private Dictionary<NeutralEndpoint, Int32> _outputStatus = new Dictionary<NeutralEndpoint, Int32>();

        public Int32 LastSignalStatus
        {
            get
            {
                Int32 preSignal = 0;
                foreach (NeutralEndpoint points in _endpoints)
                {
                    preSignal = points.LastReceivedSignal | preSignal;
                }
                return preSignal;
            }
        }

        public Nexus(Int32 endpointCount, params IEndpoint[] connectedPoints)
        {
            _endpoints = new NeutralEndpoint[endpointCount];
            InitializeEndpoints(connectedPoints);
        }

        private void InitializeEndpoints(IEndpoint[] connectedPoints)
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
                _inputStatus.Add(_endpoints[i], 0);
                _outputStatus.Add(_endpoints[i], 0);
            }
        }

        public Int32 EndpointsCount()
        {
            return _endpoints.Length;
        }

        public void ConnectAt(IEndpoint connectedPoint, Int32 atIndex)
        {
            NeutralEndpoint endpoint = _endpoints[atIndex];
            _inputStatus[endpoint] = 0;
            endpoint.ConnectTo(connectedPoint);
            endpoint.Produce(LastSignalStatus);
        }

        private void SignalReceivedHandler(IEndpoint sender, Int32 signal)
        {
            if (LastSignalStatusExcludeThisSender(sender) == 0)
            {
                Int32 previousSignal = _inputStatus[(NeutralEndpoint)sender.ConnectedPoint];
                if (previousSignal != signal)
                {
                    foreach (NeutralEndpoint points in _endpoints)
                    {
                        if (!Object.ReferenceEquals(sender, points.ConnectedPoint))
                        {
                            points.Produce(signal);
                        }
                    }
                    _inputStatus[(NeutralEndpoint) sender.ConnectedPoint] = signal;
                }
            }
        }

        private Int32 LastSignalStatusExcludeThisSender(IEndpoint sender)
        {
            Int32 preSignal = 0;
            foreach (NeutralEndpoint points in _endpoints)
            {
                if (!Object.ReferenceEquals(sender, points.ConnectedPoint))
                {
                    preSignal = points.LastReceivedSignal | preSignal;
                }
            }
            return preSignal;
        }

        public INeutralEndpoint GetEndpointAt(Int32 index)
        {
            if(index <0 || index >= _endpoints.Length) throw new ArgumentException("Index out of the endpoints bound");
            return _endpoints[index];
        }
    }
}
