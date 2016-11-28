using System;
using System.Collections.Generic;

namespace LearnComputer.CircuitInfrustructure
{
    public interface IEndpointCollection<out T> : IReadOnlyCollection<T> where T : IEndpoint
    {
        void Connect<TE>(IEndpointCollection<TE> connectToPoints) where TE : IEndpoint;
        void ConnectAt(Int32 index, IEndpoint endpoint);
        void DisconnectAll();
        void DisconnectAt(Int32 index);
        T this[int index] { get; }
    }
}