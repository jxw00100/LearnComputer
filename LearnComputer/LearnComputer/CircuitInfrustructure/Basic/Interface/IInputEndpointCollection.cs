using System;
using System.Collections.Generic;

namespace LearnComputer.CircuitInfrustructure
{
    public interface IInputEndpointCollection<T>: IEndpointCollection<T> where T:IInputEndpoint
    {
        void RegisterReceiveHandler(ReceiveSignalHanlder handler);
        IEnumerable<Int32> GetLastReceivedSignals();
        Int32 GetLastReceivedSignalAt(Int32 index);
        void Transmit(IEnumerable<Int32> signal);
        void TransmitAt(Int32 index, Int32 signal);
        IInputEndpoint this[int index] { get; }
    }
}