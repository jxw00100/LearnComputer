using System;
using LearnComputer.CircuitInfrustructure;

namespace LearnComputer.Component
{
    public class Decoder2To4
    {
        TShapedNexus _tshapeNexus1, _tshapeNexus2;
        CrossNexus _crossNexus1, _crossNexus2;
        Invertor _invertor1, _invertor2;
        ANDGate _andGate00, _andGate01, _andGate10, _andGate11;

        public IInputEndpoint Input1 { get; private set; }
        public IInputEndpoint Input2 { get; private set; }
        public IOutputEndpoint Output00 { get; private set; }
        public IOutputEndpoint Output01 { get; private set; }
        public IOutputEndpoint Output10 { get; private set; }
        public IOutputEndpoint Output11 { get; private set; }

        public Decoder2To4()
        {
            _invertor1 = new Invertor();
            _invertor2 = new Invertor();
            _andGate00 = new ANDGate();
            _andGate01 = new ANDGate();
            _andGate10 = new ANDGate();
            _andGate11 = new ANDGate();

            _crossNexus1 = new CrossNexus(null, _andGate10.Input1, _invertor1.Input, _andGate11.Input1);
            _crossNexus2 = new CrossNexus(null, _andGate01.Input2, _invertor2.Input, _andGate11.Input2);
            _tshapeNexus1 = new TShapedNexus(_invertor1.Output, _andGate00.Input1, _andGate01.Input1);
            _tshapeNexus2 = new TShapedNexus(_invertor2.Output, _andGate00.Input2, _andGate10.Input2);

            Input1 = _crossNexus1.GetEndpointAt(0);
            Input2 = _crossNexus2.GetEndpointAt(0);
            Output00 = _andGate00.Output;
            Output01 = _andGate01.Output;
            Output10 = _andGate10.Output;
            Output11 = _andGate11.Output;
        }
    }
}
