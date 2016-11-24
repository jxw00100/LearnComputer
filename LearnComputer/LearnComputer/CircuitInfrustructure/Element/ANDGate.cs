using System;
using System.Threading;

namespace LearnComputer.CircuitInfrustructure.Element
{
    public class ANDGate
    {
        private Relay _relay1 = new Relay();
        private Relay _relay2 = new Relay();

        public InputEndpoint Input1 { get; private set; }
        public InputEndpoint Input2 { get; private set; }
        public OutputEndpoint Output { get; private set; }

        public ANDGate()
        {
            Input1 = new InputEndpoint();
            Input2 = new InputEndpoint();
            Output = new OutputEndpoint();
        }

    }
}
