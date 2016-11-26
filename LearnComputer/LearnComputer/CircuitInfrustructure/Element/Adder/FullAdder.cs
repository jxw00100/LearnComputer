namespace LearnComputer.CircuitInfrustructure
{
    public class FullAdder : IFullAdder
    {
        private ORGate _orGate;
        private IHalfAdder _halfAdderOrigin, _halfAdderCarry;

        public IInputEndpoint Number1In { get; private set; }
        public IInputEndpoint Number2In { get; private set; }
        public IInputEndpoint CarryIn { get; private set; }
        public IOutputEndpoint Sum { get; private set; }
        public IOutputEndpoint CarryOut { get; private set; }

        public FullAdder()
        {
            _orGate = new ORGate();
            _halfAdderOrigin = new HalfAdder();
            _halfAdderCarry = new HalfAdder();

            _halfAdderCarry.CarryOut.ConnectTo(_orGate.Input1);
            _halfAdderOrigin.Sum.ConnectTo(_halfAdderCarry.Number2In);
            _halfAdderOrigin.CarryOut.ConnectTo(_orGate.Input2);

            CarryIn = _halfAdderCarry.Number1In;
            Number1In = _halfAdderOrigin.Number1In;
            Number2In = _halfAdderOrigin.Number2In;
            Sum = _halfAdderCarry.Sum;
            CarryOut = _orGate.Output;
        }
    }
}
