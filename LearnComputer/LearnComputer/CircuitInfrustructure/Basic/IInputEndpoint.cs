using System;

namespace LearnComputer.CircuitInfrustructure
{
    public interface IInputEndpoint
    {
        Byte LastReceivedSignal { get; }
        void Transmit(byte signal);
    }
}
