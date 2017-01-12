using System;
using System.Threading;
using System.Threading.Tasks;

namespace LearnComputer.CircuitInfrustructure
{
    public class Relay
    {
        private Int32 _deplayMilliseconds = 0;
        private Int32 _invertBit = 0;
        private Int32 _initialSendBit = 1;
        public IInputEndpoint Input { get; private set; }
        public IInputEndpoint InputOfContact { get; private set; }
        public IOutputEndpoint Output { get; private set; }

        public Relay(Int32 delayMilliseconds = 0, Boolean invert = false)
        {
            if (delayMilliseconds < 0) throw new ArgumentException("Delay time must be larger than or equal to 0");

            _deplayMilliseconds = delayMilliseconds;
            _invertBit = Convert.ToInt32(invert);
            Input = new InputEndpoint();
            Input.Receive += OnReceive;
            InputOfContact = new InputEndpoint();
            InputOfContact.Receive += OnReceive;
            Output = new OutputEndpoint();

            Input.Transmit(0);  //Relay can send signals from beginning. When being used in invert mode, it plays as power role.
        }

        private void OnReceive(IEndpoint sender, Int32 signal)
        {
            if (_initialSendBit == 0 && _deplayMilliseconds > 0)
            {
                Thread thread = new Thread(DoDelay);
                thread.Start();
                thread.Join();
            }
            else
            {
                DoProduceSignal();
            }
        }

        private void DoDelay()
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(_deplayMilliseconds));
            DoProduceSignal();
        }

        private void DoProduceSignal()
        {
            var inputSignal = Input.LastReceivedSignal;
            var contactInputSignal = GetContactInputSignal();
            Int32 outSignal = (_invertBit ^ inputSignal) & contactInputSignal;

            Output.Produce(outSignal);

            _initialSendBit = 0 & _initialSendBit;
        }

        private Int32 GetContactInputSignal()
        {
            //When unconnected, it will be the power provider of itself.
            return (InputOfContact.ConnectedPoint == null) ?  1 : InputOfContact.LastReceivedSignal;
        }
    }
}
