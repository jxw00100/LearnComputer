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
        private Complementor8Bits _cplmtor8;
        private Switches _switches8;
        private PowerSupplier _invertPwr;
        private IndicatorLight[] _resultLights8;

        public ComplementorTest()
        {
            _cplmtor8 = new Complementor8Bits();
            _switches8 = new Switches(8);
            _invertPwr = new PowerSupplier();
            InitResult8BitsLights();

            _cplmtor8.Inputs.Connect(_switches8.Outputs);
            _cplmtor8.Invert.ConnectTo(_invertPwr.Output);
            _cplmtor8.Outputs.Connect(_resultLights8.Select(l => l.Input as InputEndpoint));
        }

        private void InitResult8BitsLights()
        {
            _resultLights8 = new IndicatorLight[8];
            for (var i = 0; i < 8; i++)
            {
                _resultLights8[i] = new IndicatorLight();
            }
        }

        [TestInitialize]
        public void TestInit()
        {
            _switches8.Clear();
            _invertPwr.Off();
        }

        [TestCleanup]
        public void TestClean()
        {
        }

        [TestMethod]
        public void CreateNew8BitsComplementor()
        {
            Complementor8Bits cplmtor8 = new Complementor8Bits();
            Assert.IsNotNull(cplmtor8);
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
    }
}
