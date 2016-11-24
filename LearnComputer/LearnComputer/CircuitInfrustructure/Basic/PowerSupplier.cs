using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class PowerSupplier
    {
        public IOutputEndpoint Output { get; private set; }
        private Boolean _switchOn = false;

        public PowerSupplier()
        {
            Output = new OutputEndpoint();
            _switchOn = false;
        }
        
        public void Toggle()
        {
            _switchOn = !_switchOn;
            Output.Produce(Convert.ToInt32(_switchOn));
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
