using System;
using System.ComponentModel;

namespace LearnComputer.CircuitInfrustructure
{
    public class Switches
    {
        private Int32 _width;
        private Boolean[] _switches;
        public IOutputEndpointCollection<IOutputEndpoint> Outputs { get; private set; }
        private Boolean[] _defaults;

        public Switches(Int32 width, params Boolean[] switchesOn)
        {
            _width = width;
            _switches = new Boolean[_width];
            _defaults = new Boolean[_width];
            Outputs = new OutputEndpointCollection<OutputEndpoint>(_width);
            
            Set(switchesOn);
        }

        public void Set(params Boolean[] switchesOn)
        {
            if (switchesOn != null)
            {
                for (var i = 0; i < _width && i < switchesOn.Length; i++)
                {
                    _switches[i] = switchesOn[i];
                }
            }
            DispatchSignal();
        }

        private void DispatchSignal()
        {
            for (var i = 0; i < _width; i++)
            {
                Outputs[i].Produce(Convert.ToInt32(_switches[i]));
            }
        }

        public void Connect(params IEndpoint[] points)
        {
            for (var i = 0; i < _width && i < points.Length; i++)
            {
                Outputs[i].ConnectTo(points[i]);
            }
        }

        public void Clear()
        {
            Set(_defaults);
        }
    }
}