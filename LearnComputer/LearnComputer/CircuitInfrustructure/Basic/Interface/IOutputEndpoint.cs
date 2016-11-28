using System;

namespace LearnComputer.CircuitInfrustructure
{
    public interface IOutputEndpoint : IEndpoint
    {
        Int32 LastSentSignal { get; }
        void Produce(Int32 signal);
    }
}
