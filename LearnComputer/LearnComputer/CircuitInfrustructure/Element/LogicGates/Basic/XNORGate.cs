namespace LearnComputer.CircuitInfrustructure
{
    public class XNORGate
    {
        private ORGate _orGate;
        private NORGate _norGate;
        private ANDGate _andGate;
        private TShapedNexus _nexus1, _nexus2;

        public IInputEndpoint Input1 { get; private set; }
        public IInputEndpoint Input2 { get; private set; }
        public IOutputEndpoint Output { get; private set; }


        public XNORGate()
        {
            _orGate = new ORGate();
            _norGate = new NORGate();
            _andGate = new ANDGate();
            _nexus1 = new TShapedNexus(null, _andGate.Input1, _norGate.Input1);
            _nexus2 = new TShapedNexus(null, _andGate.Input2, _norGate.Input2);

            _orGate.Input1.ConnectTo(_andGate.Output);
            _orGate.Input2.ConnectTo(_norGate.Output);

            Input1 = _nexus1.GetEndpointAt(0);
            Input2 = _nexus2.GetEndpointAt(0);
            Output = _orGate.Output;
        }

    }
}
