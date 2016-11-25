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
            ORGate orGate = new ORGate();
            IndicatorLight light = new IndicatorLight();

            orGate.Output.ConnectTo(light.Input);
            Assert.IsFalse(light.Lighting);

            orGate.Input1.ConnectTo(power1.Output);
            orGate.Input2.ConnectTo(power2.Output);
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

        [TestMethod]
        public void NOR()
        {
            PowerSupplier power1 = new PowerSupplier();
            PowerSupplier power2 = new PowerSupplier();
            NORGate norGate = new NORGate();
            IndicatorLight light = new IndicatorLight();

            norGate.Output.ConnectTo(light.Input);
            Assert.IsTrue(light.Lighting);

            norGate.Input1.ConnectTo(power1.Output);
            norGate.Input2.ConnectTo(power2.Output);
            Assert.IsTrue(light.Lighting);

            power1.On();
            power2.Off();
            Assert.IsFalse(light.Lighting);
            power1.Off();
            power2.On();
            Assert.IsFalse(light.Lighting);
            power1.On();
            power2.On();
            Assert.IsFalse(light.Lighting);
            power1.Off();
            power2.Off();
            Assert.IsTrue(light.Lighting);
        }
        
        [TestMethod]
        public void XOR()
        {
            PowerSupplier power1 = new PowerSupplier();
            PowerSupplier power2 = new PowerSupplier();
            XORGate xorGate = new XORGate();
            IndicatorLight light = new IndicatorLight();

            xorGate.Output.ConnectTo(light.Input);
            Assert.IsFalse(light.Lighting);

            xorGate.Input1.ConnectTo(power1.Output);
            xorGate.Input2.ConnectTo(power2.Output);
            Assert.IsFalse(light.Lighting);

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
            Assert.IsFalse(light.Lighting);
        }

        [TestMethod]
        public void XNOR()
        {
            PowerSupplier power1 = new PowerSupplier();
            PowerSupplier power2 = new PowerSupplier();
            XNORGate xnorGate = new XNORGate();
            IndicatorLight light = new IndicatorLight();

            xnorGate.Output.ConnectTo(light.Input);
            Assert.IsTrue(light.Lighting);

            xnorGate.Input1.ConnectTo(power1.Output);
            xnorGate.Input2.ConnectTo(power2.Output);
            Assert.IsTrue(light.Lighting);

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
            Assert.IsTrue(light.Lighting);
        }
    }
}
