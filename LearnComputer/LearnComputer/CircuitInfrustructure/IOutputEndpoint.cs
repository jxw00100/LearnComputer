using System;

namespace LearnComputer.CircuitInfrustructure
{
    public interface IOutputEndpoint
    {
        void Produce(Byte signal);
    }
}
