using System;
using System.Collections.Generic;

namespace LearnComputer.CircuitInfrustructure
{
    public interface IOutputEndpointCollection<T>: IEndpointCollection<T> where T: IOutputEndpoint
    {
        IEnumerable<Int32> GetLastSentSignals();
        Int32 GetLastSentSignalAt(Int32 index);
        void Produce(IEnumerable<Int32> signal);
        void ProduceAt(Int32 index, Int32 signal);
        IOutputEndpoint this[int index] { get; }
    }
}