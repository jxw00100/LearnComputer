namespace LearnComputer.CircuitInfrustructure
{
    public interface INeutralEndpointCollection<out T> : IInputEndpointCollection<T>, IOutputEndpointCollection<T> where T: INeutralEndpoint
    {
        INeutralEndpoint this[int index] { get; }
    }
}
