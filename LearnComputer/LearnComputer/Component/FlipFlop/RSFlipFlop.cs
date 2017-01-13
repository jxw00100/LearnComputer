using LearnComputer.CircuitInfrustructure;

namespace LearnComputer.Component
{
    public class RSFlipFlop
    {
        private NORGate _norGate1, _norGate2;
        private TShapedNexus _nexus1, _nexus2;

        public IInputEndpoint Set { get; private set; }
        public IInputEndpoint Reset { get; private set; }
        public IOutputEndpoint Q { get; private set; }
        public IOutputEndpoint QBar { get; private set; }

        public RSFlipFlop()
        {
            _norGate1 = new NORGate();
            _norGate2 = new NORGate();
            _nexus1 = new TShapedNexus(_norGate1.Output, _norGate2.Input1);
            _nexus2 = new TShapedNexus(_norGate2.Output, _norGate1.Input1);

            Reset = _norGate1.Input2;
            Set = _norGate2.Input2;
            Q = _nexus1.GetEndpointAt(2);
            QBar = _nexus2.GetEndpointAt(2);
        }
    }
}
