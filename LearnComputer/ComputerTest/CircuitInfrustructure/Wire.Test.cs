using System;
using LearnComputer.CircuitInfrustructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.CircuitInfrustructure
{
    [TestClass]
    public class WireTest
    {
        [TestMethod]
        public void CreateNew()
        {
            IInputEndpoint input = new InputEndpoint();
            IOutputEndpoint output = new OutputEndpoint();
            Wire wire = new Wire(input, output);

            Assert.IsNotNull(wire);
        }

        [TestMethod]
        public void SendSignal()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 receivedSignal = 0;
            IEndpoint senderPoint = null;
            input.Receive += (sdr, signal) =>
            {
                senderPoint = sdr;
                receivedSignal = signal;
            };

            IOutputEndpoint output = new OutputEndpoint();
            Wire wire = new Wire(input, output);

            output.Produce(1);
            Assert.AreEqual(receivedSignal, 1);

            output.Produce(0);
            Assert.AreEqual(receivedSignal, 0);
        }

        [TestMethod]
        public void SendSignalBackAndForth()
        {
            INeutralEndpoint nuetral1 = new NeutralEndpoint();
            Int32 receivedSignal1 = 0;
            IEndpoint senderPoint1 = null;
            nuetral1.Receive += (sdr, signal) =>
            {
                senderPoint1 = sdr;
                receivedSignal1 = signal;
            };

            INeutralEndpoint nuetral2 = new NeutralEndpoint();
            Int32 receivedSignal2 = 0;
            IEndpoint senderPoint2 = null;
            nuetral2.Receive += (sdr, signal) =>
            {
                senderPoint2 = sdr;
                receivedSignal2 = signal;
            };

            Wire wire = new Wire(nuetral1, nuetral2);

            nuetral1.Produce(1);
            Assert.AreEqual(receivedSignal1, 0);
            Assert.AreEqual(receivedSignal2, 1);

            nuetral2.Produce(1);
            Assert.AreEqual(receivedSignal1, 1);
            Assert.AreEqual(receivedSignal2, 1);
        }

        [TestMethod]
        public void ConnectLater()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 receivedSignal = 0;
            IEndpoint senderPoint = null;
            input.Receive += (sdr, signal) =>
            {
                senderPoint = sdr;
                receivedSignal = signal;
            };

            IOutputEndpoint output = new OutputEndpoint();

            Wire wire = new Wire();
            wire.Connect(input, output);

            output.Produce(1);
            Assert.AreEqual(receivedSignal, 1);

            output.Produce(0);
            Assert.AreEqual(receivedSignal, 0);
        }

        [TestMethod]
        public void ConnectNullPointWithoutException()
        {
            Wire wire1 = new Wire();
            Wire wire2 = new Wire(null, null);

            IInputEndpoint input = new InputEndpoint();
            Wire wire3 = new Wire(input);
            Wire wire4 = new Wire(input, null);
            Wire wire5 = new Wire(null, input);

            Assert.IsNotNull(wire1);
            Assert.IsNotNull(wire2);
            Assert.IsNotNull(wire3);
            Assert.IsNotNull(wire4);
            Assert.IsNotNull(wire5);

            wire1.Connect(input, null);
            wire1.Connect(null, input);
            wire1.Connect(null, null);
        }
        
        [TestMethod]
        [ExpectedException(typeof (ArgumentException),
            "Unable to connect 2 same endpoints.")]
        public void CreateNewWireWithSamePoint()
        {
            IInputEndpoint input = new InputEndpoint();
            Wire wire = new Wire(input, input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Unable to connect 2 same endpoints.")]
        public void ConnectSamePoint()
        {
            IInputEndpoint input1 = new InputEndpoint();
            IInputEndpoint input2 = new InputEndpoint();
            Wire wire = new Wire(input1, input2);
            wire.Connect(input1, input1);
        }

    }
}
