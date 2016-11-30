using System;
using System.Collections.Generic;
using System.Linq;

namespace LearnComputer.CircuitInfrustructure
{
    public class InputEndpointCollection<T> : EndpointCollection<T>, IInputEndpointCollection<T> where T : IInputEndpoint, new()
    {
        public InputEndpointCollection(Int32 width, IEnumerable<T> endpoints = null)
            : base(width, endpoints)
        {
        }

        public void RegisterReceiveHandler(ReceiveSignalHanlder handler)
        {
            for (var i = 0; i < Count; i++)
            {
                this[i].Receive += handler;
            }
        }

        public void RegisterReceiveHandler(ReceiveSignalHanlder[] handlers)
        {
            if(handlers.Length != this.Count) throw new ArgumentException("Receive signal handlers count are not match to endpoints count.");

            for (var i = 0; i < Count; i++)
            {
                this[i].Receive += handlers[i];
            }
        }

        public IEnumerable<Int32> GetLastReceivedSignals()
        {
            for (var i = 0; i < Count; i++)
            {
                yield return this[i].LastReceivedSignal;
            }
        }

        public Int32 GetLastReceivedSignalAt(Int32 index)
        {
            return this[index].LastReceivedSignal;
        }

        public void TransmitAll(IEnumerable<Int32> signals = null)
        {
            var signalsCount = (signals == null) ? 0: signals.Count();

            for (var i = 0; i < Count; i++)
            {
                if (i < signalsCount)
                {
                    this[i].Transmit(signals.ElementAt(i));
                }
                else
                {
                    this[i].Transmit(0);
                }
            }
        }

        public void TransmitAt(Int32 index, Int32 signal)
        {
            this[index].Transmit(signal);
        }

        public IInputEndpoint this[Int32 index]
        {
            get { return base[index] as IInputEndpoint; }
        }
    }
}
