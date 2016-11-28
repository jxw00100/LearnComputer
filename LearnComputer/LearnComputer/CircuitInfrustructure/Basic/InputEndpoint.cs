using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class InputEndpoint: Endpoint, IInputEndpoint
    {
        public event ReceiveSignalHanlder Receive = (sender, signal) => { };
        public Int32 LastReceivedSignal { get; private set; }

        public InputEndpoint():base(null)
        {}

        public InputEndpoint(IEndpoint connectTo):base(connectTo)
        {}

        public void Transmit(Int32 signal)
        {
            if (signal != 0 && signal != 1)
                throw new ArgumentException(String.Format(INVALID_SIGNAL_EXCEPTION_FORMAT, signal));
            LastReceivedSignal = signal;
            Receive(ConnectedPoint, signal);
        }

        public override void DisconnectEndpoint()
        {
            var previousEndpoint = ConnectedPoint;
            base.DisconnectEndpoint();
            if (previousEndpoint != null && LastReceivedSignal != 0) Transmit(0);
        }
    }

    
}
