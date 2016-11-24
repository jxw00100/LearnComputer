namespace LearnComputer.CircuitInfrustructure.Element
{
    public class Decoder2To4
    {
        TShapedNexus _nexusUp1, _nexusUp2, _nexusDown1, _nexusDown2;
        CrossNexus _crossNexus;
        Invertor _invertorUp, _invertorDown;
        ANDGate _andGate00, _andGate01, _andGate10, _andGate11;

        public IInputEndpoint Input1 { get; private set; }
        public IInputEndpoint Input2 { get; private set; }
        public IOutputEndpoint Output00 { get; private set; }
        public IOutputEndpoint Output01 { get; private set; }
        public IOutputEndpoint Output10 { get; private set; }
        public IOutputEndpoint Output11 { get; private set; }

        public Decoder2To4()
        {
            _invertorUp = new Invertor();
            _invertorDown = new Invertor();
            _andGate00 = new ANDGate();
            _andGate01 = new ANDGate();
            _andGate10 = new ANDGate();
            _andGate11 = new ANDGate();

            _crossNexus = new CrossNexus(null, _andGate10.Input1, _invertorUp.Input, _andGate11.Input1);
            _nexusUp1 = new TShapedNexus(_invertorUp.Output, _andGate00.Input1, _andGate01.Input1);
            _nexusUp2 = new TShapedNexus(_invertorDown.Output, _andGate00.Input2, _andGate10.Input1);
            _nexusDown1 = new TShapedNexus(null, _invertorDown.Input, null);
            _nexusDown2 = new TShapedNexus(_nexusDown1.GetEndpointAt(2), _andGate01.Input2, _andGate11.Input2);

            Output00 = _andGate00.Output;
            Output01 = _andGate01.Output;
            Output10 = _andGate10.Output;
            Output11 = _andGate11.Output;
        }

    }
}
