using System;
using LearnComputer.CircuitInfrustructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.CircuitInfrustructure
{
    [TestClass]
    public class EndpointTest
    {
        [TestMethod]
        public void CreateNewEndpoint()
        {
            InputEndpoint input = new InputEndpoint();
            OutputEndpoint output = new OutputEndpoint();
            NeutralEndpoint neutral = new NeutralEndpoint();

            Assert.IsNotNull(input);
            Assert.IsNotNull(output);
            Assert.IsNotNull(neutral);
        }

        [TestMethod]
        public void CreateNewInputPointWithConnectPoint()
        {
            InputEndpoint input1 = new InputEndpoint();
            InputEndpoint input2 = new InputEndpoint(input1);

            Assert.IsNotNull(input1.ConnectedPoint);
            Assert.IsNotNull(input2.ConnectedPoint);
            Assert.AreSame(input1.ConnectedPoint, input2);
            Assert.AreSame(input2.ConnectedPoint, input1);
        }

        [TestMethod]
        public void CreateNewOutputPointWithConnectPoint()
        {
            OutputEndpoint output1 = new OutputEndpoint();
            OutputEndpoint output2 = new OutputEndpoint(output1);

            Assert.IsNotNull(output1.ConnectedPoint);
            Assert.IsNotNull(output2.ConnectedPoint);
            Assert.AreSame(output1.ConnectedPoint, output2);
            Assert.AreSame(output2.ConnectedPoint, output1);
        }

        [TestMethod]
        public void CreateNewNeutralPointWithConnectPoint()
        {
            NeutralEndpoint nuetral1 = new NeutralEndpoint();
            NeutralEndpoint neutral2 = new NeutralEndpoint(nuetral1);

            Assert.IsNotNull(nuetral1.ConnectedPoint);
            Assert.IsNotNull(neutral2.ConnectedPoint);
            Assert.AreSame(nuetral1.ConnectedPoint, neutral2);
            Assert.AreSame(neutral2.ConnectedPoint, nuetral1);
        }

        [TestMethod]
        public void CreateDifferentPointWithConnectPoint()
        {
            InputEndpoint input1 = new InputEndpoint();
            OutputEndpoint output1 = new OutputEndpoint();
            NeutralEndpoint nuetral1 = new NeutralEndpoint();

            InputEndpoint input2 = new InputEndpoint(output1);
            OutputEndpoint output2 = new OutputEndpoint(nuetral1);
            NeutralEndpoint neutral2 = new NeutralEndpoint(input1);

            Assert.IsNotNull(input1.ConnectedPoint);
            Assert.IsNotNull(input2.ConnectedPoint);
            Assert.IsNotNull(output1.ConnectedPoint);
            Assert.IsNotNull(output2.ConnectedPoint);
            Assert.IsNotNull(nuetral1.ConnectedPoint);
            Assert.IsNotNull(neutral2.ConnectedPoint);

            Assert.AreSame(input1.ConnectedPoint, neutral2);
            Assert.AreSame(neutral2.ConnectedPoint, input1);
            Assert.AreSame(output1.ConnectedPoint, input2);
            Assert.AreSame(input2.ConnectedPoint, output1);
            Assert.AreSame(nuetral1.ConnectedPoint, output2);
            Assert.AreSame(output2.ConnectedPoint, nuetral1);
        }

        [TestMethod]
        public void DisconnectEndpoint()
        {
            InputEndpoint input1 = new InputEndpoint();
            InputEndpoint input2 = new InputEndpoint(input1);

            input1.DisconnectEndpoint();

            Assert.IsNull(input1.ConnectedPoint);
            Assert.IsNull(input2.ConnectedPoint);
        }

        [TestMethod]
        public void DisconnectEndpointWithPrechargedSignal()
        {
            InputEndpoint input = new InputEndpoint();
            Int32 receivedSignal = 0;
            input.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            OutputEndpoint output = new OutputEndpoint(input);

            output.Produce(1);
            Assert.AreEqual(receivedSignal, 1);

            output.DisconnectEndpoint();
            Assert.AreEqual(receivedSignal, 0);
        }

        [TestMethod]
        public void ChangeEndpointMidwayWithPrechargedSignal()
        {
            InputEndpoint input1 = new InputEndpoint();
            Int32 receivedSignal1 = 0;
            input1.Receive += (sender, signal) =>
            {
                receivedSignal1 = signal;
            };
            InputEndpoint input2 = new InputEndpoint();
            Int32 receivedSignal2 = 0;
            input2.Receive += (sender, signal) =>
            {
                receivedSignal2 = signal;
            };
            OutputEndpoint output = new OutputEndpoint(input1);

            output.Produce(1);
            Assert.AreEqual(receivedSignal1, 1);
            Assert.AreEqual(receivedSignal2, 0);

            output.ConnectTo(input2);
            Assert.AreEqual(receivedSignal1, 0);
            Assert.AreEqual(receivedSignal2, 1);
        }

        [TestMethod]
        public void Reconnect()
        {
            InputEndpoint input1 = new InputEndpoint();
            InputEndpoint input2 = new InputEndpoint(input1);

            input1.DisconnectEndpoint();
            input1.ConnectTo(input2);

            Assert.IsNotNull(input1.ConnectedPoint);
            Assert.IsNotNull(input2.ConnectedPoint);
            Assert.AreSame(input1.ConnectedPoint, input2);
            Assert.AreSame(input2.ConnectedPoint, input1);
        }

        [TestMethod]
        public void ChangeConnection()
        {
            InputEndpoint input1 = new InputEndpoint();
            InputEndpoint input2 = new InputEndpoint(input1);
            InputEndpoint input3 = new InputEndpoint(input2);

            Assert.IsNull(input1.ConnectedPoint);
            Assert.IsNotNull(input2.ConnectedPoint);
            Assert.IsNotNull(input3.ConnectedPoint);
            Assert.AreSame(input2.ConnectedPoint, input3);
            Assert.AreSame(input3.ConnectedPoint, input2);
        }

        [TestMethod]
        public void InputEndpointTransimit()
        {
            InputEndpoint input1 = new InputEndpoint();
            Int32 receivedSignal = 0;
            input1.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            input1.Transmit(1);

            Assert.AreEqual(receivedSignal, 1);
        }

        [TestMethod]
        public void InputEndpointTransimitWithoutHandler()
        {
            InputEndpoint input1 = new InputEndpoint();
            input1.Transmit(1);
        }

        [TestMethod]
        public void InputEndpointRecordLastReceivedSignal()
        {
            InputEndpoint input1 = new InputEndpoint();
            Int32 receivedSignal = 0;
            input1.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            input1.Transmit(1);
            Assert.AreEqual(input1.LastReceivedSignal, 1);
            input1.Transmit(0);
            Assert.AreEqual(input1.LastReceivedSignal, 0);

            InputEndpoint input2 = new InputEndpoint();
            input2.Transmit(1);
            Assert.AreEqual(input2.LastReceivedSignal, 1);
            input2.Transmit(0);
            Assert.AreEqual(input2.LastReceivedSignal, 0);
        }

        [TestMethod]
        public void NeutralEndpointTransimit()
        {
            NeutralEndpoint newtral = new NeutralEndpoint();
            Int32 receivedSignal = 0;
            newtral.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            newtral.Transmit(1);

            Assert.AreEqual(receivedSignal, 1);
        }

        [TestMethod]
        public void NeutralEndpointTransimitWithoutHandler()
        {
            NeutralEndpoint neutral = new NeutralEndpoint();
            neutral.Transmit(1);
        }

        [TestMethod]
        public void NeutralEndpointRecordLastReceivedSignal()
        {
            NeutralEndpoint neutral1 = new NeutralEndpoint();
            Int32 receivedSignal = 0;
            neutral1.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            neutral1.Transmit(1);
            Assert.AreEqual(neutral1.LastReceivedSignal, 1);
            neutral1.Transmit(0);
            Assert.AreEqual(neutral1.LastReceivedSignal, 0);

            NeutralEndpoint neutral2 = new NeutralEndpoint();
            neutral2.Transmit(1);
            Assert.AreEqual(neutral2.LastReceivedSignal, 1);
            neutral2.Transmit(0);
            Assert.AreEqual(neutral2.LastReceivedSignal, 0);
        }

        [TestMethod]
        public void UnconnectedOutputEndpointProduceWithoutException()
        {
            OutputEndpoint output = new OutputEndpoint();
            output.Produce(1);
        }

        [TestMethod]
        public void OutputEndpointProduceToInput()
        {
            InputEndpoint input = new InputEndpoint();
            Int32 receivedSignal = 0;
            input.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            OutputEndpoint output = new OutputEndpoint(input);
            output.Produce(1);

            Assert.AreEqual(receivedSignal, 1);
        }

        [TestMethod]
        public void OutputEndpointProduceToNeutral()
        {
            NeutralEndpoint neutral = new NeutralEndpoint();
            Int32 receivedSignal = 0;
            neutral.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            OutputEndpoint output = new OutputEndpoint(neutral);
            output.Produce(1);

            Assert.AreEqual(receivedSignal, 1);
        }

        [TestMethod]
        public void OutputEndpointProduceToOutputWithNoResult()
        {
            OutputEndpoint output1 = new OutputEndpoint();
            OutputEndpoint output2 = new OutputEndpoint(output1);
            output2.Produce(1);
        }

        [TestMethod]
        public void NeutralEndpointProduceToNeutral()
        {
            NeutralEndpoint neutral1 = new NeutralEndpoint();
            Int32 receivedSignal = 0;
            neutral1.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            NeutralEndpoint neutral2 = new NeutralEndpoint(neutral1);
            neutral2.Produce(1);

            Assert.AreEqual(receivedSignal, 1);
        }

        [TestMethod]
        public void NeutralEndpointProduceToInput()
        {
            InputEndpoint input = new InputEndpoint();
            Int32 receivedSignal = 0;
            input.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            NeutralEndpoint neutral = new NeutralEndpoint(input);
            neutral.Produce(1);

            Assert.AreEqual(receivedSignal, 1);
        }

        [TestMethod]
        public void NeutralEndpointProduceToOutputWithNoResult()
        {
            OutputEndpoint output = new OutputEndpoint();
            NeutralEndpoint neutral = new NeutralEndpoint(output);
            neutral.Produce(1);
        }

        [TestMethod]
        public void ContinuousSendSignals()
        {
            InputEndpoint input = new InputEndpoint();
            Int32 receivedSignal = 0;
            input.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            OutputEndpoint output = new OutputEndpoint(input);

            output.Produce(1);
            Assert.AreEqual(receivedSignal, 1);

            output.Produce(0);
            Assert.AreEqual(receivedSignal, 0);

            output.Produce(0);
            Assert.AreEqual(receivedSignal, 0);

            output.Produce(1);
            Assert.AreEqual(receivedSignal, 1);

            output.Produce(0);
            Assert.AreEqual(receivedSignal, 0);
        }

        [TestMethod]
        public void ChargeTheOutputThenConnectInputOrNuetral()
        {
            InputEndpoint input = new InputEndpoint();
            Int32 receivedSignal = 0;
            input.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            NeutralEndpoint neutral = new NeutralEndpoint();
            Int32 neutralReceivedSignal = 0;
            neutral.Receive += (sender, signal) =>
            {
                neutralReceivedSignal = signal;
            };
            OutputEndpoint output = new OutputEndpoint();

            output.Produce(1);
            Assert.AreEqual(receivedSignal, 0);
            Assert.AreEqual(neutralReceivedSignal, 0);

            output.ConnectTo(input);
            Assert.AreEqual(receivedSignal, 1);
            Assert.AreEqual(neutralReceivedSignal, 0);

            output.ConnectTo(neutral);
            Assert.AreEqual(receivedSignal, 0);
            Assert.AreEqual(neutralReceivedSignal, 1);
        }

        [TestMethod]
        public void ChargeTheNeutralThenConnectInputOrNuetral()
        {
            InputEndpoint input = new InputEndpoint();
            Int32 receivedSignal = 0;
            input.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            NeutralEndpoint neutral = new NeutralEndpoint();
            Int32 neutralReceivedSignal = 0;
            neutral.Receive += (sender, signal) =>
            {
                neutralReceivedSignal = signal;
            };
            NeutralEndpoint neutralOut = new NeutralEndpoint();

            neutralOut.Produce(1);
            Assert.AreEqual(receivedSignal, 0);
            Assert.AreEqual(neutralReceivedSignal, 0);

            neutralOut.ConnectTo(input);
            Assert.AreEqual(receivedSignal, 1);
            Assert.AreEqual(neutralReceivedSignal, 0);

            neutralOut.ConnectTo(neutral);
            Assert.AreEqual(receivedSignal, 0);
            Assert.AreEqual(neutralReceivedSignal, 1);
        }

        #region Exceptions Test
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Endpoint cannot connect to itself.")]
        public void InputEndpointConnectToSelf()
        {
            InputEndpoint point = new InputEndpoint();
            point.ConnectTo(point);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Endpoint cannot connect to itself.")]
        public void OutputEndpointConnectToSelf()
        {
            OutputEndpoint point = new OutputEndpoint();
            point.ConnectTo(point);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Endpoint cannot connect to itself.")]
        public void NeutralEndpointConnectToSelf()
        {
            NeutralEndpoint point = new NeutralEndpoint();
            point.ConnectTo(point);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 2 is not a valid value.")]
        public void InputTransmitInvalidSignal2()
        {
            InputEndpoint input1 = new InputEndpoint();
            input1.Transmit(2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 255 is not a valid value.")]
        public void InputTransmitInvalidSignal255()
        {
            InputEndpoint input1 = new InputEndpoint();
            input1.Transmit(255);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 2 is not a valid value.")]
        public void NeutralTransmitInvalidSignal2()
        {
            NeutralEndpoint neutral = new NeutralEndpoint();
            neutral.Transmit(2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 255 is not a valid value.")]
        public void NeutralTransmitInvalidSignal255()
        {
            NeutralEndpoint neutral = new NeutralEndpoint();
            neutral.Transmit(255);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 2 is not a valid value.")]
        public void NeutralProduceInvalidSignal2()
        {
            NeutralEndpoint neutral = new NeutralEndpoint();
            neutral.Produce(2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 255 is not a valid value.")]
        public void NeutralProduceInvalidSignal255()
        {
            NeutralEndpoint neutral = new NeutralEndpoint();
            neutral.Produce(255);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 2 is not a valid value.")]
        public void OutputProduceInvalidSignal2()
        {
            OutputEndpoint output = new OutputEndpoint();
            output.Produce(2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 255 is not a valid value.")]
        public void OutputProduceInvalidSignal255()
        {
            OutputEndpoint output = new OutputEndpoint();
            output.Produce(255);
        }
        #endregion
        
    }
}
