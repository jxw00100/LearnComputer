using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class PowerSupplier
    {
        public OutputEndpoint Output { get; private set; }
        private Boolean _switchOn = false;

        public PowerSupplier()
        {
            Output = new OutputEndpoint();
            _switchOn = false;
        }
        
        public void Toggle()
        {
            _switchOn = !_switchOn;
            Output.Produce((Byte)(_switchOn ? 1 : 0));
        }

        public void On()
        {
            if(!_switchOn) Toggle();
        }

        public void Off()
        {
            if (_switchOn) Toggle();
        }
    }
}
