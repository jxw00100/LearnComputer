using LearnComputer.CircuitInfrustructure;

namespace LearnComputer.Component
{
    public class FlipFlop
    {
        private NORGate _norGate1, _norGate2;
        private TShapedNexus _nexus;

        public IInputEndpoint Set { get; private set; }
        public IInputEndpoint Clear { get; private set; }
        public IOutputEndpoint Output { get; private set; }

        public FlipFlop()
        {
            _norGate1 = new NORGate();
            _norGate2 = new NORGate();
            _nexus = new TShapedNexus(_norGate2.Output, _norGate1.Input1);

            _norGate1.Output.ConnectTo(_norGate2.Input1);

            Set = _norGate1.Input2;
            Clear = _norGate2.Input2;
            Output = _nexus.GetEndpointAt(2);
        }
    }
}
