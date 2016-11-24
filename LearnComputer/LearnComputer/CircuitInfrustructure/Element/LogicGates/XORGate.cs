namespace LearnComputer.CircuitInfrustructure.Element.LogicGates
{
    public class XORGate
    {
        private ORGate _orGate;
        private NANDGate _nandGate;
        private ANDGate _andGate;
        private TShapedNexus _nexus1, _nexus2;

        public IInputEndpoint Input1 { get; private set; }
        public IInputEndpoint Input2 { get; private set; }
        public IOutputEndpoint Output { get; private set; }

        public XORGate()
        {
            _orGate = new ORGate();
            _nandGate = new NANDGate();
            _andGate = new ANDGate();
            _nexus1 = new TShapedNexus(null, _orGate.Input1, _nandGate.Input1);
            _nexus2 = new TShapedNexus(null, _orGate.Input2, _nandGate.Input2);
            
            _andGate.Input1.ConnectTo(_orGate.Output);
            _andGate.Input2.ConnectTo(_nandGate.Output);

            Input1 = _nexus1.GetEndpointAt(0);
            Input2 = _nexus2.GetEndpointAt(0);
            Output = _andGate.Output;
        }

    }
}
