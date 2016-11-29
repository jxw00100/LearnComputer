using System;
using System.Collections.Generic;

namespace LearnComputer.CircuitInfrustructure
{
    public interface IOutputEndpointCollection<out T> : IEndpointCollection<T> where T : IOutputEndpoint
    {
        IEnumerable<Int32> GetLastSentSignals();
        Int32 GetLastSentSignalAt(Int32 index);
        void ProduceAll(IEnumerable<Int32> signals = null);
        void ProduceAt(Int32 index, Int32 signal);
        IOutputEndpoint this[Int32 index] { get; }
    }
}