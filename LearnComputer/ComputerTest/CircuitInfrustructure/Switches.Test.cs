using System;
using LearnComputer.CircuitInfrustructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.CircuitInfrustructure
{
    [TestClass]
    public class SwitchesTest
    {
        Switches8Bits _switches = new Switches8Bits();
        IndicatorLight _light1 = new IndicatorLight();
        IndicatorLight _light2 = new IndicatorLight();
        IndicatorLight _light3 = new IndicatorLight();
        IndicatorLight _light4 = new IndicatorLight();
        IndicatorLight _light5 = new IndicatorLight();
        IndicatorLight _light6 = new IndicatorLight();
        IndicatorLight _light7 = new IndicatorLight();
        IndicatorLight _light8 = new IndicatorLight();

        public SwitchesTest()
        {
            _switches.Connect(_light1.Input,
                              _light2.Input,
                              _light3.Input,
                              _light4.Input,
                              _light5.Input,
                              _light6.Input,
                              _light7.Input,
                              _light8.Input);
        }

        [TestInitialize]
        public void TestInit()
        {
            _switches.Clear();
        }

        [TestCleanup]
        public void TestClean()
        {
        }

        [TestMethod]
        public void DefaultInitialize()
        {
            Switches8Bits switches = new Switches8Bits();
            Assert.IsNotNull(switches);
        }

        [TestMethod]
        public void InitializeWithFullSettings()
        {
            Switches8Bits switches = new Switches8Bits(false, true, false, true, true, false, true, false);
            Assert.IsNotNull(switches);
            Assert.AreEqual(switches.Outputs[0].LastSentSignal, 0);
            Assert.AreEqual(switches.Outputs[1].LastSentSignal, 1);
            Assert.AreEqual(switches.Outputs[2].LastSentSignal, 0);
            Assert.AreEqual(switches.Outputs[3].LastSentSignal, 1);
            Assert.AreEqual(switches.Outputs[4].LastSentSignal, 1);
            Assert.AreEqual(switches.Outputs[5].LastSentSignal, 0);
            Assert.AreEqual(switches.Outputs[6].LastSentSignal, 1);
            Assert.AreEqual(switches.Outputs[7].LastSentSignal, 0);
        }

        [TestMethod]
        public void InitializeWithNotFullSettings()
        {
            Switches8Bits switches = new Switches8Bits(false, true, false, true);
            Assert.IsNotNull(switches);
            Assert.AreEqual(switches.Outputs[0].LastSentSignal, 0);
            Assert.AreEqual(switches.Outputs[1].LastSentSignal, 1);
            Assert.AreEqual(switches.Outputs[2].LastSentSignal, 0);
            Assert.AreEqual(switches.Outputs[3].LastSentSignal, 1);
            Assert.AreEqual(switches.Outputs[4].LastSentSignal, 0);
            Assert.AreEqual(switches.Outputs[5].LastSentSignal, 0);
            Assert.AreEqual(switches.Outputs[6].LastSentSignal, 0);
            Assert.AreEqual(switches.Outputs[7].LastSentSignal, 0);
        }

        [TestMethod]
        public void SetFull()
        {
            _switches.Set(false, true, false, true, true, false, true, false);

            Assert.IsFalse(_light1.Lighting);
            Assert.IsTrue(_light2.Lighting);
            Assert.IsFalse(_light3.Lighting);
            Assert.IsTrue(_light4.Lighting);
            Assert.IsTrue(_light5.Lighting);
            Assert.IsFalse(_light6.Lighting);
            Assert.IsTrue(_light7.Lighting);
            Assert.IsFalse(_light8.Lighting);
        }

        [TestMethod]
        public void SetNotFull()
        {
            _switches.Set(false, true, false, true);

            Assert.IsFalse(_light1.Lighting);
            Assert.IsTrue(_light2.Lighting);
            Assert.IsFalse(_light3.Lighting);
            Assert.IsTrue(_light4.Lighting);
            Assert.IsFalse(_light5.Lighting);
            Assert.IsFalse(_light6.Lighting);
            Assert.IsFalse(_light7.Lighting);
            Assert.IsFalse(_light8.Lighting);
        }

        [TestMethod]
        public void Clear()
        {
            _switches.Set(true, true, true, true, true, true, true, true);

            Assert.IsTrue(_light1.Lighting);
            Assert.IsTrue(_light2.Lighting);
            Assert.IsTrue(_light3.Lighting);
            Assert.IsTrue(_light4.Lighting);
            Assert.IsTrue(_light5.Lighting);
            Assert.IsTrue(_light6.Lighting);
            Assert.IsTrue(_light7.Lighting);
            Assert.IsTrue(_light8.Lighting);

            _switches.Clear();

            Assert.IsFalse(_light1.Lighting);
            Assert.IsFalse(_light2.Lighting);
            Assert.IsFalse(_light3.Lighting);
            Assert.IsFalse(_light4.Lighting);
            Assert.IsFalse(_light5.Lighting);
            Assert.IsFalse(_light6.Lighting);
            Assert.IsFalse(_light7.Lighting);
            Assert.IsFalse(_light8.Lighting);
        }
    }
}
