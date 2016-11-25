using System;
using LearnComputer.CircuitInfrustructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.CircuitInfrustructure
{
    [TestClass]
    public class NexusTest
    {
        [TestMethod]
        public void CreateNew()
        {
            IInputEndpoint input1 = new InputEndpoint();
            IInputEndpoint input2 = new InputEndpoint();
            IOutputEndpoint output1 = new OutputEndpoint();
            IOutputEndpoint output2 = new OutputEndpoint();
            INeutralEndpoint netrual1 = new NeutralEndpoint();
            INeutralEndpoint netrual2 = new NeutralEndpoint();

            Nexus nexus = new Nexus(6, input1, input2, output1, output2, netrual1, netrual2);

            Assert.IsNotNull(nexus);
        }

        [TestMethod]
        public void CreateNewWithInsufficientConnectionPoints()
        {
            IInputEndpoint input = new InputEndpoint();
            IOutputEndpoint output = new OutputEndpoint();
            INeutralEndpoint netrual = new NeutralEndpoint();

            Nexus nexus = new Nexus(6, input, output, netrual);

            Assert.IsNotNull(nexus);
        }

        [TestMethod]
        public void CreateNewWithExcessiveConnectionPoints()
        {
            IInputEndpoint input1 = new InputEndpoint();
            IInputEndpoint input2 = new InputEndpoint();
            IOutputEndpoint output1 = new OutputEndpoint();
            IOutputEndpoint output2 = new OutputEndpoint();
            INeutralEndpoint netrual1 = new NeutralEndpoint();
            INeutralEndpoint netrual2 = new NeutralEndpoint();

            Nexus nexus = new Nexus(3, input1, input2, output1, output2, netrual1, netrual2);

            Assert.IsNotNull(nexus);
        }

        [TestMethod]
        public void CreateNewWithNoConnectionPoint()
        {
            Nexus nexus = new Nexus(3);

            Assert.IsNotNull(nexus);
        }

        [TestMethod]
        public void SendSignal()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 inputSignal = 0;
            input.Receive += (sender, signal) => { inputSignal = signal; };
            IOutputEndpoint output1 = new OutputEndpoint();
            IOutputEndpoint output2 = new OutputEndpoint();
            INeutralEndpoint netrual = new NeutralEndpoint();
            Int32 netrualSignal = 0;
            netrual.Receive += (sender, signal) => { netrualSignal = signal; };

            Nexus nexus = new Nexus(6, input, output1, output2, netrual);

            output1.Produce(1);
            Assert.AreEqual(inputSignal, 1);
            Assert.AreEqual(netrualSignal, 1);
            output2.Produce(0);
            Assert.AreEqual(inputSignal, 1);
            Assert.AreEqual(netrualSignal, 1);
            output1.Produce(0);
            Assert.AreEqual(inputSignal, 0);
            Assert.AreEqual(netrualSignal, 0);
            netrual.Produce(1);
            Assert.AreEqual(inputSignal, 1);
            Assert.AreEqual(netrualSignal, 0);
        }

        [TestMethod]
        public void ConnectPostCreating()
        {
            IInputEndpoint input1 = new InputEndpoint();
            Int32 inputSignal1 = 0;
            input1.Receive += (sender, signal) => { inputSignal1 = signal; };
            IInputEndpoint input2 = new InputEndpoint();
            Int32 inputSignal2 = 0;
            input2.Receive += (sender, signal) => { inputSignal2 = signal; };
            IOutputEndpoint output = new OutputEndpoint();
            INeutralEndpoint netrual = new NeutralEndpoint();

            Nexus nexus = new Nexus(6, input1, output, netrual);

            output.Produce(1);
            Assert.AreEqual(inputSignal1, 1);
            Assert.AreEqual(inputSignal2, 0);

            nexus.ConnectAt(input2, 3);
            Assert.AreEqual(inputSignal1, 1);
            Assert.AreEqual(inputSignal2, 1);
        }

        [TestMethod]
        public void ToggleOneInpuSigalWhileOtherInputIs0()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 inputSignal = 0;
            input.Receive += (sender, signal) => { inputSignal = signal; };
            IOutputEndpoint output1 = new OutputEndpoint();
            IOutputEndpoint output2 = new OutputEndpoint();

            Nexus nexus = new Nexus(3, input, output1, output2);

            output1.Produce(1);
            Assert.AreEqual(inputSignal, 1);
            output1.Produce(0);
            Assert.AreEqual(inputSignal, 0);
            output1.Produce(1);
            Assert.AreEqual(inputSignal, 1);
            output1.Produce(0);
            Assert.AreEqual(inputSignal, 0);
        }

        [TestMethod]
        public void ToggleOneInpuSigalWhileOtherInputIs1()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 inputSignal = 0;
            input.Receive += (sender, signal) => { inputSignal = signal; };
            IOutputEndpoint output1 = new OutputEndpoint();
            IOutputEndpoint output2 = new OutputEndpoint();
            output2.Produce(1);

            Nexus nexus = new Nexus(3, input, output1, output2);

            output1.Produce(1);
            Assert.AreEqual(inputSignal, 1);
            output1.Produce(0);
            Assert.AreEqual(inputSignal, 1);
            output1.Produce(1);
            Assert.AreEqual(inputSignal, 1);
            output1.Produce(1);
            Assert.AreEqual(inputSignal, 1);
        }
    }
}
