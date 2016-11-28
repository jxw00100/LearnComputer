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
            IInputEndpoint input = new InputEndpoint();
            IOutputEndpoint output = new OutputEndpoint();
            INeutralEndpoint neutral = new NeutralEndpoint();

            Assert.IsNotNull(input);
            Assert.IsNotNull(output);
            Assert.IsNotNull(neutral);
        }

        [TestMethod]
        public void CreateNewInputPointWithConnectPoint()
        {
            IInputEndpoint input1 = new InputEndpoint();
            IInputEndpoint input2 = new InputEndpoint(input1);

            Assert.IsNotNull(input1.ConnectedPoint);
            Assert.IsNotNull(input2.ConnectedPoint);
            Assert.AreSame(input1.ConnectedPoint, input2);
            Assert.AreSame(input2.ConnectedPoint, input1);
        }

        [TestMethod]
        public void CreateNewOutputPointWithConnectPoint()
        {
            IOutputEndpoint output1 = new OutputEndpoint();
            IOutputEndpoint output2 = new OutputEndpoint(output1);

            Assert.IsNotNull(output1.ConnectedPoint);
            Assert.IsNotNull(output2.ConnectedPoint);
            Assert.AreSame(output1.ConnectedPoint, output2);
            Assert.AreSame(output2.ConnectedPoint, output1);
        }

        [TestMethod]
        public void CreateNewNeutralPointWithConnectPoint()
        {
            INeutralEndpoint nuetral1 = new NeutralEndpoint();
            INeutralEndpoint neutral2 = new NeutralEndpoint(nuetral1);

            Assert.IsNotNull(nuetral1.ConnectedPoint);
            Assert.IsNotNull(neutral2.ConnectedPoint);
            Assert.AreSame(nuetral1.ConnectedPoint, neutral2);
            Assert.AreSame(neutral2.ConnectedPoint, nuetral1);
        }

        [TestMethod]
        public void CreateDifferentPointWithConnectPoint()
        {
            IInputEndpoint input1 = new InputEndpoint();
            IOutputEndpoint output1 = new OutputEndpoint();
            INeutralEndpoint nuetral1 = new NeutralEndpoint();

            IInputEndpoint input2 = new InputEndpoint(output1);
            IOutputEndpoint output2 = new OutputEndpoint(nuetral1);
            INeutralEndpoint neutral2 = new NeutralEndpoint(input1);

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
            IInputEndpoint input1 = new InputEndpoint();
            IInputEndpoint input2 = new InputEndpoint(input1);

            input1.DisconnectEndpoint();

            Assert.IsNull(input1.ConnectedPoint);
            Assert.IsNull(input2.ConnectedPoint);
        }

        [TestMethod]
        public void DisconnectEndpointWithPrechargedSignal()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 receivedSignal = 0;
            input.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            IOutputEndpoint output = new OutputEndpoint(input);

            output.Produce(1);
            Assert.AreEqual(receivedSignal, 1);

            output.DisconnectEndpoint();
            Assert.AreEqual(receivedSignal, 0);
        }

        [TestMethod]
        public void DisconnectInputWithoutConnection()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 callTimes = 0;
            input.Receive += (sender, signal) =>
            {
                callTimes++;
            };

            input.DisconnectEndpoint();
            Assert.AreEqual(callTimes, 0);
            input.DisconnectEndpoint();
            Assert.AreEqual(callTimes, 0);
        }

        [TestMethod]
        public void DisconnectInputWithConnectionNoCharge()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 callTimes = 0;
            input.Receive += (sender, signal) =>
            {
                callTimes++;
            };
            IOutputEndpoint output = new OutputEndpoint(input);

            input.DisconnectEndpoint();

            Assert.AreEqual(callTimes, 1);
        }

        [TestMethod]
        public void DisconnectInputMoreThanOneTimeWithConnectionNoCharge()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 callTimes = 0;
            input.Receive += (sender, signal) =>
            {
                callTimes++;
            };
            IOutputEndpoint output = new OutputEndpoint(input);

            input.DisconnectEndpoint();
            input.DisconnectEndpoint();
            input.DisconnectEndpoint();
            input.DisconnectEndpoint();

            Assert.AreEqual(callTimes, 1);
        }

        [TestMethod]
        public void DisconnectInputWithConnectionPrecharged()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 callTimes = 0;
            input.Receive += (sender, signal) =>
            {
                callTimes++;
            };
            IOutputEndpoint output = new OutputEndpoint();
            output.Produce(1);
            output.ConnectTo(input);

            input.DisconnectEndpoint();

            Assert.AreEqual(callTimes, 2);
        }

        [TestMethod]
        public void DisconnectInputMoreThanOneTimeWithConnectionPrecharged()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 callTimes = 0;
            input.Receive += (sender, signal) =>
            {
                callTimes++;
            };
            IOutputEndpoint output = new OutputEndpoint();
            output.Produce(1);
            output.ConnectTo(input);

            input.DisconnectEndpoint();
            input.DisconnectEndpoint();
            input.DisconnectEndpoint();
            input.DisconnectEndpoint();

            Assert.AreEqual(callTimes, 2);
        }

        [TestMethod]
        public void ChangeEndpointMidwayWithPrechargedSignal()
        {
            IInputEndpoint input1 = new InputEndpoint();
            Int32 receivedSignal1 = 0;
            input1.Receive += (sender, signal) =>
            {
                receivedSignal1 = signal;
            };
            IInputEndpoint input2 = new InputEndpoint();
            Int32 receivedSignal2 = 0;
            input2.Receive += (sender, signal) =>
            {
                receivedSignal2 = signal;
            };
            IOutputEndpoint output = new OutputEndpoint(input1);

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
            IInputEndpoint input1 = new InputEndpoint();
            IInputEndpoint input2 = new InputEndpoint(input1);

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
            IInputEndpoint input1 = new InputEndpoint();
            IInputEndpoint input2 = new InputEndpoint(input1);
            IInputEndpoint input3 = new InputEndpoint(input2);

            Assert.IsNull(input1.ConnectedPoint);
            Assert.IsNotNull(input2.ConnectedPoint);
            Assert.IsNotNull(input3.ConnectedPoint);
            Assert.AreSame(input2.ConnectedPoint, input3);
            Assert.AreSame(input3.ConnectedPoint, input2);
        }

        [TestMethod]
        public void inputConnectToTheSameOutputMoreThanOneTime()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 callTimes = 0;
            input.Receive += (sender, signal) =>
            {
                callTimes++;
            };
            IOutputEndpoint output = new OutputEndpoint();
            input.ConnectTo(output);
            Assert.AreEqual(callTimes, 1);

            input.ConnectTo(output);
            Assert.AreEqual(callTimes, 1);

            output.ConnectTo(input);
            Assert.AreEqual(callTimes, 1);

            output.ConnectTo(input);
            Assert.AreEqual(callTimes, 1);

            input.ConnectTo(output);
            Assert.AreEqual(callTimes, 1);

            output.ConnectTo(input);
            Assert.AreEqual(callTimes, 1);
        }

        [TestMethod]
        public void inputConnectToTheSameNeutralMoreThanOneTime()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 callTimes = 0;
            input.Receive += (sender, signal) =>
            {
                callTimes++;
            };
            INeutralEndpoint neutral = new NeutralEndpoint();
            input.ConnectTo(neutral);
            Assert.AreEqual(callTimes, 1);

            input.ConnectTo(neutral);
            Assert.AreEqual(callTimes, 1);

            neutral.ConnectTo(input);
            Assert.AreEqual(callTimes, 1);

            neutral.ConnectTo(input);
            Assert.AreEqual(callTimes, 1);

            input.ConnectTo(neutral);
            Assert.AreEqual(callTimes, 1);

            neutral.ConnectTo(input);
            Assert.AreEqual(callTimes, 1);
        }

        [TestMethod]
        public void InputEndpointTransimit()
        {
            IInputEndpoint input1 = new InputEndpoint();
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
            IInputEndpoint input1 = new InputEndpoint();
            input1.Transmit(1);
        }

        [TestMethod]
        public void InputEndpointRecordLastReceivedSignal()
        {
            IInputEndpoint input1 = new InputEndpoint();
            Int32 receivedSignal = 0;
            input1.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            input1.Transmit(1);
            Assert.AreEqual(input1.LastReceivedSignal, 1);
            input1.Transmit(0);
            Assert.AreEqual(input1.LastReceivedSignal, 0);

            IInputEndpoint input2 = new InputEndpoint();
            input2.Transmit(1);
            Assert.AreEqual(input2.LastReceivedSignal, 1);
            input2.Transmit(0);
            Assert.AreEqual(input2.LastReceivedSignal, 0);
        }

        [TestMethod]
        public void NeutralEndpointTransimit()
        {
            INeutralEndpoint newtral = new NeutralEndpoint();
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
            INeutralEndpoint neutral = new NeutralEndpoint();
            neutral.Transmit(1);
        }

        [TestMethod]
        public void NeutralEndpointRecordLastReceivedSignal()
        {
            INeutralEndpoint neutral1 = new NeutralEndpoint();
            Int32 receivedSignal = 0;
            neutral1.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            neutral1.Transmit(1);
            Assert.AreEqual(neutral1.LastReceivedSignal, 1);
            neutral1.Transmit(0);
            Assert.AreEqual(neutral1.LastReceivedSignal, 0);

            INeutralEndpoint neutral2 = new NeutralEndpoint();
            neutral2.Transmit(1);
            Assert.AreEqual(neutral2.LastReceivedSignal, 1);
            neutral2.Transmit(0);
            Assert.AreEqual(neutral2.LastReceivedSignal, 0);
        }

        [TestMethod]
        public void UnconnectedOutputEndpointProduceWithoutException()
        {
            IOutputEndpoint output = new OutputEndpoint();
            output.Produce(1);
        }

        [TestMethod]
        public void OutputEndpointProduceToInput()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 receivedSignal = 0;
            input.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            IOutputEndpoint output = new OutputEndpoint(input);
            output.Produce(1);

            Assert.AreEqual(receivedSignal, 1);
        }

        [TestMethod]
        public void OutputEndpointProduceToNeutral()
        {
            INeutralEndpoint neutral = new NeutralEndpoint();
            Int32 receivedSignal = 0;
            neutral.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            IOutputEndpoint output = new OutputEndpoint(neutral);
            output.Produce(1);

            Assert.AreEqual(receivedSignal, 1);
        }

        [TestMethod]
        public void OutputEndpointProduceToOutputWithNoResult()
        {
            IOutputEndpoint output1 = new OutputEndpoint();
            IOutputEndpoint output2 = new OutputEndpoint(output1);
            output2.Produce(1);
        }

        [TestMethod]
        public void NeutralEndpointProduceToNeutral()
        {
            INeutralEndpoint neutral1 = new NeutralEndpoint();
            Int32 receivedSignal = 0;
            neutral1.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            INeutralEndpoint neutral2 = new NeutralEndpoint(neutral1);
            neutral2.Produce(1);

            Assert.AreEqual(receivedSignal, 1);
        }

        [TestMethod]
        public void NeutralEndpointProduceToInput()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 receivedSignal = 0;
            input.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            INeutralEndpoint neutral = new NeutralEndpoint(input);
            neutral.Produce(1);

            Assert.AreEqual(receivedSignal, 1);
        }

        [TestMethod]
        public void NeutralEndpointProduceToOutputWithNoResult()
        {
            IOutputEndpoint output = new OutputEndpoint();
            INeutralEndpoint neutral = new NeutralEndpoint(output);
            neutral.Produce(1);
        }

        [TestMethod]
        public void ContinuousSendSignals()
        {
            IInputEndpoint input = new InputEndpoint();
            Int32 receivedSignal = 0;
            input.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            IOutputEndpoint output = new OutputEndpoint(input);

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
            IInputEndpoint input = new InputEndpoint();
            Int32 receivedSignal = 0;
            input.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            INeutralEndpoint neutral = new NeutralEndpoint();
            Int32 neutralReceivedSignal = 0;
            neutral.Receive += (sender, signal) =>
            {
                neutralReceivedSignal = signal;
            };
            IOutputEndpoint output = new OutputEndpoint();

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
            IInputEndpoint input = new InputEndpoint();
            Int32 receivedSignal = 0;
            input.Receive += (sender, signal) =>
            {
                receivedSignal = signal;
            };
            INeutralEndpoint neutral = new NeutralEndpoint();
            Int32 neutralReceivedSignal = 0;
            neutral.Receive += (sender, signal) =>
            {
                neutralReceivedSignal = signal;
            };
            INeutralEndpoint neutralOut = new NeutralEndpoint();

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
            IInputEndpoint point = new InputEndpoint();
            point.ConnectTo(point);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Endpoint cannot connect to itself.")]
        public void OutputEndpointConnectToSelf()
        {
            IOutputEndpoint point = new OutputEndpoint();
            point.ConnectTo(point);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Endpoint cannot connect to itself.")]
        public void NeutralEndpointConnectToSelf()
        {
            INeutralEndpoint point = new NeutralEndpoint();
            point.ConnectTo(point);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 2 is not a valid value.")]
        public void InputTransmitInvalidSignal2()
        {
            IInputEndpoint input1 = new InputEndpoint();
            input1.Transmit(2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 255 is not a valid value.")]
        public void InputTransmitInvalidSignal255()
        {
            IInputEndpoint input1 = new InputEndpoint();
            input1.Transmit(255);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 2 is not a valid value.")]
        public void NeutralTransmitInvalidSignal2()
        {
            INeutralEndpoint neutral = new NeutralEndpoint();
            neutral.Transmit(2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 255 is not a valid value.")]
        public void NeutralTransmitInvalidSignal255()
        {
            INeutralEndpoint neutral = new NeutralEndpoint();
            neutral.Transmit(255);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 2 is not a valid value.")]
        public void NeutralProduceInvalidSignal2()
        {
            INeutralEndpoint neutral = new NeutralEndpoint();
            neutral.Produce(2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 255 is not a valid value.")]
        public void NeutralProduceInvalidSignal255()
        {
            INeutralEndpoint neutral = new NeutralEndpoint();
            neutral.Produce(255);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 2 is not a valid value.")]
        public void OutputProduceInvalidSignal2()
        {
            IOutputEndpoint output = new OutputEndpoint();
            output.Produce(2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Signal value can only be either 0 or 1. 255 is not a valid value.")]
        public void OutputProduceInvalidSignal255()
        {
            IOutputEndpoint output = new OutputEndpoint();
            output.Produce(255);
        }
        #endregion
        
    }
}
