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
            InputEndpoint input1 = new InputEndpoint();
            InputEndpoint input2 = new InputEndpoint();
            OutputEndpoint output1 = new OutputEndpoint();
            OutputEndpoint output2 = new OutputEndpoint();
            NeutralEndpoint netrual1 = new NeutralEndpoint();
            NeutralEndpoint netrual2 = new NeutralEndpoint();

            Nexus nexus = new Nexus(6, input1, input2, output1, output2, netrual1, netrual2);

            Assert.IsNotNull(nexus);
        }

        [TestMethod]
        public void CreateNewWithInsufficientConnectionPoints()
        {
            InputEndpoint input = new InputEndpoint();
            OutputEndpoint output = new OutputEndpoint();
            NeutralEndpoint netrual = new NeutralEndpoint();

            Nexus nexus = new Nexus(6, input, output, netrual);

            Assert.IsNotNull(nexus);
        }

        [TestMethod]
        public void CreateNewWithExcessiveConnectionPoints()
        {
            InputEndpoint input1 = new InputEndpoint();
            InputEndpoint input2 = new InputEndpoint();
            OutputEndpoint output1 = new OutputEndpoint();
            OutputEndpoint output2 = new OutputEndpoint();
            NeutralEndpoint netrual1 = new NeutralEndpoint();
            NeutralEndpoint netrual2 = new NeutralEndpoint();

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
            InputEndpoint input = new InputEndpoint();
            Int32 inputSignal = 0;
            input.Receive += (sender, signal) => { inputSignal = signal; };
            OutputEndpoint output1 = new OutputEndpoint();
            OutputEndpoint output2 = new OutputEndpoint();
            NeutralEndpoint netrual = new NeutralEndpoint();
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
            InputEndpoint input1 = new InputEndpoint();
            Int32 inputSignal1 = 0;
            input1.Receive += (sender, signal) => { inputSignal1 = signal; };
            InputEndpoint input2 = new InputEndpoint();
            Int32 inputSignal2 = 0;
            input2.Receive += (sender, signal) => { inputSignal2 = signal; };
            OutputEndpoint output = new OutputEndpoint();
            NeutralEndpoint netrual = new NeutralEndpoint();

            Nexus nexus = new Nexus(6, input1, output, netrual);

            output.Produce(1);
            Assert.AreEqual(inputSignal1, 1);
            Assert.AreEqual(inputSignal2, 0);

            nexus.ConnectAt(input2, 3);
            Assert.AreEqual(inputSignal1, 1);
            Assert.AreEqual(inputSignal2, 1);
        }
        
    }
}
