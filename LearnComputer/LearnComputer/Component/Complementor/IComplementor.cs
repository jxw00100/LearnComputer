using LearnComputer.CircuitInfrustructure;

namespace LearnComputer.Component
{
    public interface IComplementor: IBitWidthDescriber
    {
        IInputEndpointCollection<IInputEndpoint> Inputs { get; }
        IInputEndpoint Invert { get; }
        IOutputEndpointCollection<IOutputEndpoint> Outputs { get; }
    }
}