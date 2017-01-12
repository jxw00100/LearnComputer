using System;
using ComputerTest.Util;
using LearnComputer.CircuitInfrustructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.CircuitInfrustructure
{
    [TestClass]
    public class FullAdderTest
    {
        private IFullAdder _fullAdder;
        private PowerSupplier _power1, _power2, _powerCarryIn;
        private IndicatorLight _lightSum, _lightCarry;

        public FullAdderTest()
        {
            _fullAdder = new FullAdder();
            _power1 = new PowerSupplier();
            _power2 = new PowerSupplier();
            _powerCarryIn = new PowerSupplier();
            _lightSum = new IndicatorLight();
            _lightCarry = new IndicatorLight();

            _fullAdder.Number1In.ConnectTo(_power1.Output);
            _fullAdder.Number2In.ConnectTo(_power2.Output);
            _fullAdder.CarryIn.ConnectTo(_powerCarryIn.Output);
            _fullAdder.Sum.ConnectTo(_lightSum.Input);
            _fullAdder.CarryOut.ConnectTo(_lightCarry.Input);
        }

        [TestInitialize]
        public void TestInit()
        {
            _power1.Off();
            _power2.Off();
            _powerCarryIn.Off();
        }

        [TestCleanup]
        public void TestClean()
        {
        }

        [TestMethod]
        public void CreateNew()
        {
            IFullAdder fullAdder = new FullAdder();
            Assert.IsNotNull(fullAdder);

            Assert.IsNotNull(fullAdder.Number1In);
            Assert.AreEqual(fullAdder.Number1In.LastReceivedSignal, 0);

            Assert.IsNotNull(fullAdder.Number2In);
            Assert.AreEqual(fullAdder.Number2In.LastReceivedSignal, 0);

            Assert.IsNotNull(fullAdder.CarryIn);
            Assert.AreEqual(fullAdder.CarryIn.LastReceivedSignal, 0);

            Assert.IsNotNull(fullAdder.Sum);
            Assert.AreEqual(fullAdder.Sum.LastSentSignal, 0);

            Assert.IsNotNull(fullAdder.CarryOut);
            Assert.AreEqual(fullAdder.CarryOut.LastSentSignal, 0);
        }

        [TestMethod]
        public void Add0To0()
        {
            _power1.Off();
            _power2.Off();

            Assert.IsFalse(_lightSum.Lighting);
            Assert.IsFalse(_lightCarry.Lighting);
        }

        [TestMethod]
        public void Add0To1()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_power1.Off());
            ThreadHelper.ExecuteThenSleepShortly(()=>_power2.On());

            Assert.IsTrue(_lightSum.Lighting);
            Assert.IsFalse(_lightCarry.Lighting);
        }

        [TestMethod]
        public void Add1To0()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_power1.On());
            ThreadHelper.ExecuteThenSleepShortly(()=>_power2.Off());

            Assert.IsTrue(_lightSum.Lighting);
            Assert.IsFalse(_lightCarry.Lighting);
        }

        [TestMethod]
        public void Add1To1()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_power1.On());
            ThreadHelper.ExecuteThenSleepShortly(()=>_power2.On());

            Assert.IsFalse(_lightSum.Lighting);
            Assert.IsTrue(_lightCarry.Lighting);
        }

        [TestMethod]
        public void Add0To0WithCarry()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_powerCarryIn.On());
            ThreadHelper.ExecuteThenSleepShortly(()=>_power1.Off());
            ThreadHelper.ExecuteThenSleepShortly(()=>_power2.Off());

            Assert.IsTrue(_lightSum.Lighting);
            Assert.IsFalse(_lightCarry.Lighting);
        }

        [TestMethod]
        public void Add0To1WithCarry()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_powerCarryIn.On());
            ThreadHelper.ExecuteThenSleepShortly(()=>_power1.Off());
            ThreadHelper.ExecuteThenSleepShortly(()=>_power2.On());

            Assert.IsFalse(_lightSum.Lighting);
            Assert.IsTrue(_lightCarry.Lighting);
        }

        [TestMethod]
        public void Add1To0WithCarry()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_powerCarryIn.On());
            ThreadHelper.ExecuteThenSleepShortly(()=>_power1.On());
            ThreadHelper.ExecuteThenSleepShortly(()=>_power2.Off());

            Assert.IsFalse(_lightSum.Lighting);
            Assert.IsTrue(_lightCarry.Lighting);
        }

        [TestMethod]
        public void Add1To1WithCarry()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_powerCarryIn.On());
            ThreadHelper.ExecuteThenSleepShortly(()=>_power1.On());
            ThreadHelper.ExecuteThenSleepShortly(()=>_power2.On());

            Assert.IsTrue(_lightSum.Lighting);
            Assert.IsTrue(_lightCarry.Lighting);
        }
    }
}
