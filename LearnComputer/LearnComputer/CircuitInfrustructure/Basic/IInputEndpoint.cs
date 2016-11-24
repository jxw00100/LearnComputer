using System;

namespace LearnComputer.CircuitInfrustructure
{
    public interface IInputEndpoint
    {
        Int32 LastReceivedSignal { get; }
        void Transmit(Int32 signal);
    }
}
