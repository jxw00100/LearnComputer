using System;
using System.Linq;
using ComputerTest.Assist;
using LearnComputer.CircuitInfrustructure;
using LearnComputer.Component;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.Component
{
    [TestClass]
    public class AdderSubtractorTest
    {
        private Switches _8BitsNumber1Switches, _8BitsNumber2Switches;
        private IndicatorLight[] _8BitsResultLights;
        PowerSupplier _turnSub8Bits;
        private IndicatorLight _overflowUnderflowLight8Bits;
        private IAdderSubtractor8Bits _8BitsAdderSuber;

        public AdderSubtractorTest()
        {
            _8BitsNumber1Switches = new Switches(8);
            _8BitsNumber2Switches = new Switches(8);
            Init8BitsLights();
            _turnSub8Bits = new PowerSupplier();
            _overflowUnderflowLight8Bits = new IndicatorLight();

            _8BitsAdderSuber = new AdderSubtractor8Bits();

            _8BitsAdderSuber.Number1Inputs.Connect(_8BitsNumber1Switches.Outputs);
            _8BitsAdderSuber.Number2Inputs.Connect(_8BitsNumber2Switches.Outputs);
            _8BitsAdderSuber.TurnSubstract.ConnectTo(_turnSub8Bits.Output);
            _8BitsAdderSuber.ResultOutputs.Connect(_8BitsResultLights.Select(l => l.Input as InputEndpoint));
            _8BitsAdderSuber.OverflowUnderflow.ConnectTo(_overflowUnderflowLight8Bits.Input);
        }

        private void Init8BitsLights()
        {
            _8BitsResultLights = new IndicatorLight[8];
            for (var i = 0; i < 8; i++)
            {
                _8BitsResultLights[i] = new IndicatorLight();
            }
        }

        private void Assert8BitsResultEquals(String binaryResult)
        {
            if (binaryResult.Length > 9) throw new ArgumentException("Result is too long.");

            _8BitsResultLights.AssertEquals(binaryResult);
        }

        [TestInitialize]
        public void TestInit()
        {
            _8BitsNumber1Switches.Clear();
            _8BitsNumber2Switches.Clear();
            _turnSub8Bits.Off();
        }

        [TestCleanup]
        public void TestClean()
        {
        }

        [TestMethod]
        public void CreateNewFor8Bits()
        {
            AdderSubtractor8Bits adderSuber = new AdderSubtractor8Bits();

            Assert.IsNotNull(adderSuber);
        }

        [TestMethod]
        public void Add0To0()
        {
            _8BitsNumber1Switches.Set();
            _8BitsNumber2Switches.Set();

            Assert8BitsResultEquals("0");
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Add1To0()
        {
            _8BitsNumber1Switches.Set(true);
            _8BitsNumber2Switches.Set();

            Assert8BitsResultEquals("1");
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Add0To1()
        {
            _8BitsNumber1Switches.Set();
            _8BitsNumber2Switches.Set(true);

            Assert8BitsResultEquals("1");
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Add1To1()
        {
            _8BitsNumber1Switches.Set(true);
            _8BitsNumber2Switches.Set(true);

            Assert8BitsResultEquals("10");
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Sub0By0()
        {
            _8BitsNumber1Switches.Set();
            _8BitsNumber2Switches.Set();
            _turnSub8Bits.On();
            Assert8BitsResultEquals("0");
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Sub1By0()
        {
            _8BitsNumber1Switches.Set(true);
            _8BitsNumber2Switches.Set();
            _turnSub8Bits.On();

            Assert8BitsResultEquals("1");
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Sub0By1()
        {
            _8BitsNumber1Switches.Set();
            _8BitsNumber2Switches.Set(true);
            _turnSub8Bits.On();

            Assert8BitsResultEquals("11111111"); //means -1
            Assert.IsTrue(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Sub1By1()
        {
            _8BitsNumber1Switches.Set(true);
            _8BitsNumber2Switches.Set(true);
            _turnSub8Bits.On();

            Assert8BitsResultEquals("0");
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Add10000000To10000000()
        {
            _8BitsNumber1Switches.Set(false, false, false, false, false, false, false, true);
            _8BitsNumber2Switches.Set(false, false, false, false, false, false, false, true);
            Assert8BitsResultEquals("00000000");
            Assert.IsTrue(_overflowUnderflowLight8Bits.Lighting); //means 100000000
        }

        [TestMethod]
        public void Sub10101010By01010101()
        {
            _8BitsNumber1Switches.Set(false, true, false, true, false, true, false, true);
            _8BitsNumber2Switches.Set(true, false, true, false, true, false, true, false);
            _turnSub8Bits.On();

            Assert8BitsResultEquals("01010101");
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Sub01010101By10101010()
        {
            _8BitsNumber1Switches.Set(true, false, true, false, true, false, true, false);
            _8BitsNumber2Switches.Set(false, true, false, true, false, true, false, true);
            _turnSub8Bits.On();

            Assert8BitsResultEquals("10101011");  //means -01010101
            Assert.IsTrue(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Sub01011011By00010100()
        {
            _8BitsNumber1Switches.Set(true, true, false, true, true, false, true, false);
            _8BitsNumber2Switches.Set(false, false, true, false, true, false, false, false);
            _turnSub8Bits.On();

            Assert8BitsResultEquals("01000111"); 
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Sub00010100By01011011()
        {
            _8BitsNumber1Switches.Set(false, false, true, false, true, false, false, false);
            _8BitsNumber2Switches.Set(true, true, false, true, true, false, true, false);
            _turnSub8Bits.On();

            Assert8BitsResultEquals("10111001"); //means -01000111
            Assert.IsTrue(_overflowUnderflowLight8Bits.Lighting);
        }
    }
}
