using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using ComputerTest.Util;
using LearnComputer.CircuitInfrustructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.CircuitInfrustructure
{
    [TestClass]
    public class OscillatorTest
    {
        private IOscillator _oscillator10Hz;
        private PowerSupplier _startPower;
        private Object _count = 0;

        public OscillatorTest()
        {
            _oscillator10Hz = new Oscillator(50);
            _startPower = new PowerSupplier();

            _oscillator10Hz.Start.ConnectTo(_startPower.Output);
        }

        [TestInitialize]
        public void TestInit()
        {
            _oscillator10Hz.Output.DisconnectEndpoint();
            _startPower.Off();
            _count = 0;
        }

        [TestCleanup]
        public void TestClean()
        {
        }

        [TestMethod]
        public void CreateDefault()
        {
            IOscillator osc = new Oscillator();

            Assert.IsNotNull(osc);
            Assert.AreEqual(osc.Interval, 100);
            Assert.IsNull(osc.Start.ConnectedPoint);
            Assert.IsNull(osc.Output.ConnectedPoint);
            Assert.AreEqual(osc.Output.LastSentSignal, 1);
        }

        [TestMethod]
        public void Create25HzOscillator()
        {
            IOscillator osc = new Oscillator(20);

            Assert.IsNotNull(osc);
            Assert.AreEqual(osc.Interval, 20);
            Assert.IsNull(osc.Start.ConnectedPoint);
            Assert.IsNull(osc.Output.ConnectedPoint);
            Assert.AreEqual(osc.Output.LastSentSignal, 1);
        }

        [TestMethod]
        public void AbleToOscillate()
        {
            IInputEndpoint input = new InputEndpoint();
            input.ConnectTo(_oscillator10Hz.Output);
            input.Receive += (sd, sgn) =>
            {
                Thread.Sleep(1);
                _count = (Int32)_count + 1;
 
            };

            ThreadHelper.ExecuteThenSleepShortly(() => _startPower.On(), 500);
            _startPower.Off();
            Assert.IsTrue((Int32)_count > 0);
        }

        [TestMethod]
        public void Oscillate20HzOnTime()
        {
            IInputEndpoint input = new InputEndpoint();
            input.ConnectTo(_oscillator10Hz.Output);
            input.Receive += (sd, sgn) =>
            {
                Thread.Sleep(1);
                if ((Int32) _count >= 5)
                {

                    _startPower.Off();
                }
                else
                {
                    _count = (Int32)_count + 1;
                }
            };

            ThreadHelper.ExecuteThenSleepShortly(() => _startPower.On(), 300);
            Assert.AreEqual(_count, 5);

            _count = 0;
            ThreadHelper.ExecuteThenSleepShortly(() => _startPower.On(), 250);
            Assert.AreEqual(_count, 5);

            _count = 0;
            ThreadHelper.ExecuteThenSleepShortly(() => _startPower.On(), 100);
            Assert.IsTrue((Int32)_count < 5);

        }

        [TestMethod]
        public void Oscillate25HzOnTime()
        {
            IOscillator oscillator = new Oscillator(20);
            PowerSupplier power = new PowerSupplier();
            power.Output.ConnectTo(oscillator.Start);

            _count = 0;
            Boolean oscillating = false;
            IInputEndpoint input = new InputEndpoint();
            input.ConnectTo(oscillator.Output);
            input.Receive += (sd, sgn) =>
            {
                Thread.Sleep(1);
                if ((Int32)_count >= 5)
                {

                    _startPower.Off();
                }
                else
                {
                    _count = (Int32)_count + 1;
                }
            };

            ThreadHelper.ExecuteThenSleepShortly(() => power.On(), 200);
            Assert.AreEqual(_count, 5);

            _count = 0;
            ThreadHelper.ExecuteThenSleepShortly(() => power.On(), 100);
            Assert.AreEqual(_count, 5);

            _count = 0;
            ThreadHelper.ExecuteThenSleepShortly(() => power.On(), 40);
            Assert.IsTrue((Int32)_count < 5);
        }

        [TestMethod]
        public void OscillateFloppyExactly()
        {
            IInputEndpoint input = new InputEndpoint();
            IList<Int32> results = new List<Int32>();
            input.ConnectTo(_oscillator10Hz.Output);

            input.Receive += (sd, sgn) =>
            {
                Thread.Sleep(1);
                if ((Int32)_count >= 5)
                {
                    _startPower.Off();
                }
                else
                {
                    _count = (Int32)_count + 1;
                    results.Add(sgn);
                }

            };

            ThreadHelper.ExecuteThenSleepShortly(() => _startPower.On(), 500);

            Assert.AreEqual(results.Count, 5);
            Assert.AreEqual(results[0], 0);
            Assert.AreEqual(results[1], 1);
            Assert.AreEqual(results[2], 0);
            Assert.AreEqual(results[3], 1);
            Assert.AreEqual(results[4], 0);
        }
    }
}
