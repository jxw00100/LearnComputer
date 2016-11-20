using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class InputEndpoint: Endpoint, IInputEndpoint
    {
        public delegate void ReceiveSignalHanlder(Endpoint sender, Byte signal);

        public event ReceiveSignalHanlder Receive = (sender, signal) => { };
        public Byte LastReceivedSignal { get; private set; }

        public InputEndpoint():base(null)
        {}

        public InputEndpoint(Endpoint connectTo):base(connectTo)
        {}

        public void Transmit(Byte signal)
        {
            if (signal != 0 && signal != 1)
                throw new ArgumentException(String.Format(INVALID_SIGNAL_EXCEPTION_FORMAT, signal));
            LastReceivedSignal = signal;
            Receive(ConnectedPoint, signal);
        }

        public void DisconnectEndpoint()
        {
            base.DisconnectEndpoint();
            Transmit(0);
        }
    }
}
