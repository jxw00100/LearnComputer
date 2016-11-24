namespace LearnComputer.CircuitInfrustructure
{
    public interface IEndpoint
    {
        IEndpoint ConnectedPoint { get; }
        void ConnectTo(IEndpoint point);
        void DisconnectEndpoint();
    }
}
