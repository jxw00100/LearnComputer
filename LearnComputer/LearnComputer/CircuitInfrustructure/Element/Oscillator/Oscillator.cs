using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class Oscillator : IOscillator
    {
        private readonly Int32 _interval;

        private ANDGate _andGate;
        private Relay _delayInvertor;
        private TShapedNexus _asyncNexus;

        public IInputEndpoint Start { get; private set; }
        public IOutputEndpoint Output { get; private set; }
        public Int32 Interval {
            get { return _interval; }
        }

        public Oscillator(Int32 interval = 100)
        {
            _interval = interval;
            _andGate = new ANDGate();
            _delayInvertor = new Relay(_interval, true);
            _asyncNexus = new TShapedNexus(_delayInvertor.Output, _andGate.Input2) {AsyncDispatch = true};

            _andGate.Output.ConnectTo(_delayInvertor.Input);

            Start = _andGate.Input1;
            Output = _asyncNexus.GetEndpointAt(2);
        }
    }
}
