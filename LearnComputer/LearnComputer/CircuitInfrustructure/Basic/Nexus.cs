using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LearnComputer.CircuitInfrustructure
{
    public class Nexus
    {
        private const string DUPLICATE_ENDPOINTS_EXCETION = "Unable to connect 2 same endpoints.";
        private INeutralEndpointCollection<INeutralEndpoint> _endpoints;
        private Dictionary<INeutralEndpoint, Int32> _inputStatus = new Dictionary<INeutralEndpoint, Int32>();
        private Mutex mutex = new Mutex();

        public Int32 LastSignalStatus
        {
            get
            {
                Int32 preSignal = 0;
                foreach (Int32 lastSignal in _endpoints.GetLastReceivedSignals())
                {
                    preSignal = lastSignal | preSignal;
                }
                return preSignal;
            }
        }

        public Nexus(Int32 endpointCount, params IEndpoint[] connectedPoints)
        {
            _endpoints = new NeutralEndpointCollection<NeutralEndpoint>(endpointCount);
            InitializeEndpoints(connectedPoints);
        }

        public Nexus(Int32 endpointCount, IEnumerable<IEndpoint> connectedPoints)
        {
            _endpoints = new NeutralEndpointCollection<NeutralEndpoint>(endpointCount);
            InitializeEndpoints(connectedPoints);
        }

        private void InitializeEndpoints(IEnumerable<IEndpoint> connectedPoints)
        {
            _endpoints.RegisterReceiveHandler(SignalReceivedHandler);
            foreach (var endpoint in _endpoints)
            {
                _inputStatus.Add(endpoint, 0);
            }
            _endpoints.Connect(connectedPoints);
        }

        public Int32 EndpointsCount()
        {
            return _endpoints.Count;
        }

        public void ConnectAt(IEndpoint connectedPoint, Int32 index)
        {
            _inputStatus[_endpoints[index]] = 0;
            _endpoints.ConnectAt(index, connectedPoint);
            _endpoints.ProduceAt(index, LastSignalStatus);
        }

        private void SignalReceivedHandler(IEndpoint sender, Int32 signal)
        {
            mutex.WaitOne();
            if (LastSignalStatusExcludeThisSender(sender) == 0)
            {
                Int32 previousSignal = _inputStatus[(INeutralEndpoint)sender.ConnectedPoint];
                if (previousSignal != signal)
                {
                    foreach (INeutralEndpoint endpoint in _endpoints)
                    {
                        if (!Object.ReferenceEquals(sender, endpoint.ConnectedPoint))
                        {
                            new Task((ep) =>
                            {
                                var endpt = (IOutputEndpoint) ep;
                                if(endpt != null) endpt.Produce(signal);
                            }, endpoint).Start();
                        }
                    }
                }
            }
            _inputStatus[(INeutralEndpoint)sender.ConnectedPoint] = signal;
            mutex.ReleaseMutex();
        }

        private Int32 LastSignalStatusExcludeThisSender(IEndpoint sender)
        {
            Int32 preSignal = 0;
            foreach (INeutralEndpoint endpoint in _endpoints)
            {
                if (!Object.ReferenceEquals(sender, endpoint.ConnectedPoint))
                {
                    preSignal = endpoint.LastReceivedSignal | preSignal;
                }
            }
            return preSignal;
        }

        public INeutralEndpoint GetEndpointAt(Int32 index)
        {
            if(index <0 || index >= _endpoints.Count) throw new ArgumentException("Index out of the endpoints bound");
            return _endpoints[index];
        }
    }
}
