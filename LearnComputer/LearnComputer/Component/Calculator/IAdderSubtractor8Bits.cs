﻿using LearnComputer.CircuitInfrustructure;

namespace LearnComputer.Component
{
    public interface IAdderSubtractor8Bits
    {
        IInputEndpointCollection<IInputEndpoint> Number1Inputs { get; }
        IInputEndpointCollection<IInputEndpoint> Number2Inputs { get; }
        IInputEndpoint TurnSubstract { get; }
        IOutputEndpointCollection<IOutputEndpoint> ResultOutputs { get; }
        IOutputEndpoint OverflowUnderflow { get; }
    }
}