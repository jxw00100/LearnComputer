using LearnComputer.CircuitInfrustructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.CircuitInfrustructure
{
    [TestClass]
    public class LogicGateTest
    {
        [TestMethod]
        public void AND()
        {
            PowerSupplier power1 = new PowerSupplier();
            PowerSupplier power2 = new PowerSupplier();
            ANDGate andGate = new ANDGate();
            IndicatorLight light = new IndicatorLight();

            andGate.Output.ConnectTo(light.Input);
            Assert.IsFalse(light.Lighting);

            andGate.Input1.ConnectTo(power1.Output);
            andGate.Input2.ConnectTo(power2.Output);
            Assert.IsFalse(light.Lighting);

            power1.On();
            power2.Off();
            Assert.IsFalse(light.Lighting);
            power1.Off();
            power2.On();
            Assert.IsFalse(light.Lighting);
            power1.On();
            power2.On();
            Assert.IsTrue(light.Lighting);
            power1.Off();
            power2.Off();
            Assert.IsFalse(light.Lighting);
        }

        [TestMethod]
        public void NAND()
        {
            PowerSupplier power1 = new PowerSupplier();
            PowerSupplier power2 = new PowerSupplier();
            NANDGate nandGate = new NANDGate();
            IndicatorLight light = new IndicatorLight();

            nandGate.Output.ConnectTo(light.Input);
            Assert.IsTrue(light.Lighting);

            nandGate.Input1.ConnectTo(power1.Output);
            nandGate.Input2.ConnectTo(power2.Output);
            Assert.IsTrue(light.Lighting);

            power1.On();
            power2.Off();
            Assert.IsTrue(light.Lighting);
            power1.Off();
            power2.On();
            Assert.IsTrue(light.Lighting);
            power1.On();
            power2.On();
            Assert.IsFalse(light.Lighting);
            power1.Off();
            power2.Off();
            Assert.IsTrue(light.Lighting);
        }

        [TestMethod]
        public void OR()
        {
            PowerSupplier power1 = new PowerSupplier();
            PowerSupplier power2 = new PowerSupplier();
            ORGate ORGate = new ORGate();
            IndicatorLight light = new IndicatorLight();

            ORGate.Output.ConnectTo(light.Input);
            Assert.IsFalse(light.Lighting);

            ORGate.Input1.ConnectTo(power1.Output);
            ORGate.Input2.ConnectTo(power2.Output);
            Assert.IsFalse(light.Lighting);

            power1.On();
            power2.Off();
            Assert.IsTrue(light.Lighting);
            power1.Off();
            power2.On();
            Assert.IsTrue(light.Lighting);
            power1.On();
            power2.On();
            Assert.IsTrue(light.Lighting);
            power1.Off();
            power2.Off();
            Assert.IsFalse(light.Lighting);
        }

        
    }
}
