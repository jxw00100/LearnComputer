using System;
using LearnComputer.CircuitInfrustructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.CircuitInfrustructure
{
    [TestClass]
    public class HalfAdderTest
    {
        private HalfAdder _halfAdder;
        private PowerSupplier _power1, _power2;
        private IndicatorLight _lightSum, _lightCarry;
                
        public HalfAdderTest()
        {
            _halfAdder = new HalfAdder();
            _power1 = new PowerSupplier();
            _power2 = new PowerSupplier();
            _lightSum = new IndicatorLight();
            _lightCarry = new IndicatorLight();

            _halfAdder.Number1In.ConnectTo(_power1.Output);
            _halfAdder.Number2In.ConnectTo(_power2.Output);
            _halfAdder.Sum.ConnectTo(_lightSum.Input);
            _halfAdder.CarryOut.ConnectTo(_lightCarry.Input);
        }

        [TestInitialize]
        public void TestInit()
        {
            _power1.Off();
            _power2.Off();
        }

        [TestCleanup]
        public void TestClean()
        {
        }

        [TestMethod]
        public void CreateNew()
        {
            IHalfAdder halfAdder = new HalfAdder();
            Assert.IsNotNull(halfAdder);

            Assert.IsNotNull(halfAdder.Number1In);
            Assert.AreEqual(halfAdder.Number1In.LastReceivedSignal, 0);

            Assert.IsNotNull(halfAdder.Number2In);
            Assert.AreEqual(halfAdder.Number2In.LastReceivedSignal, 0);

            Assert.IsNotNull(halfAdder.Sum);
            Assert.AreEqual(halfAdder.Sum.LastSentSignal, 0);

            Assert.IsNotNull(halfAdder.CarryOut);
            Assert.AreEqual(halfAdder.CarryOut.LastSentSignal, 0);
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
            _power1.Off();
            _power2.On();

            Assert.IsTrue(_lightSum.Lighting);
            Assert.IsFalse(_lightCarry.Lighting);
        }

        [TestMethod]
        public void Add1To0()
        {
            _power1.On();
            _power2.Off();

            Assert.IsTrue(_lightSum.Lighting);
            Assert.IsFalse(_lightCarry.Lighting);
        }

        [TestMethod]
        public void Add1To1()
        {
            _power1.On();
            _power2.On();

            Assert.IsFalse(_lightSum.Lighting);
            Assert.IsTrue(_lightCarry.Lighting);
        }
    }
}
