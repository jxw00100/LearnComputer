using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class Oscillator : IOscillator
    {
        private readonly Int32 _interval;

        private ANDGate _andGate;
        private Relay _delayInvertor;
        private TShapedNexus _nexus;

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
            _nexus = new TShapedNexus(_delayInvertor.Output, _andGate.Input2);

            _andGate.Output.ConnectTo(_delayInvertor.Input);

            Start = _andGate.Input1;
            Output = _nexus.GetEndpointAt(2);

            _andGate.Input2.Receive +=
                (s, ss) =>
                {
                    var sss = ss;
                };

            _nexus.GetEndpointAt(0).Receive += (s, ss) =>
            {
                var sss = ss;
            };

            _delayInvertor.Input.Receive +=
            (s, ss) =>
            {
                var sss = ss;
            };

            

        }


    }
}
