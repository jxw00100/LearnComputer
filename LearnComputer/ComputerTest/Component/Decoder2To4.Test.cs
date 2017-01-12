using System;
using ComputerTest.Util;
using LearnComputer.CircuitInfrustructure;
using LearnComputer.Component;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.CircuitInfrustructure
{
    [TestClass]
    public class Decoder2To4Test
    {
        private PowerSupplier _power1, _power2;
        private IndicatorLight _light11, _light10, _light01, _light00;
        private Decoder2To4 _decoder;

        public Decoder2To4Test()
        {
            _power1 = new PowerSupplier();
            _power2 = new PowerSupplier();
            _light11 = new IndicatorLight();
            _light10 = new IndicatorLight();
            _light01 = new IndicatorLight();
            _light00 = new IndicatorLight();

            _decoder = new Decoder2To4();
            _decoder.Input1.ConnectTo(_power1.Output);
            _decoder.Input2.ConnectTo(_power2.Output);
            _decoder.Output11.ConnectTo(_light11.Input);
            _decoder.Output10.ConnectTo(_light10.Input);
            _decoder.Output01.ConnectTo(_light01.Input);
            _decoder.Output00.ConnectTo(_light00.Input);
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
        public void Input00()
        {
            Assert.IsTrue(_light00.Lighting);
            Assert.IsFalse(_light01.Lighting);
            Assert.IsFalse(_light10.Lighting);
            Assert.IsFalse(_light11.Lighting);
        }

        [TestMethod]
        public void Input10()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_power1.On());
            ThreadHelper.ExecuteThenSleepShortly(()=>_power2.Off());

            Assert.IsFalse(_light00.Lighting);
            Assert.IsFalse(_light01.Lighting);
            Assert.IsTrue(_light10.Lighting);
            Assert.IsFalse(_light11.Lighting);
        }

        [TestMethod]
        public void Input01()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_power1.Off());
            ThreadHelper.ExecuteThenSleepShortly(()=>_power2.On());

            Assert.IsFalse(_light00.Lighting);
            Assert.IsTrue(_light01.Lighting);
            Assert.IsFalse(_light10.Lighting);
            Assert.IsFalse(_light11.Lighting);
        }

        [TestMethod]
        public void Input11()
        {
            ThreadHelper.ExecuteThenSleepShortly(()=>_power1.On());
            ThreadHelper.ExecuteThenSleepShortly(()=>_power2.On());

            Assert.IsFalse(_light00.Lighting);
            Assert.IsFalse(_light01.Lighting);
            Assert.IsFalse(_light10.Lighting);
            Assert.IsTrue(_light11.Lighting);
        }
    }
}
