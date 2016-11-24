namespace LearnComputer.CircuitInfrustructure
{
    public class NORGate
    {
        private ORGate _orGate;
        private Invertor _invertor;

        public IInputEndpoint Input1 { get; private set; }
        public IInputEndpoint Input2 { get; private set; }
        public IOutputEndpoint Output { get; private set; }

        public NORGate()
        {
            _orGate = new ORGate();
            _invertor = new Invertor();

            _orGate.Output.ConnectTo(_invertor.Input);

            Input1 = _orGate.Input1;
            Input2 = _orGate.Input2;
            Output = _invertor.Output;
        }
    }
}
