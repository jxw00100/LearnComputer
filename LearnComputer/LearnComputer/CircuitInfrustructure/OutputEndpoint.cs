﻿using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class OutputEndpoint: Endpoint, IOutputEndpoint
    {
        public OutputEndpoint():base(null)
        {}

        public OutputEndpoint(Endpoint connectTo)
            : base(connectTo)
        {}

        public byte LastSentSignal { get; private set; }

        public void Produce(Byte signal)
        {
            if (signal != 0 && signal != 1)
                throw new ArgumentException(String.Format(INVALID_SIGNAL_EXCEPTION_FORMAT, signal));
            LastSentSignal = signal;

            var connectedPoint = ConnectedPoint as IInputEndpoint;
            if (connectedPoint != null)
            {
                connectedPoint.Transmit(signal);
            }
        }
    }
}
