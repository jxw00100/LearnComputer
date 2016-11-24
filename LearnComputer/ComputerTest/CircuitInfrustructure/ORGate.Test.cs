using LearnComputer.CircuitInfrustructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.CircuitInfrustructure
{
    [TestClass]
    public class ORGateTest
    {
        [TestMethod]
        public void Normal()
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
