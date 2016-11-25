namespace LearnComputer.CircuitInfrustructure
{
    public class ANDGate
    {
        private Relay _relay1;
        private Relay _relay2;

        public IInputEndpoint Input1 { get; private set; }
        public IInputEndpoint Input2 { get; private set; }
        public IOutputEndpoint Output { get; private set; }

        public ANDGate()
        {
            _relay1 = new Relay();
            _relay2 = new Relay();
            _relay2.InputOfContact.ConnectTo(_relay1.Output);

            Input1 = _relay1.Input;
            Input2 = _relay2.Input;
            Output = _relay2.Output;
        }
    }
}
