using LearnComputer.CircuitInfrustructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.CircuitInfrustructure
{
    [TestClass]
    public class SimpleCircuitsTest
    {
        [TestMethod]
        public void PowerToLight()
        {
            PowerSupplier power = new PowerSupplier();
            IndicatorLight light = new IndicatorLight();

            power.Output.ConnectTo(light.Input);
            Assert.IsFalse(light.Lighting);

            power.On();
            Assert.IsTrue(light.Lighting);
            power.Off();
            Assert.IsFalse(light.Lighting);
            power.Toggle();
            Assert.IsTrue(light.Lighting);
            power.Toggle();
            Assert.IsFalse(light.Lighting);

            power.On();
            light.Input.DisconnectEndpoint();
            Assert.IsFalse(light.Lighting);
        }




        [TestMethod]
        public void PowerWireLight()
        {
            PowerSupplier power = new PowerSupplier();
            Wire wire = new Wire();
            IndicatorLight light = new IndicatorLight();
            
            wire.Connect(power.Output, light.Input);

            Assert.IsFalse(light.Lighting);

            power.On();
            Assert.IsTrue(light.Lighting);
            power.On();
            Assert.IsTrue(light.Lighting);
            power.Off();
            Assert.IsFalse(light.Lighting);
            power.Off();
            Assert.IsFalse(light.Lighting);
            power.Toggle();
            Assert.IsTrue(light.Lighting);
            power.Toggle();
            Assert.IsFalse(light.Lighting);
            power.Toggle();
            Assert.IsTrue(light.Lighting);
            power.Toggle();
            Assert.IsFalse(light.Lighting);
            
            power.On();
            Assert.IsTrue(light.Lighting);
            light.Input.DisconnectEndpoint();
            Assert.IsFalse(light.Lighting);
            power.On();
            Assert.IsFalse(light.Lighting);

        }
    }
}
