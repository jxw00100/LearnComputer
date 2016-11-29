using System;
using System.Collections.Generic;

namespace LearnComputer.CircuitInfrustructure
{
    public interface IInputEndpointCollection<out T>: IEndpointCollection<T> where T:IInputEndpoint
    {
        void RegisterReceiveHandler(ReceiveSignalHanlder handler);
        void RegisterReceiveHandler(ReceiveSignalHanlder[] handlers);
        IEnumerable<Int32> GetLastReceivedSignals();
        Int32 GetLastReceivedSignalAt(Int32 index);
        void TransmitAll(IEnumerable<Int32> signal = null);
        void TransmitAt(Int32 index, Int32 signal);
        IInputEndpoint this[int index] { get; }
    }
}