using System;

namespace LearnComputer.CircuitInfrustructure
{
    public class MultiORGate
    {
        private Relay[] _relays;
        private Nexus _nexus;
        public IInputEndpoint[] Inputs { get; private set; }
        public IOutputEndpoint Output { get; private set; }

        public MultiORGate(Int32 inputCount)
        {
            if(inputCount < 2) throw  new ArgumentException("Input ports must be 2 or more.");

            _relays = new Relay[inputCount];
            _nexus = new Nexus(inputCount + 1);
            Inputs = new IInputEndpoint[inputCount];

            for(var i= 0; i<inputCount; i++)
            {
                _relays[i] = new Relay();
                Inputs[i] = _relays[i].Input;
                _nexus.ConnectAt(_relays[i].Output, i);
            }
            Output = _nexus.GetEndpointAt(inputCount);
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
