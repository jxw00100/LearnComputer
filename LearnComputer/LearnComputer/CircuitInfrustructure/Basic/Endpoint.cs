using System;

namespace LearnComputer.CircuitInfrustructure
{
    public abstract class Endpoint: IEndpoint
    {
        protected const String INVALID_SIGNAL_EXCEPTION_FORMAT =
            "Signal value can only be either 0 or 1. {0} is not a valid value.";
        public IEndpoint ConnectedPoint { get; protected set; }

        public Endpoint(IEndpoint connectTo = null)
        {
            ConnectTo(connectTo);
        }

        public virtual void ConnectTo(IEndpoint point)
        {
            if (Object.ReferenceEquals(this, ConnectedPoint)) throw new ArgumentException("Endpoint cannot connect to itself.");

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
                IEndpoint point = ConnectedPoint;
                ConnectedPoint = null;
                point.DisconnectEndpoint();
            }
        }
    }
}
