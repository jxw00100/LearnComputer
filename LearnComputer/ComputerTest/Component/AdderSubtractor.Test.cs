using System;
using System.Linq;
using ComputerTest.Assist;
using ComputerTest.Util;
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
        private IAdderSubtractor _8BitsAdderSuber;

        private Switches _16BitsNumber1Switches, _16BitsNumber2Switches;
        private IndicatorLight[] _16BitsResultLights;
        PowerSupplier _turnSub16Bits;
        private IndicatorLight _overflowUnderflowLight16Bits;
        private IAdderSubtractor _16BitsAdderSuber;

        public AdderSubtractorTest()
        {
            Init8Bits();
            Init16Bits();
        }

        private void Init8Bits()
        {
            _8BitsNumber1Switches = new Switches(8);
            _8BitsNumber2Switches = new Switches(8);
            Init8BitsLights();
            _turnSub8Bits = new PowerSupplier();
            _overflowUnderflowLight8Bits = new IndicatorLight();

            _8BitsAdderSuber = new AdderSubtractor<Adder8Bits, Complementor8Bits>();

            _8BitsAdderSuber.Number1Inputs.Connect(_8BitsNumber1Switches.Outputs);
            _8BitsAdderSuber.Number2Inputs.Connect(_8BitsNumber2Switches.Outputs);
            _8BitsAdderSuber.TurnSubstract.ConnectTo(_turnSub8Bits.Output);
            _8BitsAdderSuber.ResultOutputs.Connect(_8BitsResultLights.Select(l => l.Input as InputEndpoint));
            _8BitsAdderSuber.OverflowUnderflow.ConnectTo(_overflowUnderflowLight8Bits.Input);
        }

        private void Init16Bits()
        {
            _16BitsNumber1Switches = new Switches(16);
            _16BitsNumber2Switches = new Switches(16);
            Init16BitsLights();
            _turnSub16Bits = new PowerSupplier();
            _overflowUnderflowLight16Bits = new IndicatorLight();

            _16BitsAdderSuber = new AdderSubtractor<Adder16Bits, Complementor16Bits>();

            _16BitsAdderSuber.Number1Inputs.Connect(_16BitsNumber1Switches.Outputs);
            _16BitsAdderSuber.Number2Inputs.Connect(_16BitsNumber2Switches.Outputs);
            _16BitsAdderSuber.TurnSubstract.ConnectTo(_turnSub16Bits.Output);
            _16BitsAdderSuber.ResultOutputs.Connect(_16BitsResultLights.Select(l => l.Input as InputEndpoint));
            _16BitsAdderSuber.OverflowUnderflow.ConnectTo(_overflowUnderflowLight16Bits.Input);
        }

        private void Init8BitsLights()
        {
            _8BitsResultLights = new IndicatorLight[8];
            for (var i = 0; i < 8; i++)
            {
                _8BitsResultLights[i] = new IndicatorLight();
            }
        }

        private void Init16BitsLights()
        {
            _16BitsResultLights = new IndicatorLight[16];
            for (var i = 0; i < 16; i++)
            {
                _16BitsResultLights[i] = new IndicatorLight();
            }
        }

        private void Assert8BitsResultEquals(String binaryResult)
        {
            if (binaryResult.Length > 8) throw new ArgumentException("Result is too long.");

            _8BitsResultLights.AssertEquals(binaryResult);
        }

        private void Assert16BitsResultEquals(String binaryResult)
        {
            if (binaryResult.Length > 16) throw new ArgumentException("Result is too long.");

            _16BitsResultLights.AssertEquals(binaryResult);
        }

        [TestInitialize]
        public void TestInit()
        {
            _8BitsNumber1Switches.Clear();
            _8BitsNumber2Switches.Clear();
            _turnSub8Bits.Off();
            _16BitsNumber1Switches.Clear();
            _16BitsNumber2Switches.Clear();
            _turnSub16Bits.Off();
        }

        [TestCleanup]
        public void TestClean()
        {
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Bit width between adder and complementor are not match.")]
        public void CreateWithNonmatchedBitwidthMemberType()
        {
            IAdderSubtractor adderSuber = new AdderSubtractor<Adder8Bits, Complementor16Bits>();
        }

        #region 8 Bits
        [TestMethod]
        public void CreateNewFor8Bits()
        {
            IAdderSubtractor adderSuber = new AdderSubtractor<Adder8Bits, Complementor8Bits>();

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
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set());

            Assert8BitsResultEquals("1");
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Add0To1()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true));

            Assert8BitsResultEquals("1");
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Add1To1()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true));

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
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub8Bits.On());

            Assert8BitsResultEquals("1");
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Sub0By1()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub8Bits.On());

            Assert8BitsResultEquals("11111111"); //means -1
            Assert.IsTrue(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Sub1By1()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub8Bits.On());

            Assert8BitsResultEquals("0");
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Add10000000To10000000()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(false, false, false, false, false, false, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(false, false, false, false, false, false, false, true));
            Assert8BitsResultEquals("00000000");
            Assert.IsTrue(_overflowUnderflowLight8Bits.Lighting); //means 100000000
        }

        [TestMethod]
        public void Sub10101010By01010101()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(false, true, false, true, false, true, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true, false, true, false, true, false, true, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub8Bits.On());

            Assert8BitsResultEquals("01010101");
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Sub01010101By10101010()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true, false, true, false, true, false, true, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(false, true, false, true, false, true, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub8Bits.On());

            Assert8BitsResultEquals("10101011");  //means -01010101
            Assert.IsTrue(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Sub01011011By00010100()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true, true, false, true, true, false, true, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(false, false, true, false, true, false, false, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub8Bits.On());

            Assert8BitsResultEquals("01000111");
            Assert.IsFalse(_overflowUnderflowLight8Bits.Lighting);
        }

        [TestMethod]
        public void Sub00010100By01011011()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(false, false, true, false, true, false, false, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true, true, false, true, true, false, true, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub8Bits.On());

            Assert8BitsResultEquals("10111001"); //means -01000111
            Assert.IsTrue(_overflowUnderflowLight8Bits.Lighting);
        }
        #endregion

        #region 16 Bits
        [TestMethod]
        public void CreateNewFor16Bits()
        {
            IAdderSubtractor adderSuber = new AdderSubtractor<Adder16Bits, Complementor16Bits>();

            Assert.IsNotNull(adderSuber);
        }

        [TestMethod]
        public void Add0To0_16Bits()
        {
            _16BitsNumber1Switches.Set();
            _16BitsNumber2Switches.Set();

            Assert16BitsResultEquals("0");
            Assert.IsFalse(_overflowUnderflowLight16Bits.Lighting);
        }

        [TestMethod]
        public void Add1To0_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set());

            Assert16BitsResultEquals("1");
            Assert.IsFalse(_overflowUnderflowLight16Bits.Lighting);
        }

        [TestMethod]
        public void Add0To1_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(true));

            Assert16BitsResultEquals("1");
            Assert.IsFalse(_overflowUnderflowLight16Bits.Lighting);
        }

        [TestMethod]
        public void Add1To1_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(true));

            Assert16BitsResultEquals("10");
            Assert.IsFalse(_overflowUnderflowLight16Bits.Lighting);
        }

        [TestMethod]
        public void Sub0By0_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub16Bits.On());
            Assert16BitsResultEquals("0");
            Assert.IsFalse(_overflowUnderflowLight16Bits.Lighting);
        }

        [TestMethod]
        public void Sub1By0_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub16Bits.On());

            Assert16BitsResultEquals("1");
            Assert.IsFalse(_overflowUnderflowLight16Bits.Lighting);
        }

        [TestMethod]
        public void Sub0By1_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub16Bits.On());

            Assert16BitsResultEquals("1111111111111111"); //means -1
            Assert.IsTrue(_overflowUnderflowLight16Bits.Lighting);
        }

        [TestMethod]
        public void Sub1By1_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub16Bits.On());

            Assert16BitsResultEquals("0");
            Assert.IsFalse(_overflowUnderflowLight16Bits.Lighting);
        }

        [TestMethod]
        public void Add1000000000000000To1000000000000000_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set("1000000000000000"));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set("1000000000000000"));
            Assert16BitsResultEquals("0000000000000000");
            Assert.IsTrue(_overflowUnderflowLight16Bits.Lighting); //means 10000000000000000
        }

        [TestMethod]
        public void Sub1010101010101010By0101010101010101_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set("1010101010101010"));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set("0101010101010101"));
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub16Bits.On());

            Assert16BitsResultEquals("0101010101010101");
            Assert.IsFalse(_overflowUnderflowLight16Bits.Lighting);
        }

        [TestMethod]
        public void Sub0101010101010101By1010101010101010_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set("0101010101010101"));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set("1010101010101010"));
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub16Bits.On());                        

            Assert16BitsResultEquals("1010101010101011");  //means -0101010101010101
            Assert.IsTrue(_overflowUnderflowLight16Bits.Lighting);
        }

        [TestMethod]
        public void Sub0101101101011011By0001010011010100_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set("0101101101011011"));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set("0001010011010100"));
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub16Bits.On());

            Assert16BitsResultEquals("0100011010000111");
            Assert.IsFalse(_overflowUnderflowLight16Bits.Lighting);
        }

        [TestMethod]
        public void Sub0001010011010100By0101101101011011_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set("0001010011010100"));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set("0101101101011011"));
            ThreadHelper.ExecuteThenSleepShortly(()=>_turnSub16Bits.On());

            Assert16BitsResultEquals("1011100101111001"); //means -0100011010000111
            Assert.IsTrue(_overflowUnderflowLight16Bits.Lighting);
        }
        #endregion
    }
}
