using System.Linq;
using ComputerTest.Assist;
using LearnComputer.CircuitInfrustructure;
using LearnComputer.Component;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.Component
{
    [TestClass]
    public class ComplementorTest
    {
        private IComplementor _cplmtor8;
        private Switches _switches8;
        private PowerSupplier _invertPwr;
        private IndicatorLight[] _resultLights8;

        private IComplementor _cplmtor16;
        private Switches _switches16;
        private PowerSupplier _invertPwr16;
        private IndicatorLight[] _resultLights16;

        public ComplementorTest()
        {
            Init8BitsComplementor();
            Init16BitsComplementor();
        }

        private void Init8BitsComplementor()
        {
            _cplmtor8 = new Complementor8Bits();
            _switches8 = new Switches(8);
            _invertPwr = new PowerSupplier();
            InitResult8BitsLights();

            _cplmtor8.Inputs.Connect(_switches8.Outputs);
            _cplmtor8.Invert.ConnectTo(_invertPwr.Output);
            _cplmtor8.Outputs.Connect(_resultLights8.Select(l => l.Input as InputEndpoint));
        }

        private void Init16BitsComplementor()
        {
            _cplmtor16 = new Complementor16Bits();
            _switches16 = new Switches(16);
            _invertPwr16 = new PowerSupplier();
            InitResult16BitsLights();

            _cplmtor16.Inputs.Connect(_switches16.Outputs);
            _cplmtor16.Invert.ConnectTo(_invertPwr16.Output);
            _cplmtor16.Outputs.Connect(_resultLights16.Select(l => l.Input as InputEndpoint));
        }

        private void InitResult8BitsLights()
        {
            _resultLights8 = new IndicatorLight[8];
            for (var i = 0; i < 8; i++)
            {
                _resultLights8[i] = new IndicatorLight();
            }
        }

        private void InitResult16BitsLights()
        {
            _resultLights16 = new IndicatorLight[16];
            for (var i = 0; i < 16; i++)
            {
                _resultLights16[i] = new IndicatorLight();
            }
        }

        [TestInitialize]
        public void TestInit()
        {
            _switches8.Clear();
            _switches16.Clear();
            _invertPwr.Off();
            _invertPwr16.Off();
        }

        [TestCleanup]
        public void TestClean()
        {
        }

        #region 8 Bits
        [TestMethod]
        public void CreateNew8BitsComplementor()
        {
            Complementor8Bits cplmtor8 = new Complementor8Bits();
            Assert.IsNotNull(cplmtor8);
        }

        [TestMethod]
        public void MakeSureBitWidthOf8BitsComplementor()
        {
            Assert.AreEqual(_cplmtor8.BitWidth, 8);
        }

        [TestMethod]
        public void Keep00000000()
        {
            _switches8.Set();
            _resultLights8.AssertEquals("00000000");
        }

        [TestMethod]
        public void Keep11111111()
        {
            _switches8.Set(true, true, true, true, true, true, true, true);
            _resultLights8.AssertEquals("11111111");
        }

        [TestMethod]
        public void Complement00000000()
        {
            _switches8.Set();
            _invertPwr.On();
            _resultLights8.AssertEquals("11111111");
        }

        [TestMethod]
        public void Complement11111111()
        {
            _switches8.Set(true, true, true, true, true, true, true, true);
            _invertPwr.On();
            _resultLights8.AssertEquals("00000000");
        }

        [TestMethod]
        public void Complement01010101()
        {
            _switches8.Set(true, false, true, false, true, false, true, false);
            _invertPwr.On();
            _resultLights8.AssertEquals("10101010");
        }

        [TestMethod]
        public void Complement10101010()
        {
            _switches8.Set(false, true, false, true, false, true, false, true);
            _invertPwr.On();
            _resultLights8.AssertEquals("01010101");
        }

        [TestMethod]
        public void Complement11010111()
        {
            _switches8.Set(true, true, true, false, true, false, true, true);
            _invertPwr.On();
            _resultLights8.AssertEquals("00101000");
        }
        #endregion

        #region 16 Bits
        [TestMethod]
        public void CreateNew16BitsComplementor()
        {
            Complementor16Bits cplmtor = new Complementor16Bits();
            Assert.IsNotNull(cplmtor);
        }

        [TestMethod]
        public void MakeSureBitWidthOf16BitsComplementor()
        {
            Assert.AreEqual(_cplmtor16.BitWidth, 16);
        }

        [TestMethod]
        public void Keep0000000000000000_For16Bits()
        {
            _switches16.Set();
            _resultLights16.AssertEquals("0000000000000000");
        }

        [TestMethod]
        public void Keep1111111111111111_For16Bits()
        {
            _switches16.Set("1111111111111111");
            _resultLights16.AssertEquals("1111111111111111");
        }

        [TestMethod]
        public void Invert0000000000000000_For16Bits()
        {
            _switches16.Set();
            _invertPwr16.On();
            _resultLights16.AssertEquals("1111111111111111");
        }

        [TestMethod]
        public void Invert1111111111111111_For16Bits()
        {
            _switches16.Set("1111111111111111");
            _invertPwr16.On();
            _resultLights16.AssertEquals("0000000000000000");
        }

        

        [TestMethod]
        public void Invert0101010101010101_For16Bits()
        {
            _switches16.Set("0101010101010101");
            _invertPwr16.On();
            _resultLights16.AssertEquals("1010101010101010");
        }

        [TestMethod]
        public void Invert1010101010101010_For16Bits()
        {
            _switches16.Set("1010101010101010");
            _invertPwr16.On();
            _resultLights16.AssertEquals("0101010101010101");
        }
        #endregion
    }
}
