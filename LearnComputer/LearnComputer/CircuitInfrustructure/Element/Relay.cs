using System;
using System.Threading;

namespace LearnComputer.CircuitInfrustructure
{
    public class Relay
    {
        private Int32 _deplayMilliseconds = 0;
        private Byte _invertBit = 0;
        private Byte _initialSendBit = 1;
        public InputEndpoint Input { get; private set; }
        public OutputEndpoint Output { get; private set; }

        public Relay(Int32 delayMilliseconds = 0, Boolean invert = false)
        {
            _deplayMilliseconds = delayMilliseconds;
            _invertBit = (Byte)(invert ? 1 : 0);
            Input = new InputEndpoint();
            Input.Receive += OnReceive;
            Output = new OutputEndpoint();

            Input.Transmit(0);  //Relay can send signals from beginning. When being used in invert mode, it plays as power role.
        }

        private void OnReceive(Endpoint sender, Byte signal)
        {
            if (_initialSendBit == 0 && _deplayMilliseconds > 0) Thread.Sleep(_deplayMilliseconds);
            Byte outSignal = (Byte) (_invertBit ^ signal);
            Output.Produce(outSignal);
            _initialSendBit = (Byte) (0 & _initialSendBit);
        }
    }
}
