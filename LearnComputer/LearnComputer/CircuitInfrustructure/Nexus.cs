using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnComputer.CircuitInfrustructure
{
    public class Nexus
    {
        private const string DUPLICATE_ENDPOINTS_EXCETION = "Unable to connect 2 same endpoints.";
        private NeutralEndpoint[] _endpoints;
        private Dictionary<NeutralEndpoint, Byte> _inputStatus = new Dictionary<NeutralEndpoint, Byte>();
        private Dictionary<NeutralEndpoint, Byte> _outputStatus = new Dictionary<NeutralEndpoint, Byte>();

        public Byte LastSignalStatus
        {
            get
            {
                Int32 preSignal = 0;
                foreach (NeutralEndpoint points in _endpoints)
                {
                    preSignal = points.LastReceivedSignal | preSignal;
                }
                return (Byte)preSignal;
            }
        }

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
                _inputStatus.Add(_endpoints[i], 0);
                _outputStatus.Add(_endpoints[i], 0);
            }
        }

        public Int32 EndpointsCount()
        {
            return _endpoints.Length;
        }

        public void ConnectAt(Endpoint connectedPoint, Int32 atIndex)
        {
            NeutralEndpoint endpoint = _endpoints[atIndex];
            _inputStatus[endpoint] = 0;
            endpoint.ConnectTo(connectedPoint);
            endpoint.Produce(LastSignalStatus);
        }

        private void SignalReceivedHandler(Endpoint sender, Byte signal)
        {
            if (LastSignalStatusExcludeThisSender(sender) == 0)
            {
                Byte previousSignal = _inputStatus[(NeutralEndpoint) sender.ConnectedPoint];
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

        private Byte LastSignalStatusExcludeThisSender(Endpoint sender)
        {
            Int32 preSignal = 0;
            foreach (NeutralEndpoint points in _endpoints)
            {
                if (!Object.ReferenceEquals(sender, points.ConnectedPoint))
                {
                    preSignal = points.LastReceivedSignal | preSignal;
                }
            }
            return (Byte)preSignal;
        }

        private Boolean SenderIsFromOutside(Endpoint sender)
        {
            return _endpoints.All((point) => !Object.ReferenceEquals(point, sender));
;       }
    }
}
