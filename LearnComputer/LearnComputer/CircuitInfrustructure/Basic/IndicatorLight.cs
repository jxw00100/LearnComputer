using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class IndicatorLight
    {
        public IInputEndpoint Input { get; private set; }
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
