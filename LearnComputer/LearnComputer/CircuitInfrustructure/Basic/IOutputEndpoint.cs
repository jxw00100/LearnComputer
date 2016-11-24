using System;

namespace LearnComputer.CircuitInfrustructure
{
    public interface IOutputEndpoint
    {
        Byte LastSentSignal { get; }
        void Produce(Byte signal);
    }
}
