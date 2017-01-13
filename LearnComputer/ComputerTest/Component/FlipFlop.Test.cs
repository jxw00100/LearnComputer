using LearnComputer.CircuitInfrustructure;
using LearnComputer.Component;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.Component
{
    [TestClass]
    public class FlipFlopTest
    {
        private PowerSupplier _powerSet, _powerClear;
        private IndicatorLight _light;
        private FlipFlop _flipFlop;

        public FlipFlopTest()
        {
            _powerSet = new PowerSupplier();
            _powerClear = new PowerSupplier();
            _light = new IndicatorLight();
            _flipFlop = new FlipFlop();

            _flipFlop.Set.ConnectTo(_powerSet.Output);
            _flipFlop.Clear.ConnectTo(_powerClear.Output);
            _flipFlop.Output.ConnectTo(_light.Input);
        }

        [TestInitialize]
        public void TestInit()
        {
            _powerSet.Off();
            _powerClear.Off();
        }

        [TestCleanup]
        public void TestClean()
        {
        }

        [TestMethod]
        public void CreateNew()
        {
            Assert.IsNotNull(_flipFlop);
        }

        [TestMethod]
        public void AbleToSet()
        {
            _powerSet.On();
            Assert.IsTrue(_light.Lighting);
        }

        [TestMethod]
        public void AbleToClear()
        {
            _powerSet.On();
            _powerClear.On();
            Assert.IsFalse(_light.Lighting);
        }

        [TestMethod]
        public void SetIsLatched()
        {
            _powerSet.On();
            _powerSet.Off();
            Assert.IsTrue(_light.Lighting);

            _powerSet.On();
            _powerSet.Off();
            _powerSet.On();
            _powerSet.Off();
            Assert.IsTrue(_light.Lighting);
        }

        [TestMethod]
        public void ClearIsLatched()
        {
            _powerSet.On();
            _powerSet.Off();
            _powerClear.On();
            _powerClear.Off();
            Assert.IsFalse(_light.Lighting);

            _powerClear.Off();
            _powerClear.On();
            _powerClear.Off();
            _powerClear.Off();
            Assert.IsFalse(_light.Lighting);
        }
    }
}
