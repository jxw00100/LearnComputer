using System;
using System.Diagnostics;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography.X509Certificates;
using LearnComputer.CircuitInfrustructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.CircuitInfrustructure
{
    [TestClass]
    public class RelayTest
    {
        #region underlying relay test
        [TestMethod]
        public void DefaultRelay()
        {
            PowerSupplier power = new PowerSupplier();
            IndicatorLight light = new IndicatorLight();
            Relay relay = new Relay();

            relay.Input.ConnectTo(power.Output);
            relay.Output.ConnectTo(light.Input);

            power.On();
            Assert.IsTrue(light.Lighting);
            power.Off();
            Assert.IsFalse(light.Lighting);
            power.Toggle();
            Assert.IsTrue(light.Lighting);
            power.Toggle();
            Assert.IsFalse(light.Lighting);
        }

        [TestMethod]
        public void InvertRelay()
        {
            PowerSupplier power = new PowerSupplier();
            IndicatorLight light = new IndicatorLight();
            Relay relay = new Relay(invert: true);

            relay.Input.ConnectTo(power.Output);
            relay.Output.ConnectTo(light.Input);

            power.On();
            Assert.IsFalse(light.Lighting);
            power.Off();
            Assert.IsTrue(light.Lighting);
            power.Toggle();
            Assert.IsFalse(light.Lighting);
            power.Toggle();
            Assert.IsTrue(light.Lighting);
        }

        [TestMethod]
        public void InvertRelayNotConnectToPower()
        {
            IndicatorLight light = new IndicatorLight();
            Relay relay = new Relay(invert: true);

            relay.Output.ConnectTo(light.Input);

            Assert.IsTrue(light.Lighting);
        }

        [TestMethod]
        public void DelayRelay()
        {
            Int32 delayMilliseconds = 50;
            PowerSupplier power = new PowerSupplier();
            Relay relay = new Relay(delayMilliseconds: delayMilliseconds);
            InputEndpoint input = new InputEndpoint();
            Stopwatch timer = new Stopwatch();
            input.Receive += (s, sg) => { if(sg == 1) timer.Stop(); };

            relay.Input.ConnectTo(power.Output);
            relay.Output.ConnectTo(input);
            
            timer.Start();
            power.On();

            var deltaMilliseconds = Math.Abs(timer.ElapsedMilliseconds - delayMilliseconds);
            Assert.IsTrue(deltaMilliseconds <= 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Delay time must be larger than or equal to 0")]
        public void RelayDelayWithInvalidTime()
        {
            Relay relay = new Relay(delayMilliseconds: -1);
        }
        #endregion

        #region delayer test
        [TestMethod]
        public void Delayer50Milliseconds()
        {
            Int32 delayMilliseconds = 50;
            PowerSupplier power = new PowerSupplier();
            Delayer delayer = new Delayer(delayMilliseconds);
            InputEndpoint input = new InputEndpoint();
            Stopwatch timer = new Stopwatch();
            input.Receive += (s, sg) => { if (sg == 1) timer.Stop(); };

            delayer.Input.ConnectTo(power.Output);
            delayer.Output.ConnectTo(input);

            timer.Start();
            power.On();

            var deltaMilliseconds = Math.Abs(timer.ElapsedMilliseconds - delayMilliseconds);
            Assert.IsTrue(deltaMilliseconds <= 1);
        }

        [TestMethod]
        public void Delayer5Milliseconds()
        {
            Int32 delayMilliseconds = 5;
            PowerSupplier power = new PowerSupplier();
            Delayer delayer = new Delayer(delayMilliseconds);
            InputEndpoint input = new InputEndpoint();
            Stopwatch timer = new Stopwatch();
            input.Receive += (s, sg) => { if (sg == 1) timer.Stop(); };

            delayer.Input.ConnectTo(power.Output);
            delayer.Output.ConnectTo(input);

            timer.Start();
            power.On();

            var deltaMilliseconds = Math.Abs(timer.ElapsedMilliseconds - delayMilliseconds);
            Assert.IsTrue(deltaMilliseconds <= 1);
        }

        [TestMethod]
        public void Delayer999Milliseconds()
        {
            Int32 delayMilliseconds = 999;
            PowerSupplier power = new PowerSupplier();
            Delayer delayer = new Delayer(delayMilliseconds);
            InputEndpoint input = new InputEndpoint();
            Stopwatch timer = new Stopwatch();
            input.Receive += (s, sg) => { if (sg == 1) timer.Stop(); };

            delayer.Input.ConnectTo(power.Output);
            delayer.Output.ConnectTo(input);

            timer.Start();
            power.On();

            var deltaMilliseconds = Math.Abs(timer.ElapsedMilliseconds - delayMilliseconds);
            Assert.IsTrue(deltaMilliseconds <= 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Delay time must be larger than 0")]
        public void DelayerDelayWith0Millisecond()
        {
            Delayer delayer = new Delayer(0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Delay time must be larger than 0")]
        public void DelayerDelayWithNegativeMilliseconds()
        {
            Delayer delayer = new Delayer(-10);
        }
        #endregion

        #region invertor test
        [TestMethod]
        public void DefaultInvertor()
        {
            PowerSupplier power = new PowerSupplier();
            IndicatorLight light = new IndicatorLight();
            Invertor invertor = new Invertor();

            invertor.Input.ConnectTo(power.Output);
            invertor.Output.ConnectTo(light.Input);

            power.On();
            Assert.IsFalse(light.Lighting);
            power.Off();
            Assert.IsTrue(light.Lighting);
            power.Toggle();
            Assert.IsFalse(light.Lighting);
            power.Toggle();
            Assert.IsTrue(light.Lighting);
        }

        [TestMethod]
        public void InvertorNotConnectToPower()
        {
            IndicatorLight light = new IndicatorLight();
            Invertor invertor = new Invertor();

            invertor.Output.ConnectTo(light.Input);

            Assert.IsTrue(light.Lighting);
        }
        #endregion
    }
}
