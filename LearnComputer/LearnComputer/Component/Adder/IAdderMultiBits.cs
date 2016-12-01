using LearnComputer.CircuitInfrustructure;

namespace LearnComputer.Component
{
    public interface IAdderMultiBits
    {
        IInputEndpointCollection<IInputEndpoint> Number1Inputs { get; }
        IInputEndpointCollection<IInputEndpoint> Number2Inputs { get; }
        IInputEndpoint CarryInput { get; }
        IOutputEndpointCollection<IOutputEndpoint> SumOutputs { get; }
        IOutputEndpoint CarryOutput { get; }
    }
}
