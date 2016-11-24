using System;

namespace LearnComputer.CircuitInfrustructure
{
    public interface IInputEndpoint : IEndpoint
    {
        event ReceiveSignalHanlder Receive;
        Int32 LastReceivedSignal { get; }
        void Transmit(Int32 signal);
    }
}
