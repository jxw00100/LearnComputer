using System;
using System.ComponentModel;

namespace LearnComputer.CircuitInfrustructure
{
    public class Switches8Bits
    {
        private const Int32 WIDTH = 8;
        private Boolean[] _switches;
        public IOutputEndpoint[] Outputs { get; private set; }
        private Boolean[] _defaults;

        public Switches8Bits(params Boolean[] switchesOn)
        {
            _switches = new Boolean[WIDTH];
            _defaults = new Boolean[WIDTH];
            InitializeOutputs();
            
            Set(switchesOn);
        }

        private void InitializeOutputs()
        {
            Outputs = new IOutputEndpoint[WIDTH];

            for (var i = 0; i < WIDTH; i++)
            {
                Outputs[i] = new OutputEndpoint();
            }
        }

        public void Set(params Boolean[] switchesOn)
        {
            if (switchesOn != null)
            {
                for (var i = 0; i < WIDTH && i < switchesOn.Length; i++)
                {
                    _switches[i] = switchesOn[i];
                }
            }
            DispatchSignal();
        }

        private void DispatchSignal()
        {
            for (var i = 0; i < WIDTH; i++)
            {
                Outputs[i].Produce(Convert.ToInt32(_switches[i]));
            }
        }

        public void Connect(params IEndpoint[] points)
        {
            for (var i = 0; i < WIDTH && i < points.Length; i++)
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