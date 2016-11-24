using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class NeutralEndpoint: InputEndpoint, INeutralEndpoint
    {
        public NeutralEndpoint():base(null)
        {}

        public NeutralEndpoint(IEndpoint connectTo)
            : base(connectTo)
        {}

        public Int32 LastSentSignal { get; private set; }

        public void Produce(Int32 signal)
        {
            if (signal != 0 && signal != 1)
                throw new ArgumentException(String.Format(INVALID_SIGNAL_EXCEPTION_FORMAT, signal));
            LastSentSignal = signal;

            var connectedPoint = ConnectedPoint as IInputEndpoint;
            if (connectedPoint != null)
            {
                connectedPoint.Transmit(signal);
            }
        }

        public override void ConnectTo(IEndpoint point)
        {
            base.ConnectTo(point);
            Produce(LastSentSignal);
        }
    }
}
