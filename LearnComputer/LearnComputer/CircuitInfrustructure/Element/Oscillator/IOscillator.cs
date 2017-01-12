using System;

namespace LearnComputer.CircuitInfrustructure
{
    public interface IOscillator
    {
        IInputEndpoint Start { get; }
        IOutputEndpoint Output { get; }
        Int32 Interval { get; }
    }
}