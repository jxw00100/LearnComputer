namespace LearnComputer.CircuitInfrustructure
{
    public interface INeutralEndpointCollection<T> : IInputEndpointCollection<T>, IOutputEndpointCollection<T> where T: INeutralEndpoint
    {
        INeutralEndpoint this[int index] { get; }
    }
}
