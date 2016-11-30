using System;
using System.Linq;
using LearnComputer.CircuitInfrustructure;

namespace LearnComputer.Component
{
    public class Adder16Bits : IAdderMultiBits
    {
        private const Int32 WIDTH = 16;
        private Adder8Bits _lowerAdder, _upperAdder;

        public IInputEndpointCollection<IInputEndpoint> Number1Inputs { get; private set; }
        public IInputEndpointCollection<IInputEndpoint> Number2Inputs { get; private set; }
        public IInputEndpoint CarryInput { get; private set; }
        public IOutputEndpointCollection<IOutputEndpoint> SumOutputs { get; private set; }
        public IOutputEndpoint CarryOutput { get; private set; }

        public Adder16Bits()
        {
            _lowerAdder = new Adder8Bits();
            _upperAdder = new Adder8Bits();
            _lowerAdder.CarryOutput.ConnectTo(_upperAdder.CarryInput);

            var inputpointsNumber1 = _lowerAdder.Number1Inputs.Concat(_upperAdder.Number1Inputs).Select(i => i as InputEndpoint);
            var inputpointsNumber2 = _lowerAdder.Number2Inputs.Concat(_upperAdder.Number2Inputs).Select(i => i as InputEndpoint);
            var outputsSum = _lowerAdder.SumOutputs.Concat(_upperAdder.SumOutputs).Select(i => i as OutputEndpoint);

            Number1Inputs = new InputEndpointCollection<InputEndpoint>(WIDTH, inputpointsNumber1);
            Number2Inputs = new InputEndpointCollection<InputEndpoint>(WIDTH, inputpointsNumber2);
            CarryInput = _lowerAdder.CarryInput;
            SumOutputs = new OutputEndpointCollection<OutputEndpoint>(WIDTH, outputsSum);
            CarryOutput = _upperAdder.CarryOutput;
        }
    }
}
