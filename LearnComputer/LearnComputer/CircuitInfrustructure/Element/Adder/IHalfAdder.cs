namespace LearnComputer.CircuitInfrustructure
{
    public interface IHalfAdder
    {
        IInputEndpoint Number1In { get; }
        IInputEndpoint Number2In { get; }
        IOutputEndpoint Sum { get; }
        IOutputEndpoint CarryOut { get; }
    }
}