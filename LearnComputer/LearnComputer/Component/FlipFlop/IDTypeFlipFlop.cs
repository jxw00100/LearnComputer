using LearnComputer.CircuitInfrustructure;

namespace LearnComputer.Component
{
    public interface IDTypeFlipFlop
    {
        IInputEndpoint Data { get; }
        IInputEndpoint Clock { get; }
        IOutputEndpoint Q { get; }
        IOutputEndpoint QBar { get; }
    }
}