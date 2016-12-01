using System;
using System.Linq;
using LearnComputer.CircuitInfrustructure;

namespace LearnComputer.Component
{
    public class Complementor8Bits
    {
        private const Int32 WIDTH = 8;
        private XORGate[] _xorGates;
        private Nexus _nexus;

        public IInputEndpointCollection<IInputEndpoint> Inputs { get; private set; }
        public IInputEndpoint Invert { get; private set; }
        public IOutputEndpointCollection<IOutputEndpoint> Outputs { get; private set; }

        public Complementor8Bits()
        {
            InitXORGates();

            Inputs = new InputEndpointCollection<InputEndpoint>(WIDTH, _xorGates.Select(x => x.Input1 as InputEndpoint));
            _nexus = new Nexus(WIDTH + 1, _xorGates.Select(x => x.Input2 as InputEndpoint));
            Invert = _nexus.GetEndpointAt(WIDTH);
            Outputs = new OutputEndpointCollection<OutputEndpoint>(WIDTH,
                _xorGates.Select(x => x.Output as OutputEndpoint));
        }

        private void InitXORGates()
        {
            _xorGates = new XORGate[8];
            for (var i = 0; i < WIDTH; i++)
            {
                _xorGates[i] = new XORGate();
            }
        }    
    }
}
