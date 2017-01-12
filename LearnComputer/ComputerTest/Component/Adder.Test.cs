using System;
using System.Linq;
using ComputerTest.Assist;
using ComputerTest.Util;
using LearnComputer.CircuitInfrustructure;
using LearnComputer.Component;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;

namespace ComputerTest.Component
{
    [TestClass]
    public class AdderTest
    {
        private Switches _8BitsNumber1Switches, _8BitsNumber2Switches, _16BitsNumber1Switches, _16BitsNumber2Switches;
        private IndicatorLight[] _8BitsSumLights, _16BitsSumLights;
        PowerSupplier _carryInPowerFor8Bits, _carryInPowerFor16Bits;
        private IndicatorLight _carryOutLightFor8Bits, _carryOutLightFor16Bits;
        private IAdderMultiBits _8BitsAdder, _16BitsAdder;

        public AdderTest()
        {
            Init8BitsTest();
            Init16BitsTest();
        }

        private void Init8BitsTest()
        {
            _8BitsNumber1Switches = new Switches(8);
            _8BitsNumber2Switches = new Switches(8);
            Init8BitsLights();
            _carryInPowerFor8Bits = new PowerSupplier();
            _carryOutLightFor8Bits = new IndicatorLight();

            _8BitsAdder = new Adder8Bits();

            _8BitsAdder.Number1Inputs.Connect(_8BitsNumber1Switches.Outputs);
            _8BitsAdder.Number2Inputs.Connect(_8BitsNumber2Switches.Outputs);
            _8BitsAdder.CarryInput.ConnectTo(_carryInPowerFor8Bits.Output);
            _8BitsAdder.SumOutputs.Connect(_8BitsSumLights.Select(l => l.Input as InputEndpoint));
            _8BitsAdder.CarryOutput.ConnectTo(_carryOutLightFor8Bits.Input);
        }

        private void Init16BitsTest()
        {
            _16BitsNumber1Switches = new Switches(16);
            _16BitsNumber2Switches = new Switches(16);
            Init16BitsLights();
            _carryInPowerFor16Bits = new PowerSupplier();
            _carryOutLightFor16Bits = new IndicatorLight();

            _16BitsAdder = new Adder16Bits();

            _16BitsAdder.Number1Inputs.Connect(_16BitsNumber1Switches.Outputs);
            _16BitsAdder.Number2Inputs.Connect(_16BitsNumber2Switches.Outputs);
            _16BitsAdder.CarryInput.ConnectTo(_carryInPowerFor16Bits.Output);
            _16BitsAdder.SumOutputs.Connect(_16BitsSumLights.Select(l => l.Input as InputEndpoint));
            _16BitsAdder.CarryOutput.ConnectTo(_carryOutLightFor16Bits.Input);
        }

        private void Init8BitsLights()
        {
            _8BitsSumLights = new IndicatorLight[8];
            for (var i = 0; i < 8; i++)
            {
                _8BitsSumLights[i] = new IndicatorLight();
            }
        }

        private void Init16BitsLights()
        {
            _16BitsSumLights = new IndicatorLight[16];
            for (var i = 0; i < 16; i++)
            {
                _16BitsSumLights[i] = new IndicatorLight();
            }
        }

        private void Assert8BitsResultEquals(String binaryResult)
        {
            if(binaryResult.Length > 9) throw new ArgumentException("Result is too long.");

            _8BitsSumLights.AssertEquals(binaryResult);
            AssertCarryEquals(_carryOutLightFor8Bits, 8, binaryResult);
        }

        private void Assert16BitsResultEquals(String binaryResult)
        {
            if (binaryResult.Length > 17) throw new ArgumentException("Result is too long.");

            _16BitsSumLights.AssertEquals(binaryResult);
            AssertCarryEquals(_carryOutLightFor16Bits, 16, binaryResult);
        }

        private void AssertCarryEquals(IndicatorLight carryLight, Int32 bits, String binaryResult)
        {
            Boolean carry = false;
            if (binaryResult.Length > bits)
            {
                carry = (binaryResult[0] == '1' ? true : false);
            }
            Assert.AreEqual(carryLight.Lighting, carry);
        }

        [TestInitialize]
        public void TestInit()
        {
            _8BitsNumber1Switches.Clear();
            _8BitsNumber2Switches.Clear();
            _16BitsNumber1Switches.Clear();
            _16BitsNumber2Switches.Clear();
            _carryInPowerFor8Bits.Off();
            _carryInPowerFor16Bits.Off();
        }

        [TestCleanup]
        public void TestClean()
        {
        }

        #region 8 Bits Adder
        [TestMethod]
        public void CreateNew8BitsAdder()
        {
            Adder8Bits adder = new Adder8Bits();
            Assert.IsNotNull(adder);
        }

        [TestMethod]
        public void MakeSureBitWidthOf8BitsAdder()
        {
            Assert.AreEqual(_8BitsAdder.BitWidth, 8);
        }

        [TestMethod]
        public void Add0To0()
        {
            _8BitsNumber1Switches.Set();
            _8BitsNumber2Switches.Set();
            Assert8BitsResultEquals("0");
        }

        [TestMethod]
        public void Add1To0()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set());
            Assert8BitsResultEquals("1");
        }

        [TestMethod]
        public void Add0To1()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true));
            Assert8BitsResultEquals("1");
        }

        [TestMethod]
        public void Add1To1()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true));
            Assert8BitsResultEquals("10");
        }

        [TestMethod]
        public void Add0To0WithCarryIn()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor8Bits.On());
            Assert8BitsResultEquals("1");
        }

        [TestMethod]
        public void Add1To0WithCarryIn()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor8Bits.On());
            Assert8BitsResultEquals("10");
        }

        [TestMethod]
        public void Add0To1WithCarryIn()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor8Bits.On());
            Assert8BitsResultEquals("10");
        }

        [TestMethod]
        public void Add1To1WithCarryIn()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor8Bits.On());
            Assert8BitsResultEquals("11");
        }

        [TestMethod]
        public void Add11To00()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(false, false));
            Assert8BitsResultEquals("11");
        }

        [TestMethod]
        public void Add00To11()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(false, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true, true));
            Assert8BitsResultEquals("11");
        }

        [TestMethod]
        public void Add00110101To01001001()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true, false, true, false, true, true, false, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true, false, false, true, false, false, true, false));
            Assert8BitsResultEquals("01111110");
        }

        [TestMethod]
        public void Add00011101To00001011()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true, false, true, true, true, false, false, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true, true, false, true, false, false, false, false));
            Assert8BitsResultEquals("00101000");
        }

        [TestMethod]
        public void Add10000000To10000000()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(false, false, false, false, false, false, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(false, false, false, false, false, false, false, true));
            Assert8BitsResultEquals("100000000");
        }

        [TestMethod]
        public void Add11111111To00000000WithCarryIn()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true, true, true, true, true, true, true, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(false, false, false, false, false, false, false, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor8Bits.On());
            Assert8BitsResultEquals("100000000");
        }

        [TestMethod]
        public void Add00000000To11111111WithCarryIn()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(false, false, false, false, false, false, false, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true, true, true, true, true, true, true, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor8Bits.On());
            Assert8BitsResultEquals("100000000");
        }

        [TestMethod]
        public void Add10101010To01010101()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true, false, true, false, true, false, true, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(false, true, false, true, false, true, false, true));
            Assert8BitsResultEquals("11111111");
        }

        [TestMethod]
        public void Add01010101To10101010()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(false, true, false, true, false, true, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true, false, true, false, true, false, true, false));
            Assert8BitsResultEquals("11111111");
        }

        [TestMethod]
        public void Add10101010To01010101WithCarryIn()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true, false, true, false, true, false, true, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(false, true, false, true, false, true, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor8Bits.On());
            Assert8BitsResultEquals("100000000");
        }

        [TestMethod]
        public void Add01010101To10101010WithCarryIn()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(false, true, false, true, false, true, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true, false, true, false, true, false, true, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor8Bits.On());
            Assert8BitsResultEquals("100000000");
        }

        [TestMethod]
        public void Add10110101To11101110()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true, false, true, false, true, true, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(false, true, true, true, false, true, true, true));
            Assert8BitsResultEquals("110100011");
        }

        [TestMethod]
        public void Add11111111To11111111()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true, true, true, true, true, true, true, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true, true, true, true, true, true, true, true));
            Assert8BitsResultEquals("111111110");
        }

        [TestMethod]
        public void Add11111111To11111111WithCarry()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber1Switches.Set(true, true, true, true, true, true, true, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_8BitsNumber2Switches.Set(true, true, true, true, true, true, true, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor8Bits.On());
            Assert8BitsResultEquals("111111111");
        }
        #endregion

        #region 16 Bits Adder
        [TestMethod]
        public void CreateNew16BitsAdder()
        {
            Adder16Bits adder = new Adder16Bits();
            Assert.IsNotNull(adder);
        }

        [TestMethod]
        public void MakeSureBitWidthOf16BitsAdder()
        {
            Assert.AreEqual(_16BitsAdder.BitWidth, 16);
        }

        [TestMethod]
        public void Add0To0_16Bits()
        {
            _16BitsNumber1Switches.Set();
            _16BitsNumber2Switches.Set();
            Assert16BitsResultEquals("0");
        }

        [TestMethod]
        public void Add1To0_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set());
            Assert16BitsResultEquals("1");
        }

        [TestMethod]
        public void Add0To1_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(true));
            Assert16BitsResultEquals("1");
        }

        [TestMethod]
        public void Add1To1_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(true));
            Assert16BitsResultEquals("10");
        }

        [TestMethod]
        public void Add0To0WithCarryIn_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor16Bits.On());
            Assert16BitsResultEquals("1");
        }

        [TestMethod]
        public void Add1To0WithCarryIn_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor16Bits.On());
            Assert16BitsResultEquals("10");
        }

        [TestMethod]
        public void Add0To1WithCarryIn_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set());
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor16Bits.On());
            Assert16BitsResultEquals("10");
        }

        [TestMethod]
        public void Add1To1WithCarryIn_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor16Bits.On());
            Assert16BitsResultEquals("11");
        }

        [TestMethod]
        public void Add10000000To10000000_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(false, false, false, false, false, false, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(false, false, false, false, false, false, false, true));
            Assert16BitsResultEquals("100000000");
        }

        [TestMethod]
        public void Add11111111To00000000WithCarryIn_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(true, true, true, true, true, true, true, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(false, false, false, false, false, false, false, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor16Bits.On());
            Assert16BitsResultEquals("100000000");
        }

        [TestMethod]
        public void Add00000000To11111111WithCarryIn_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(false, false, false, false, false, false, false, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(true, true, true, true, true, true, true, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor16Bits.On());
            Assert16BitsResultEquals("100000000");
        }

        [TestMethod]
        public void Add1000000000000000To1000000000000000_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, true));
            Assert16BitsResultEquals("10000000000000000");
        }

        [TestMethod]
        public void Add1111111111111111To0000000000000000WithCarryIn_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor16Bits.On());
            Assert16BitsResultEquals("10000000000000000");
        }

        [TestMethod]
        public void Add0000000000000000To1111111111111111WithCarryIn_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor16Bits.On());
            Assert16BitsResultEquals("10000000000000000");
        }

        [TestMethod]
        public void Add1010101010101010To0101010101010101_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true));
            Assert16BitsResultEquals("1111111111111111");
        }

        [TestMethod]
        public void Add0101010101010101To1010101010101010_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false));
            Assert16BitsResultEquals("1111111111111111");
        }

        [TestMethod]
        public void Add10101010To01010101WithCarryIn_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(true, false, true, false, true, false, true, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(false, true, false, true, false, true, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor16Bits.On());
            Assert16BitsResultEquals("100000000");
        }

        [TestMethod]
        public void Add01010101To10101010WithCarryIn_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(false, true, false, true, false, true, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(true, false, true, false, true, false, true, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor16Bits.On());
            Assert16BitsResultEquals("100000000");
        }

        [TestMethod]
        public void Add1010101010101010To0101010101010101WithCarryIn_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor16Bits.On());
            Assert16BitsResultEquals("10000000000000000");
        }

        [TestMethod]
        public void Add0101010101010101To1010101010101010WithCarryIn_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(false, true, false, true, false, true, false, true, false, true, false, true, false, true, false, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(true, false, true, false, true, false, true, false, true, false, true, false, true, false, true, false));
            ThreadHelper.ExecuteThenSleepShortly(()=>_carryInPowerFor16Bits.On());
            Assert16BitsResultEquals("10000000000000000");
        }

        [TestMethod]
        public void Add1101010101010111To1011101011100010_16Bits()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber1Switches.Set(true, true, true, false, true, false, true, false, true, false, true, false, true, false, true, true));
            ThreadHelper.ExecuteThenSleepShortly(()=>_16BitsNumber2Switches.Set(false, true, false, false, false, true, true, true, false, true, false, true, true, true, false, true));
            Assert16BitsResultEquals("11001000000111001");
        }
        #endregion
    }
}
