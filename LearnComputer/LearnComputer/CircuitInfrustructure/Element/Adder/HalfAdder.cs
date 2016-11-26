namespace LearnComputer.CircuitInfrustructure
{
    public class HalfAdder : IHalfAdder
    {
        private ANDGate _andGate;
        private XORGate _xorGate;
        private TShapedNexus _nexus1, _nexus2;
        
        public IInputEndpoint Number1In { get; private set; }
        public IInputEndpoint Number2In { get; private set; }
        public IOutputEndpoint Sum { get; private set; }
        public IOutputEndpoint CarryOut { get; private set; }
        
        public HalfAdder()
        {
            _andGate = new ANDGate();
            _xorGate = new XORGate();
            _nexus1 = new TShapedNexus(null, _andGate.Input1, _xorGate.Input1);
            _nexus2 = new TShapedNexus(null, _andGate.Input2, _xorGate.Input2);

            Number1In = _nexus1.GetEndpointAt(0);
            Number2In = _nexus2.GetEndpointAt(0);
            Sum = _xorGate.Output;
            CarryOut = _andGate.Output;
        }
    }
}
