namespace LearnComputer.CircuitInfrustructure
{
    public class NANDGate
    {
        private Invertor _invertor1;
        private Invertor _invertor2;
        private TShapedNexus _nexus;
        public IInputEndpoint Input1 { get; private set; }
        public IInputEndpoint Input2 { get; private set; }
        public IOutputEndpoint Output { get; private set; }

        public NANDGate()
        {
            _invertor1 = new Invertor();
            _invertor2 = new Invertor();
            _nexus = new TShapedNexus(_invertor1.Output, _invertor2.Output);

            Input1 = _invertor1.Input;
            Input2 = _invertor2.Input;
            Output = _nexus.GetEndpointAt(2);
        }
    }
}
