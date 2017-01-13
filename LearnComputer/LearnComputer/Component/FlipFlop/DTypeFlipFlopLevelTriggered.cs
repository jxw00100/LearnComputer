using LearnComputer.CircuitInfrustructure;

namespace LearnComputer.Component
{
    public class DTypeFlipFlopLevelTriggered : IDTypeFlipFlop
    {
        private RSFlipFlop _RSflipFlop;
        private ANDGate _andGate1, _andGate2;
        private TShapedNexus _nexus1, _nexus2;
        private Invertor _invertor;

        public IInputEndpoint Data { get; private set; }
        public IInputEndpoint Clock { get; private set; }
        public IOutputEndpoint Q { get; private set; }
        public IOutputEndpoint QBar { get; private set; }

        public DTypeFlipFlopLevelTriggered()
        {
            _RSflipFlop = new RSFlipFlop();
            _andGate1 = new ANDGate();
            _andGate2 = new ANDGate();
            _invertor = new Invertor();
            _nexus1 = new TShapedNexus(_andGate1.Input1, _andGate2.Input1);
            _nexus2 = new TShapedNexus(_invertor.Input, _andGate2.Input2);

            _invertor.Output.ConnectTo(_andGate1.Input2);
            _andGate1.Output.ConnectTo(_RSflipFlop.Reset);
            _andGate2.Output.ConnectTo(_RSflipFlop.Set);

            Data = _nexus2.GetEndpointAt(2);
            Clock = _nexus1.GetEndpointAt(2);
            Q = _RSflipFlop.Q;
            QBar = _RSflipFlop.QBar;
        }
    }
}
