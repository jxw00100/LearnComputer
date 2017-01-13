using LearnComputer.CircuitInfrustructure;
using LearnComputer.Component;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.Component
{
    [TestClass]
    public class RSFlipFlopTest
    {
        private PowerSupplier _powerSet, _powerReset;
        private IndicatorLight _lightSet, _lightReset;
        private RSFlipFlop _RS;

        public RSFlipFlopTest()
        {
            _powerSet = new PowerSupplier();
            _powerReset = new PowerSupplier();
            _lightSet = new IndicatorLight();
            _lightReset = new IndicatorLight();
            _RS = new RSFlipFlop();

            _RS.Set.ConnectTo(_powerSet.Output);
            _RS.Reset.ConnectTo(_powerReset.Output);
            _RS.Q.ConnectTo(_lightSet.Input);
            _RS.QBar.ConnectTo(_lightReset.Input);
        }

        [TestInitialize]
        public void TestInit()
        {
            _powerSet.Off();
            _powerReset.Off();
        }

        [TestCleanup]
        public void TestClean()
        {
        }

        [TestMethod]
        public void CreateNew()
        {
            Assert.IsNotNull(_RS);
        }

        [TestMethod]
        public void AbleToSet()
        {
            _powerSet.On();
            Assert.IsTrue(_lightSet.Lighting);
            Assert.IsFalse(_lightReset.Lighting);
        }

        [TestMethod]
        public void AbleToReset()
        {
            _powerReset.On();
            Assert.IsFalse(_lightSet.Lighting);
            Assert.IsTrue(_lightReset.Lighting);
        }

        [TestMethod]
        public void SetIsLatched()
        {
            _powerSet.On();
            Assert.IsTrue(_lightSet.Lighting);
            Assert.IsFalse(_lightReset.Lighting);

            _powerSet.Off();
            Assert.IsTrue(_lightSet.Lighting);
            Assert.IsFalse(_lightReset.Lighting);

            _powerSet.On();
            _powerSet.Off();
            _powerSet.On();
            _powerSet.Off();
            Assert.IsTrue(_lightSet.Lighting);
            Assert.IsFalse(_lightReset.Lighting);
        }

        [TestMethod]
        public void ResetIsLatched()
        {
            _powerReset.On();
            Assert.IsFalse(_lightSet.Lighting);
            Assert.IsTrue(_lightReset.Lighting);

            _powerReset.Off();
            Assert.IsFalse(_lightSet.Lighting);
            Assert.IsTrue(_lightReset.Lighting);

            _powerReset.On();
            _powerReset.Off();
            _powerReset.On();
            _powerReset.Off();
            Assert.IsFalse(_lightSet.Lighting);
            Assert.IsTrue(_lightReset.Lighting);
        }
    }
}
