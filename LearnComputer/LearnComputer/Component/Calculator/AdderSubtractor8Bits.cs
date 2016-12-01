using LearnComputer.CircuitInfrustructure;

namespace LearnComputer.Component
{
    public class AdderSubtractor8Bits : IAdderSubtractor8Bits
    {
        private Complementor8Bits _cplmtor;
        private Adder8Bits _adder;
        private XORGate _xorGate;
        private CrossNexus _subNexus;

        public IInputEndpointCollection<IInputEndpoint> Number1Inputs { get; private set; }
        public IInputEndpointCollection<IInputEndpoint> Number2Inputs { get; private set; }
        public IInputEndpoint TurnSubstract { get; private set; }
        public IOutputEndpointCollection<IOutputEndpoint> ResultOutputs { get; private set; }
        public IOutputEndpoint OverflowUnderflow { get; private set; }

        public AdderSubtractor8Bits()
        {
            InitBasicElements();

            Number1Inputs = _adder.Number1Inputs;
            Number2Inputs = _cplmtor.Inputs;
            TurnSubstract = _subNexus.GetEndpointAt(0);
            ResultOutputs = _adder.SumOutputs;
            OverflowUnderflow = _xorGate.Output;
        }

        private void InitBasicElements()
        {
            _cplmtor = new Complementor8Bits();
            _adder = new Adder8Bits();
            _xorGate = new XORGate();
            _subNexus = new CrossNexus(null, _cplmtor.Invert, _adder.CarryInput, _xorGate.Input1);

            _cplmtor.Outputs.Connect(_adder.Number2Inputs);
            _adder.CarryOutput.ConnectTo(_xorGate.Input2);
        }
    }
}
