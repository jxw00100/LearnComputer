using System;

namespace LearnComputer.CircuitInfrustructure
{
    public abstract class Endpoint
    {
        protected const String INVALID_SIGNAL_EXCEPTION_FORMAT =
            "Signal value can only be either 0 or 1. {0} is not a valid value.";
        public Endpoint ConnectedPoint { get; protected set; }

        public Endpoint(Endpoint connectTo = null)
        {
            ConnectTo(connectTo);
        }

        public void ConnectTo(Endpoint point)
        {
            if (point != null && !Object.ReferenceEquals(point, ConnectedPoint))
            {
                if (ConnectedPoint != null) ConnectedPoint.DisconnectEndpoint();
                ConnectedPoint = point;
                point.ConnectTo(this);
            }
        }

        public virtual void DisconnectEndpoint()
        {
            if (ConnectedPoint != null)
            {
                Endpoint point = ConnectedPoint;
                ConnectedPoint = null;
                point.DisconnectEndpoint();
            }
        }
    }
}
