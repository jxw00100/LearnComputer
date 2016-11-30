using System.Collections.Generic;

namespace LearnComputer.CircuitInfrustructure
{
    public class TShapedNexus : Nexus
    {
        public TShapedNexus(IEndpoint connectToPoint1 = null, IEndpoint connectToPoint2 = null,
            IEndpoint connectToPoint3 = null)
            : base(3, connectToPoint1, connectToPoint2, connectToPoint3)
        {}

        public TShapedNexus(IEnumerable<IEndpoint> connectToPoints)
            : base(3, connectToPoints)
        { }
    }
}
