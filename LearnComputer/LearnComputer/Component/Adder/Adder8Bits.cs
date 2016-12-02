using System;
using System.Linq;
using LearnComputer.CircuitInfrustructure;

namespace LearnComputer.Component
{
    public class Adder8Bits : IAdderMultiBits
    {
        private const Int32 WIDTH = 8;
        private FullAdder[] _fullAdders; 

        public IInputEndpointCollection<IInputEndpoint> Number1Inputs { get; private set; }
        public IInputEndpointCollection<IInputEndpoint> Number2Inputs { get; private set; }
        public IInputEndpoint CarryInput { get; private set; }
        public IOutputEndpointCollection<IOutputEndpoint> SumOutputs { get; private set; }
        public IOutputEndpoint CarryOutput { get; private set; }

        public Adder8Bits()
        {
            InitFullAdders();

            Number1Inputs = new InputEndpointCollection<InputEndpoint>(WIDTH, _fullAdders.Select(a => a.Number1In as InputEndpoint));
            Number2Inputs = new InputEndpointCollection<InputEndpoint>(WIDTH, _fullAdders.Select(a => a.Number2In as InputEndpoint));
            CarryInput = _fullAdders[0].CarryIn;
            SumOutputs = new OutputEndpointCollection<OutputEndpoint>(WIDTH, _fullAdders.Select(a => a.Sum as OutputEndpoint));
            CarryOutput = _fullAdders[WIDTH - 1].CarryOut;
        }

        private void InitFullAdders()
        {
            _fullAdders = new FullAdder[WIDTH];
            for (var i = 0; i < WIDTH; i++)
            {
                _fullAdders[i] = new FullAdder();
                if (i > 0) _fullAdders[i].CarryIn.ConnectTo(_fullAdders[i - 1].CarryOut);
            }
        }

        public Int32 BitWidth
        {
            get { return WIDTH; }
        }
    }
}
