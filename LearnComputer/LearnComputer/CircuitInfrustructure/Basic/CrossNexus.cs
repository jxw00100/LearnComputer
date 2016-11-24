namespace LearnComputer.CircuitInfrustructure
{
    public class CrossNexus: Nexus
    {
        public CrossNexus(IEndpoint connectToPoint1 = null, IEndpoint connectToPoint2 = null,
            IEndpoint connectToPoint3 = null, IEndpoint connEndpoint4 = null)
            : base(4, connectToPoint1, connectToPoint2, connectToPoint3, connEndpoint4)
        {
        }
    }
}
