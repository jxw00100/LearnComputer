using LearnComputer.CircuitInfrustructure;
using LearnComputer.Component;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.Component
{
    [TestClass]
    public class DTypeFlipFlopTest
    {
        private PowerSupplier _powerDataLevelTriggered, _powerClockLevelTriggered;
        private IndicatorLight _lightQLevelTriggered, _lightQBarLevelTriggered;
        private IDTypeFlipFlop _DTypeFlipFlopLevelTriggered;

        private PowerSupplier _powerDataEdgeTriggered, _powerEdgeTriggered;
        private IndicatorLight _lightSetEdgeTriggered, _lightResetEdgeTriggered;
        private IDTypeFlipFlop _DTypeFlipFlopEdgeTriggered;

        public DTypeFlipFlopTest()
        {
            _powerDataLevelTriggered = new PowerSupplier();
            _powerClockLevelTriggered = new PowerSupplier();
            _lightQLevelTriggered = new IndicatorLight();
            _lightQBarLevelTriggered = new IndicatorLight();
            _DTypeFlipFlopLevelTriggered = new DTypeFlipFlopLevelTriggered();

            _DTypeFlipFlopLevelTriggered.Data.ConnectTo(_powerDataLevelTriggered.Output);
            _DTypeFlipFlopLevelTriggered.Clock.ConnectTo(_powerClockLevelTriggered.Output);
            _DTypeFlipFlopLevelTriggered.Q.ConnectTo(_lightQLevelTriggered.Input);
            _DTypeFlipFlopLevelTriggered.QBar.ConnectTo(_lightQBarLevelTriggered.Input);
        }

        [TestInitialize]
        public void TestInit()
        {
            _powerDataLevelTriggered.Off();
            _powerClockLevelTriggered.Off();
        }

        [TestCleanup]
        public void TestClean()
        {
        }

        [TestMethod]
        public void CreateNew()
        {
            Assert.IsNotNull(_DTypeFlipFlopLevelTriggered);
        }

        [TestMethod]
        public void EnableSetData()
        {
            _powerDataLevelTriggered.On();
            _powerClockLevelTriggered.On();

            Assert.IsTrue(_lightQLevelTriggered.Lighting);
            Assert.IsFalse(_lightQBarLevelTriggered.Lighting);

            _powerDataLevelTriggered.Off();
            _powerClockLevelTriggered.On();

            Assert.IsFalse(_lightQLevelTriggered.Lighting);
            Assert.IsTrue(_lightQBarLevelTriggered.Lighting);
        }

        [TestMethod]
        public void DisableSetData()
        {
            _powerDataLevelTriggered.On();
            _powerClockLevelTriggered.On();

            Assert.IsTrue(_lightQLevelTriggered.Lighting);
            Assert.IsFalse(_lightQBarLevelTriggered.Lighting);

            _powerClockLevelTriggered.Off();
            _powerDataLevelTriggered.Off();

            Assert.IsTrue(_lightQLevelTriggered.Lighting);
            Assert.IsFalse(_lightQBarLevelTriggered.Lighting);

            _powerDataLevelTriggered.On();
            Assert.IsTrue(_lightQLevelTriggered.Lighting);
            Assert.IsFalse(_lightQBarLevelTriggered.Lighting);

            _powerDataLevelTriggered.Off();

            Assert.IsTrue(_lightQLevelTriggered.Lighting);
            Assert.IsFalse(_lightQBarLevelTriggered.Lighting);

            _powerDataLevelTriggered.On();
            Assert.IsTrue(_lightQLevelTriggered.Lighting);
            Assert.IsFalse(_lightQBarLevelTriggered.Lighting);
        }
    }
}
