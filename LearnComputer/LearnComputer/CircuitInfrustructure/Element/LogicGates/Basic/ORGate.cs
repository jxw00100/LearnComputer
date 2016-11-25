namespace LearnComputer.CircuitInfrustructure
{
    public class ORGate
    {
        private Relay _relay1;
        private Relay _relay2;
        private TShapedNexus _nexus;

        public IInputEndpoint Input1 { get; private set; }
        public IInputEndpoint Input2 { get; private set; }
        public IOutputEndpoint Output { get; private set; }

        public ORGate()
        {
            _relay1 = new Relay();
            _relay2 = new Relay();
            _nexus = new TShapedNexus(_relay1.Output, _relay2.Output);
            
            Input1 = _relay1.Input;
            Input2 = _relay2.Input;
            Output = _nexus.GetEndpointAt(2);
        }
    }
}
