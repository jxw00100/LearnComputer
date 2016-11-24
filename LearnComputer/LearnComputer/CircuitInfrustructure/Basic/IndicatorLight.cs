using System;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Security.Policy;

namespace LearnComputer.CircuitInfrustructure
{
    public class IndicatorLight
    {
        public InputEndpoint Input { get; private set; }
        public Boolean Lighting { get; private set; }

        public IndicatorLight()
        {
            Input = new InputEndpoint();
            Input.Receive += ReceivedSignal;
            Lighting = false;
        }

        public void ReceivedSignal(IEndpoint sender, Int32 signal)
        {
            Lighting = Convert.ToBoolean(signal);
        }
    }
}
