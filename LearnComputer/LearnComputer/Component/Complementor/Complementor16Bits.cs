using System;
using System.Linq;
using LearnComputer.CircuitInfrustructure;

namespace LearnComputer.Component
{
    public class Complementor16Bits : IComplementor
    {
        private const Int32 WIDTH = 16;
        private Complementor8Bits _lowerCplmtor, _upperCplmtor;
        private TShapedNexus _invertNexus;

        public IInputEndpointCollection<IInputEndpoint> Inputs { get; private set; }
        public IInputEndpoint Invert { get; private set; }
        public IOutputEndpointCollection<IOutputEndpoint> Outputs { get; private set; }

        public Complementor16Bits()
        {
            _lowerCplmtor = new Complementor8Bits();
            _upperCplmtor = new Complementor8Bits();
            _invertNexus = new TShapedNexus(null, _lowerCplmtor.Invert, _upperCplmtor.Invert);

            var mergedInputs = _lowerCplmtor.Inputs.Concat(_upperCplmtor.Inputs).Cast<InputEndpoint>();
            var mergedOutputs = _lowerCplmtor.Outputs.Concat(_upperCplmtor.Outputs).Cast<OutputEndpoint>();

            Inputs = new InputEndpointCollection<InputEndpoint>(WIDTH, mergedInputs);
            Outputs = new OutputEndpointCollection<OutputEndpoint>(WIDTH, mergedOutputs);
            Invert = _invertNexus.GetEndpointAt(0);
        }

        public Int32 BitWidth
        {
            get { return WIDTH; }
        }
    }
}
