using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnComputer.CircuitInfrustructure
{
    public class NeutralEndpointCollection<T>: InputEndpointCollection<T>, INeutralEndpointCollection<T> where T: INeutralEndpoint, new()
    {
        public NeutralEndpointCollection(Int32 width, IEnumerable<T> endpoints = null)
            : base(width, endpoints)
        {
        }

        public IEnumerable<Int32> GetLastSentSignals()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return this[i].LastSentSignal;
            }
        }

        public Int32 GetLastSentSignalAt(Int32 index)
        {
            return this[index].LastSentSignal;
        }

        public void ProduceAll(IEnumerable<Int32> signals = null)
        {
            var signalsCount = (signals == null) ? 0 : signals.Count();

            for (var i = 0; i < Count; i++)
            {
                if (i < signalsCount)
                {
                    this[i].Produce(signals.ElementAt(i));
                }
                else
                {
                    this[i].Produce(0);
                }
            }
        }

        public void ProduceAt(Int32 index, Int32 signal)
        {
            this[index].Produce(signal);
        }

        IOutputEndpoint IOutputEndpointCollection<T>.this[Int32 index]
        {
            get { return base[index] as IOutputEndpoint; }
        }

        public INeutralEndpoint this[Int32 index]
        {
            get { return base[index] as INeutralEndpoint; } 
        }
    }
}
