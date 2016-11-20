namespace LearnComputer.CircuitInfrustructure
{
    public class CrossNexus: Nexus
    {
        public CrossNexus(Endpoint connectToPoint1 = null, Endpoint connectToPoint2 = null,
            Endpoint connectToPoint3 = null, Endpoint connEndpoint4 = null)
            : base(4, connectToPoint1, connectToPoint2, connectToPoint3, connEndpoint4)
        {
        }
    }
}
