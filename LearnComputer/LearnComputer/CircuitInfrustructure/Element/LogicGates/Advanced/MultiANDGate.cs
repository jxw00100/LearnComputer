using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class MultiANDGate
    {
        private Relay[] _relays;
        public IInputEndpoint[] Inputs { get; private set; }
        public IOutputEndpoint Output { get; private set; }

        public MultiANDGate(Int32 inputCount)
        {
            if(inputCount < 2) throw new ArgumentException("Input ports must be 2 or more.");

            _relays = new Relay[inputCount];
            Inputs = new IInputEndpoint[inputCount];

            for(var i= 0; i<inputCount; i++)
            {
                _relays[i] = new Relay();
                Inputs[i] = _relays[i].Input;
                if (i > 0)
                {
                    _relays[i].InputOfContact.ConnectTo(_relays[i-1].Output);
                }
            }
            Output = _relays[inputCount - 1].Output;
        }

        public void ConnectInputsWith(params IEndpoint[] connectPoints)
        {
            var pointsCount = connectPoints.Length;
            if (pointsCount > Inputs.Length) throw new ArgumentException("The connected points are too many.");
            for (var i = 0; i < pointsCount; i++)
            {
                Inputs[i].ConnectTo(connectPoints[i]);
            }
        }
    }
}
