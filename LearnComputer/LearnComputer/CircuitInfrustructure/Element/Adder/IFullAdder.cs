namespace LearnComputer.CircuitInfrustructure
{
    public interface IFullAdder
    {
        IInputEndpoint Number1In { get; }
        IInputEndpoint Number2In { get; }
        IInputEndpoint CarryIn { get; }
        IOutputEndpoint Sum { get; }
        IOutputEndpoint CarryOut { get; }
    }
}