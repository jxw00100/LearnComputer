using System;
using LearnComputer.CircuitInfrustructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ComputerTest.CircuitInfrustructure
{
    [TestClass]
    public class EndpointCollectionsTest
    {
        #region Endpoint Collection
        [TestMethod]
        public void EndpointCollectionCount()
        {
            IEndpointCollection<IEndpoint> inputCollection = new EndpointCollection<InputEndpoint>(8);
            Assert.AreEqual(inputCollection.Count, 8);
        }

        [TestMethod]
        public void EndpointCollectionEnumeration()
        {
            IEndpointCollection<IEndpoint> inputCollection = new EndpointCollection<InputEndpoint>(8);

            Int32 i = 0;
            foreach (var input in inputCollection)
            {
                i++;
            }
            Assert.AreEqual(i, 8);
        }

        [TestMethod]
        public void EndpointCollectionIndexer()
        {
            IEndpointCollection<IEndpoint> inputCollection = new EndpointCollection<InputEndpoint>(3);
            
            Assert.IsNotNull(inputCollection[0]);
            Assert.IsNotNull(inputCollection[1]);
            Assert.IsNotNull(inputCollection[2]);

            Assert.AreNotSame(inputCollection[0], inputCollection[1]);
            Assert.AreNotSame(inputCollection[0], inputCollection[2]);
            Assert.AreNotSame(inputCollection[1], inputCollection[2]);
        }

        [TestMethod]
        public void EndpointCollectionFilledByInput()
        {
            IEndpointCollection<IEndpoint> inputCollection = new EndpointCollection<InputEndpoint>(8);

            Assert.IsNotNull(inputCollection);

            foreach (var input in inputCollection)
            {
                Assert.IsNotNull(input);
                Assert.IsInstanceOfType(input, typeof(InputEndpoint));
                Assert.IsNull(input.ConnectedPoint);
            }
        }

        [TestMethod]
        public void EndpointCollectionFilledByOutput()
        {
            IEndpointCollection<IEndpoint> outputCollection = new EndpointCollection<OutputEndpoint>(7);

            Assert.IsNotNull(outputCollection);
            Assert.AreEqual(outputCollection.Count, 7);

            foreach (var output in outputCollection)
            {
                Assert.IsNotNull(output);
                Assert.IsInstanceOfType(output, typeof(OutputEndpoint));
                Assert.IsNull(output.ConnectedPoint);
            }
        }

        [TestMethod]
        public void EndpointCollectionFilledByNeutral()
        {
            IEndpointCollection<IEndpoint> neutralEndpoints = new EndpointCollection<NeutralEndpoint>(5);

            Assert.IsNotNull(neutralEndpoints);
            Assert.AreEqual(neutralEndpoints.Count, 5);

            foreach (var neutral in neutralEndpoints)
            {
                Assert.IsNotNull(neutral);
                Assert.IsInstanceOfType(neutral, typeof(NeutralEndpoint));
                Assert.IsNull(neutral.ConnectedPoint);
            }
        }

        [TestMethod]
        public void EndpointCollectionsConnection()
        {
            IEndpointCollection<IEndpoint> inputCollection = new EndpointCollection<InputEndpoint>(4);
            IEndpointCollection<IEndpoint> outputCollection = new EndpointCollection<OutputEndpoint>(4);

            inputCollection.Connect(outputCollection);

            for (var i = 0; i < 4; i++)
            {
                Assert.AreSame(inputCollection[i].ConnectedPoint, outputCollection[i]);
                Assert.AreSame(inputCollection[i], outputCollection[i].ConnectedPoint);
            }
        }

        [TestMethod]
        public void EndpointCollectionsConnectionPartial()
        {
            IEndpointCollection<IEndpoint> inputCollection = new EndpointCollection<InputEndpoint>(4);
            IEndpointCollection<IEndpoint> outputCollection = new EndpointCollection<OutputEndpoint>(2);

            inputCollection.Connect(outputCollection);

            Assert.AreSame(inputCollection[0].ConnectedPoint, outputCollection[0]);
            Assert.AreSame(inputCollection[0], outputCollection[0].ConnectedPoint);

            Assert.AreSame(inputCollection[1].ConnectedPoint, outputCollection[1]);
            Assert.AreSame(inputCollection[1], outputCollection[1].ConnectedPoint);

            Assert.IsNull(inputCollection[2].ConnectedPoint);

            Assert.IsNull(inputCollection[3].ConnectedPoint);
        }

        [TestMethod]
        public void EndpointCollectionConnectAt()
        {
            IEndpointCollection<IEndpoint> inputCollection = new EndpointCollection<InputEndpoint>(4);
            IOutputEndpoint output = new OutputEndpoint();

            inputCollection.ConnectAt(2, output);

            Assert.IsNull(inputCollection[0].ConnectedPoint);

            Assert.IsNull(inputCollection[1].ConnectedPoint);

            Assert.AreSame(inputCollection[2].ConnectedPoint, output);

            Assert.IsNull(inputCollection[3].ConnectedPoint);
        }

        [TestMethod]
        public void EndpointCollectionDisconnectAll()
        {
            IEndpointCollection<IEndpoint> inputCollection = new EndpointCollection<InputEndpoint>(4);
            IEndpointCollection<IEndpoint> outputCollection = new EndpointCollection<OutputEndpoint>(4);

            inputCollection.Connect(outputCollection);
            inputCollection.DisconnectAll();

            for (var i = 0; i < 4; i++)
            {
                Assert.IsNull(inputCollection[i].ConnectedPoint);
                Assert.IsNull(outputCollection[i].ConnectedPoint);
            }
        }

        [TestMethod]
        public void EndpointCollectionDisconnectAt()
        {
            IEndpointCollection<IEndpoint> inputCollection = new EndpointCollection<InputEndpoint>(4);
            IEndpointCollection<IEndpoint> outputCollection = new EndpointCollection<OutputEndpoint>(4);

            inputCollection.Connect(outputCollection);
            inputCollection.DisconnectAt(1);

            Assert.AreSame(inputCollection[0].ConnectedPoint, outputCollection[0]);
            Assert.AreSame(inputCollection[0], outputCollection[0].ConnectedPoint);

            Assert.IsNull(inputCollection[1].ConnectedPoint);
            Assert.IsNull(outputCollection[1].ConnectedPoint);

            Assert.AreSame(inputCollection[2].ConnectedPoint, outputCollection[2]);
            Assert.AreSame(inputCollection[2], outputCollection[2].ConnectedPoint);

            Assert.AreSame(inputCollection[3].ConnectedPoint, outputCollection[3]);
            Assert.AreSame(inputCollection[3], outputCollection[3].ConnectedPoint);
        }
        #endregion

        
    }
}
