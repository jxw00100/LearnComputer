namespace LearnComputer.CircuitInfrustructure
{
    public class TShapedNexus : Nexus
    {
        public TShapedNexus(Endpoint connectToPoint1 = null, Endpoint connectToPoint2 = null,
            Endpoint connectToPoint3 = null)
            : base(3, connectToPoint1, connectToPoint2, connectToPoint3)
        {}
    }
}
