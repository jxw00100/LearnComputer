using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
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

            Assert.IsInstanceOfType(inputCollection[0], typeof (IEndpoint));
            Assert.IsInstanceOfType(inputCollection[1], typeof(IEndpoint));
            Assert.IsInstanceOfType(inputCollection[2], typeof(IEndpoint));

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

        #region Input Endpoint Collection
        [TestMethod]
        public void InputEndpointCollectionIndexer()
        {
            IInputEndpointCollection<IInputEndpoint> inputCollection = new InputEndpointCollection<InputEndpoint>(3);
            
            Assert.IsNotNull(inputCollection[0]);
            Assert.IsNotNull(inputCollection[1]);
            Assert.IsNotNull(inputCollection[2]);

            Assert.IsInstanceOfType(inputCollection[0], typeof(InputEndpoint));
            Assert.IsInstanceOfType(inputCollection[1], typeof(InputEndpoint));
            Assert.IsInstanceOfType(inputCollection[2], typeof(InputEndpoint));

            Assert.AreNotSame(inputCollection[0], inputCollection[1]);
            Assert.AreNotSame(inputCollection[0], inputCollection[2]);
            Assert.AreNotSame(inputCollection[1], inputCollection[2]);
        }

        [TestMethod]
        public void InputEndpointCollectionCreateNew()
        {
            IInputEndpointCollection<IInputEndpoint> inputCollection = new InputEndpointCollection<InputEndpoint>(8);

            Assert.IsNotNull(inputCollection);

            foreach (var input in inputCollection)
            {
                Assert.IsNotNull(input);
                Assert.IsInstanceOfType(input, typeof(InputEndpoint));
                Assert.IsNull(input.ConnectedPoint);
            }
        }

        [TestMethod]
        public void InputEndpointCollectionRegisterSingleReceiveHandler()
        {
            var receivedCount = 0;
            ReceiveSignalHanlder receiveHandler = (sender, signal) => { receivedCount++; };
            IInputEndpointCollection<IInputEndpoint> inputCollection = new InputEndpointCollection<InputEndpoint>(5);
            inputCollection.RegisterReceiveHandler(receiveHandler);

            var i = 0;
            foreach (var input in inputCollection)
            {
                input.Transmit(0);
                i++;
                Assert.AreEqual(receivedCount, i);
            }
        }

        [TestMethod]
        public void InputEndpointCollectionRegisterReceiveHandlers()
        {
            Int32
                receiveSignal1 = 0,
                receiveSignal2 = 0,
                receiveSignal3 = 0,
                receiveSignal4 = 0;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            IInputEndpointCollection<IInputEndpoint> inputCollection = new InputEndpointCollection<InputEndpoint>(4);
            inputCollection.RegisterReceiveHandler(handlers);

            foreach (var input in inputCollection)
            {
                input.Transmit(1);
            }

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Receive signal handlers count are not match to endpoints count.")]
        public void InputEndpointCollectionRegisterReceiveHandlersWithUnmatchedCount()
        {
            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => {},
                (sender, signal) => {},
                (sender, signal) => {},
            };

            IInputEndpointCollection<IInputEndpoint> inputCollection = new InputEndpointCollection<InputEndpoint>(5);
            inputCollection.RegisterReceiveHandler(handlers);
        }

        [TestMethod]
        public void InputEndpointCollectionTransimitAllWithSameValue()
        {
            Int32
                receiveSignal1 = 0,
                receiveSignal2 = 0,
                receiveSignal3 = 0,
                receiveSignal4 = 0;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            IInputEndpointCollection<IInputEndpoint> inputCollection = new InputEndpointCollection<InputEndpoint>(4);
            inputCollection.RegisterReceiveHandler(handlers);

            inputCollection.TransmitAll(new Int32[] {1, 1, 1, 1});

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);

            inputCollection.TransmitAll(new Int32[] { 0, 0, 0, 0 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void InputEndpointCollectionTransimitAllWithInsufficientValues()
        {
            
            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            IInputEndpointCollection<IInputEndpoint> inputCollection = new InputEndpointCollection<InputEndpoint>(4);
            inputCollection.RegisterReceiveHandler(handlers);

            inputCollection.TransmitAll(new Int32[] { 1, 0, 1});

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 0);

            inputCollection.TransmitAll(new Int32[] { 0 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void InputEndpointCollectionTransimitAllWithSuperfluousValues()
        {

            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            IInputEndpointCollection<IInputEndpoint> inputCollection = new InputEndpointCollection<InputEndpoint>(4);
            inputCollection.RegisterReceiveHandler(handlers);

            inputCollection.TransmitAll(new Int32[] {0, 0, 1, 1, 1, 0, 1});

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);
        }

        [TestMethod]
        public void InputEndpointCollectionTransimitAllWithEmptyValue()
        {

            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            IInputEndpointCollection<IInputEndpoint> inputCollection = new InputEndpointCollection<InputEndpoint>(4);
            inputCollection.RegisterReceiveHandler(handlers);

            inputCollection.TransmitAll();

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void InputEndpointCollectionTransimitAllWithVariousValues()
        {
            Int32
                receiveSignal1 = 0,
                receiveSignal2 = 0,
                receiveSignal3 = 0,
                receiveSignal4 = 0;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            IInputEndpointCollection<IInputEndpoint> inputCollection = new InputEndpointCollection<InputEndpoint>(4);
            inputCollection.RegisterReceiveHandler(handlers);

            inputCollection.TransmitAll(new Int32[] { 1, 0, 1, 0 });

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 0);

            inputCollection.TransmitAll(new Int32[] { 0, 1, 0, 1 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 1);

            inputCollection.TransmitAll(new Int32[] { 1, 0, 0, 1 });

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 1);

            inputCollection.TransmitAll(new Int32[] { 0, 1, 1, 0 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void InputEndpointCollectionTransimitAt()
        {
            Int32
                receiveSignal1 = 0,
                receiveSignal2 = 0,
                receiveSignal3 = 0,
                receiveSignal4 = 0;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            IInputEndpointCollection<IInputEndpoint> inputCollection = new InputEndpointCollection<InputEndpoint>(4);
            inputCollection.RegisterReceiveHandler(handlers);

            inputCollection.TransmitAt(1, 1);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);

            inputCollection.TransmitAt(3, 1);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 1);

            inputCollection.TransmitAt(2, 1);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);

            inputCollection.TransmitAt(1, 0);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);
        }

        [TestMethod]
        public void InputEndpointCollectionGetReceivedSignalAt()
        {
            IOutputEndpoint
                output1 = new OutputEndpoint(),
                output2 = new OutputEndpoint(),
                output3 = new OutputEndpoint(),
                output4 = new OutputEndpoint();

            Int32
                receiveSignal1 = 0,
                receiveSignal2 = 0,
                receiveSignal3 = 0,
                receiveSignal4 = 0;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            IInputEndpointCollection<IInputEndpoint> inputCollection = new InputEndpointCollection<InputEndpoint>(4);
            inputCollection.ConnectAt(0, output1);
            inputCollection.ConnectAt(1, output2);
            inputCollection.ConnectAt(2, output3);
            inputCollection.ConnectAt(3, output4);

            for (var i = 0; i < inputCollection.Count; i++)
            {
                Assert.AreEqual(inputCollection.GetLastReceivedSignalAt(i), 0);
            }

            output1.Produce(1);
            output2.Produce(1);
            output3.Produce(1);
            output4.Produce(1);

            for (var i = 0; i < inputCollection.Count; i++)
            {
                Assert.AreEqual(inputCollection.GetLastReceivedSignalAt(i), 1);
            }

            output1.Produce(0);
            output2.Produce(0);
            output3.Produce(0);
            output4.Produce(0);

            for (var i = 0; i < inputCollection.Count; i++)
            {
                Assert.AreEqual(inputCollection.GetLastReceivedSignalAt(i), 0);
            }

            output1.Produce(1);
            output2.Produce(0);
            output3.Produce(1);
            output4.Produce(0);

            Assert.AreEqual(inputCollection.GetLastReceivedSignalAt(0), 1);
            Assert.AreEqual(inputCollection.GetLastReceivedSignalAt(1), 0);
            Assert.AreEqual(inputCollection.GetLastReceivedSignalAt(2), 1);
            Assert.AreEqual(inputCollection.GetLastReceivedSignalAt(3), 0);

            output1.Produce(0);
            output2.Produce(1);
            output3.Produce(0);
            output4.Produce(1);

            Assert.AreEqual(inputCollection.GetLastReceivedSignalAt(0), 0);
            Assert.AreEqual(inputCollection.GetLastReceivedSignalAt(1), 1);
            Assert.AreEqual(inputCollection.GetLastReceivedSignalAt(2), 0);
            Assert.AreEqual(inputCollection.GetLastReceivedSignalAt(3), 1);
        }

        [TestMethod]
        public void InputEndpointCollectionGetAllReceivedSignals()
        {
            IOutputEndpoint
                output1 = new OutputEndpoint(),
                output2 = new OutputEndpoint(),
                output3 = new OutputEndpoint(),
                output4 = new OutputEndpoint();

            Int32
                receiveSignal1 = 0,
                receiveSignal2 = 0,
                receiveSignal3 = 0,
                receiveSignal4 = 0;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            IInputEndpointCollection<IInputEndpoint> inputCollection = new InputEndpointCollection<InputEndpoint>(4);
            inputCollection.ConnectAt(0, output1);
            inputCollection.ConnectAt(1, output2);
            inputCollection.ConnectAt(2, output3);
            inputCollection.ConnectAt(3, output4);

            var lastSignals = inputCollection.GetLastReceivedSignals();
            foreach (var signal in lastSignals)
            {
                Assert.AreEqual(signal, 0);
            }

            output1.Produce(1);
            output2.Produce(1);
            output3.Produce(1);
            output4.Produce(1);

            lastSignals = inputCollection.GetLastReceivedSignals();
            foreach (var signal in lastSignals)
            {
                Assert.AreEqual(signal, 1);
            }

            output1.Produce(0);
            output2.Produce(0);
            output3.Produce(0);
            output4.Produce(0);

            lastSignals = inputCollection.GetLastReceivedSignals();
            foreach (var signal in lastSignals)
            {
                Assert.AreEqual(signal, 0);
            }

            output1.Produce(1);
            output2.Produce(0);
            output3.Produce(1);
            output4.Produce(0);

            lastSignals = inputCollection.GetLastReceivedSignals();
            Assert.AreEqual(lastSignals.ElementAt(0), 1);
            Assert.AreEqual(lastSignals.ElementAt(1), 0);
            Assert.AreEqual(lastSignals.ElementAt(2), 1);
            Assert.AreEqual(lastSignals.ElementAt(3), 0);

            output1.Produce(0);
            output2.Produce(1);
            output3.Produce(0);
            output4.Produce(1);

            lastSignals = inputCollection.GetLastReceivedSignals();
            Assert.AreEqual(lastSignals.ElementAt(0), 0);
            Assert.AreEqual(lastSignals.ElementAt(1), 1);
            Assert.AreEqual(lastSignals.ElementAt(2), 0);
            Assert.AreEqual(lastSignals.ElementAt(3), 1);
        }
        #endregion

        #region Output Endpoint Collection
        [TestMethod]
        public void OutputEndpointCollectionIndexer()
        {
            IOutputEndpointCollection<IOutputEndpoint> outputEndpoints = new OutputEndpointCollection<OutputEndpoint>(3);

            Assert.IsNotNull(outputEndpoints[0]);
            Assert.IsNotNull(outputEndpoints[1]);
            Assert.IsNotNull(outputEndpoints[2]);

            Assert.IsInstanceOfType(outputEndpoints[0], typeof(IOutputEndpoint));
            Assert.IsInstanceOfType(outputEndpoints[1], typeof(IOutputEndpoint));
            Assert.IsInstanceOfType(outputEndpoints[2], typeof(IOutputEndpoint));

            Assert.AreNotSame(outputEndpoints[0], outputEndpoints[1]);
            Assert.AreNotSame(outputEndpoints[0], outputEndpoints[2]);
            Assert.AreNotSame(outputEndpoints[1], outputEndpoints[2]);
        }

        [TestMethod]
        public void OutputEndpointCollectionCreateNew()
        {
            IOutputEndpointCollection<IOutputEndpoint> outputEndpoints = new OutputEndpointCollection<OutputEndpoint>(8);

            Assert.IsNotNull(outputEndpoints);

            foreach (var output in outputEndpoints)
            {
                Assert.IsNotNull(output);
                Assert.IsInstanceOfType(output, typeof(OutputEndpoint));
                Assert.IsNull(output.ConnectedPoint);
            }
        }

        [TestMethod]
        public void OutputEndpointCollectionProduceAllWithSameValue()
        {
            Int32
                receiveSignal1 = 0,
                receiveSignal2 = 0,
                receiveSignal3 = 0,
                receiveSignal4 = 0;
            IInputEndpoint
                input1 = new InputEndpoint(),
                input2 = new InputEndpoint(),
                input3 = new InputEndpoint(),
                input4 = new InputEndpoint();
            input1.Receive += (sender, signal) => { receiveSignal1 = signal; };
            input2.Receive += (sender, signal) => { receiveSignal2 = signal; };
            input3.Receive += (sender, signal) => { receiveSignal3 = signal; };
            input4.Receive += (sender, signal) => { receiveSignal4 = signal; };

            IOutputEndpointCollection<IOutputEndpoint> outputEndpoints = new OutputEndpointCollection<OutputEndpoint>(4);
            outputEndpoints.ConnectAt(0, input1);
            outputEndpoints.ConnectAt(1, input2);
            outputEndpoints.ConnectAt(2, input3);
            outputEndpoints.ConnectAt(3, input4);

            outputEndpoints.ProduceAll(new Int32[] { 1, 1, 1, 1 });
            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);

            outputEndpoints.ProduceAll(new Int32[] { 0, 0, 0, 0 });
            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void OutputEndpointCollectionProduceAllWithInsufficientValues()
        {
            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;
            IInputEndpoint
                input1 = new InputEndpoint(),
                input2 = new InputEndpoint(),
                input3 = new InputEndpoint(),
                input4 = new InputEndpoint();
            input1.Receive += (sender, signal) => { receiveSignal1 = signal; };
            input2.Receive += (sender, signal) => { receiveSignal2 = signal; };
            input3.Receive += (sender, signal) => { receiveSignal3 = signal; };
            input4.Receive += (sender, signal) => { receiveSignal4 = signal; };

            IOutputEndpointCollection<IOutputEndpoint> outputEndpoints = new OutputEndpointCollection<OutputEndpoint>(4);
            outputEndpoints.ConnectAt(0, input1);
            outputEndpoints.ConnectAt(1, input2);
            outputEndpoints.ConnectAt(2, input3);
            outputEndpoints.ConnectAt(3, input4);

            outputEndpoints.ProduceAll(new Int32[] { 1, 0, 1 });

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 0);

            outputEndpoints.ProduceAll(new Int32[] { 0 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void OutputEndpointCollectionProduceAllWithSuperfluousValues()
        {
            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;
            IInputEndpoint
                input1 = new InputEndpoint(),
                input2 = new InputEndpoint(),
                input3 = new InputEndpoint(),
                input4 = new InputEndpoint();
            input1.Receive += (sender, signal) => { receiveSignal1 = signal; };
            input2.Receive += (sender, signal) => { receiveSignal2 = signal; };
            input3.Receive += (sender, signal) => { receiveSignal3 = signal; };
            input4.Receive += (sender, signal) => { receiveSignal4 = signal; };

            IOutputEndpointCollection<IOutputEndpoint> outputEndpoints = new OutputEndpointCollection<OutputEndpoint>(4);
            outputEndpoints.ConnectAt(0, input1);
            outputEndpoints.ConnectAt(1, input2);
            outputEndpoints.ConnectAt(2, input3);
            outputEndpoints.ConnectAt(3, input4);

            outputEndpoints.ProduceAll(new Int32[] { 0, 0, 1, 1, 1, 0, 1 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);
        }

        [TestMethod]
        public void OutputEndpointCollectionProduceAllWithEmptyValue()
        {
            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;
            IInputEndpoint
                input1 = new InputEndpoint(),
                input2 = new InputEndpoint(),
                input3 = new InputEndpoint(),
                input4 = new InputEndpoint();
            input1.Receive += (sender, signal) => { receiveSignal1 = signal; };
            input2.Receive += (sender, signal) => { receiveSignal2 = signal; };
            input3.Receive += (sender, signal) => { receiveSignal3 = signal; };
            input4.Receive += (sender, signal) => { receiveSignal4 = signal; };

            IOutputEndpointCollection<IOutputEndpoint> outputEndpoints = new OutputEndpointCollection<OutputEndpoint>(4);
            outputEndpoints.ConnectAt(0, input1);
            outputEndpoints.ConnectAt(1, input2);
            outputEndpoints.ConnectAt(2, input3);
            outputEndpoints.ConnectAt(3, input4);

            outputEndpoints.ProduceAll();

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void OutputEndpointCollectionProduceAllWithVariousValues()
        {
            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;
            IInputEndpoint
                input1 = new InputEndpoint(),
                input2 = new InputEndpoint(),
                input3 = new InputEndpoint(),
                input4 = new InputEndpoint();
            input1.Receive += (sender, signal) => { receiveSignal1 = signal; };
            input2.Receive += (sender, signal) => { receiveSignal2 = signal; };
            input3.Receive += (sender, signal) => { receiveSignal3 = signal; };
            input4.Receive += (sender, signal) => { receiveSignal4 = signal; };

            IOutputEndpointCollection<IOutputEndpoint> outputEndpoints = new OutputEndpointCollection<OutputEndpoint>(4);
            outputEndpoints.ConnectAt(0, input1);
            outputEndpoints.ConnectAt(1, input2);
            outputEndpoints.ConnectAt(2, input3);
            outputEndpoints.ConnectAt(3, input4);

            outputEndpoints.ProduceAll(new Int32[] { 1, 0, 1, 0 });

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 0);

            outputEndpoints.ProduceAll(new Int32[] { 0, 1, 0, 1 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 1);

            outputEndpoints.ProduceAll(new Int32[] { 1, 0, 0, 1 });

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 1);

            outputEndpoints.ProduceAll(new Int32[] { 0, 1, 1, 0 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void OutputEndpointCollectionProduceAt()
        {
            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;
            IInputEndpoint
                input1 = new InputEndpoint(),
                input2 = new InputEndpoint(),
                input3 = new InputEndpoint(),
                input4 = new InputEndpoint();
            input1.Receive += (sender, signal) => { receiveSignal1 = signal; };
            input2.Receive += (sender, signal) => { receiveSignal2 = signal; };
            input3.Receive += (sender, signal) => { receiveSignal3 = signal; };
            input4.Receive += (sender, signal) => { receiveSignal4 = signal; };

            IOutputEndpointCollection<IOutputEndpoint> outputEndpoints = new OutputEndpointCollection<OutputEndpoint>(4);
            outputEndpoints.ConnectAt(0, input1);
            outputEndpoints.ConnectAt(1, input2);
            outputEndpoints.ConnectAt(2, input3);
            outputEndpoints.ConnectAt(3, input4);

            outputEndpoints.ProduceAt(1, 1);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);

            outputEndpoints.ProduceAt(3, 1);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 1);

            outputEndpoints.ProduceAt(2, 1);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);

            outputEndpoints.ProduceAt(1, 0);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);
        }

        [TestMethod]
        public void OutputEndpointCollectionGetSentSignalAt()
        {
            IOutputEndpointCollection<IOutputEndpoint> outputEndpoints = new OutputEndpointCollection<OutputEndpoint>(4);
            outputEndpoints.ProduceAll(new Int32[] {1, 1, 1, 1});
            for (var i = 0; i < outputEndpoints.Count; i++)
            {
                Assert.AreEqual(outputEndpoints.GetLastSentSignalAt(i), 1);
            }

            outputEndpoints.ProduceAll();
            for (var i = 0; i < outputEndpoints.Count; i++)
            {
                Assert.AreEqual(outputEndpoints.GetLastSentSignalAt(i), 0);
            }

            outputEndpoints.ProduceAt(1, 1);
            outputEndpoints.ProduceAt(3, 1);
            Assert.AreEqual(outputEndpoints.GetLastSentSignalAt(0), 0);
            Assert.AreEqual(outputEndpoints.GetLastSentSignalAt(1), 1);
            Assert.AreEqual(outputEndpoints.GetLastSentSignalAt(2), 0);
            Assert.AreEqual(outputEndpoints.GetLastSentSignalAt(3), 1);

            outputEndpoints.ProduceAll(new Int32[] { 1, 0, 1, 0 });
            Assert.AreEqual(outputEndpoints.GetLastSentSignalAt(0), 1);
            Assert.AreEqual(outputEndpoints.GetLastSentSignalAt(1), 0);
            Assert.AreEqual(outputEndpoints.GetLastSentSignalAt(2), 1);
            Assert.AreEqual(outputEndpoints.GetLastSentSignalAt(3), 0);
        }

        [TestMethod]
        public void OutputEndpointCollectionGetAllSentSignals()
        {
            IOutputEndpointCollection<IOutputEndpoint> outputEndpoints = new OutputEndpointCollection<OutputEndpoint>(4);
            outputEndpoints.ProduceAll(new Int32[] { 1, 1, 1, 1 });
            var lastSentSignals = outputEndpoints.GetLastSentSignals();
            for (var i = 0; i < outputEndpoints.Count; i++)
            {
                Assert.AreEqual(lastSentSignals.ElementAt(i), 1);
            }

            outputEndpoints.ProduceAll();
            lastSentSignals = outputEndpoints.GetLastSentSignals();
            for (var i = 0; i < outputEndpoints.Count; i++)
            {
                Assert.AreEqual(lastSentSignals.ElementAt(i), 0);
            }

            outputEndpoints.ProduceAt(1, 1);
            outputEndpoints.ProduceAt(3, 1);
            lastSentSignals = outputEndpoints.GetLastSentSignals();
            Assert.AreEqual(lastSentSignals.ElementAt(0), 0);
            Assert.AreEqual(lastSentSignals.ElementAt(1), 1);
            Assert.AreEqual(lastSentSignals.ElementAt(2), 0);
            Assert.AreEqual(lastSentSignals.ElementAt(3), 1);

            outputEndpoints.ProduceAll(new Int32[] { 1, 0, 1, 0 });
            lastSentSignals = outputEndpoints.GetLastSentSignals();
            Assert.AreEqual(lastSentSignals.ElementAt(0), 1);
            Assert.AreEqual(lastSentSignals.ElementAt(1), 0);
            Assert.AreEqual(lastSentSignals.ElementAt(2), 1);
            Assert.AreEqual(lastSentSignals.ElementAt(3), 0);
        }
        #endregion

        #region Neutral Endpoint Collection
        [TestMethod]
        public void NeutralEndpointCollectionIndexer()
        {
            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(3);
            
            Assert.IsNotNull(neutralEndpointCollection[0]);
            Assert.IsNotNull(neutralEndpointCollection[1]);
            Assert.IsNotNull(neutralEndpointCollection[2]);

            Assert.IsInstanceOfType(neutralEndpointCollection[0], typeof(NeutralEndpoint));
            Assert.IsInstanceOfType(neutralEndpointCollection[1], typeof(NeutralEndpoint));
            Assert.IsInstanceOfType(neutralEndpointCollection[2], typeof(NeutralEndpoint));

            Assert.AreNotSame(neutralEndpointCollection[0], neutralEndpointCollection[1]);
            Assert.AreNotSame(neutralEndpointCollection[0], neutralEndpointCollection[2]);
            Assert.AreNotSame(neutralEndpointCollection[1], neutralEndpointCollection[2]);
        }

        [TestMethod]
        public void NeutralEndpointCollectionCreateNew()
        {
            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(8);

            Assert.IsNotNull(neutralEndpointCollection);

            foreach (var neutralEndpoint in neutralEndpointCollection)
            {
                Assert.IsNotNull(neutralEndpoint);
                Assert.IsInstanceOfType(neutralEndpoint, typeof(NeutralEndpoint));
                Assert.IsNull(neutralEndpoint.ConnectedPoint);
            }
        }

        [TestMethod]
        public void NeutralEndpointCollectionRegisterSingleReceiveHandler()
        {
            var receivedCount = 0;
            ReceiveSignalHanlder receiveHandler = (sender, signal) => { receivedCount++; };
            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(5);
            neutralEndpointCollection.RegisterReceiveHandler(receiveHandler);

            var i = 0;
            foreach (var neutralEndpoint in neutralEndpointCollection)
            {
                neutralEndpoint.Transmit(0);
                i++;
                Assert.AreEqual(receivedCount, i);
            }
        }

        [TestMethod]
        public void NeutralEndpointCollectionRegisterReceiveHandlers()
        {
            Int32
                receiveSignal1 = 0,
                receiveSignal2 = 0,
                receiveSignal3 = 0,
                receiveSignal4 = 0;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.RegisterReceiveHandler(handlers);

            foreach (var neutralEndpoint in neutralEndpointCollection)
            {
                neutralEndpoint.Transmit(1);
            }

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Receive signal handlers count are not match to endpoints count.")]
        public void NeutralEndpointCollectionRegisterReceiveHandlersWithUnmatchedCount()
        {
            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => {},
                (sender, signal) => {},
                (sender, signal) => {},
            };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.RegisterReceiveHandler(handlers);
        }

        [TestMethod]
        public void NeutralEndpointCollectionTransimitAllWithSameValue()
        {
            Int32
                receiveSignal1 = 0,
                receiveSignal2 = 0,
                receiveSignal3 = 0,
                receiveSignal4 = 0;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.RegisterReceiveHandler(handlers);

            neutralEndpointCollection.TransmitAll(new Int32[] {1, 1, 1, 1});

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);

            neutralEndpointCollection.TransmitAll(new Int32[] { 0, 0, 0, 0 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void NeutralEndpointCollectionTransimitAllWithInsufficientValues()
        {
            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.RegisterReceiveHandler(handlers);

            neutralEndpointCollection.TransmitAll(new Int32[] { 1, 0, 1});

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 0);

            neutralEndpointCollection.TransmitAll(new Int32[] { 0 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void NeutralEndpointCollectionTransimitAllWithSuperfluousValues()
        {

            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.RegisterReceiveHandler(handlers);

            neutralEndpointCollection.TransmitAll(new Int32[] {0, 0, 1, 1, 1, 0, 1});

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);
        }

        [TestMethod]
        public void NeutralEndpointCollectionTransimitAllWithEmptyValue()
        {
            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.RegisterReceiveHandler(handlers);

            neutralEndpointCollection.TransmitAll();

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void NeutralEndpointCollectionTransimitAllWithVariousValues()
        {
            Int32
                receiveSignal1 = 0,
                receiveSignal2 = 0,
                receiveSignal3 = 0,
                receiveSignal4 = 0;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.RegisterReceiveHandler(handlers);

            neutralEndpointCollection.TransmitAll(new Int32[] { 1, 0, 1, 0 });

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 0);

            neutralEndpointCollection.TransmitAll(new Int32[] { 0, 1, 0, 1 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 1);

            neutralEndpointCollection.TransmitAll(new Int32[] { 1, 0, 0, 1 });

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 1);

            neutralEndpointCollection.TransmitAll(new Int32[] { 0, 1, 1, 0 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void NeutralEndpointCollectionTransimitAt()
        {
            Int32
                receiveSignal1 = 0,
                receiveSignal2 = 0,
                receiveSignal3 = 0,
                receiveSignal4 = 0;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.RegisterReceiveHandler(handlers);

            neutralEndpointCollection.TransmitAt(1, 1);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);

            neutralEndpointCollection.TransmitAt(3, 1);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 1);

            neutralEndpointCollection.TransmitAt(2, 1);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);

            neutralEndpointCollection.TransmitAt(1, 0);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);
        }

        [TestMethod]
        public void NeutralEndpointCollectionGetReceivedSignalAt()
        {
            IOutputEndpoint
                output1 = new OutputEndpoint(),
                output2 = new OutputEndpoint(),
                output3 = new OutputEndpoint(),
                output4 = new OutputEndpoint();

            Int32
                receiveSignal1 = 0,
                receiveSignal2 = 0,
                receiveSignal3 = 0,
                receiveSignal4 = 0;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.ConnectAt(0, output1);
            neutralEndpointCollection.ConnectAt(1, output2);
            neutralEndpointCollection.ConnectAt(2, output3);
            neutralEndpointCollection.ConnectAt(3, output4);

            for (var i = 0; i < neutralEndpointCollection.Count; i++)
            {
                Assert.AreEqual(neutralEndpointCollection.GetLastReceivedSignalAt(i), 0);
            }

            output1.Produce(1);
            output2.Produce(1);
            output3.Produce(1);
            output4.Produce(1);

            for (var i = 0; i < neutralEndpointCollection.Count; i++)
            {
                Assert.AreEqual(neutralEndpointCollection.GetLastReceivedSignalAt(i), 1);
            }

            output1.Produce(0);
            output2.Produce(0);
            output3.Produce(0);
            output4.Produce(0);

            for (var i = 0; i < neutralEndpointCollection.Count; i++)
            {
                Assert.AreEqual(neutralEndpointCollection.GetLastReceivedSignalAt(i), 0);
            }

            output1.Produce(1);
            output2.Produce(0);
            output3.Produce(1);
            output4.Produce(0);

            Assert.AreEqual(neutralEndpointCollection.GetLastReceivedSignalAt(0), 1);
            Assert.AreEqual(neutralEndpointCollection.GetLastReceivedSignalAt(1), 0);
            Assert.AreEqual(neutralEndpointCollection.GetLastReceivedSignalAt(2), 1);
            Assert.AreEqual(neutralEndpointCollection.GetLastReceivedSignalAt(3), 0);

            output1.Produce(0);
            output2.Produce(1);
            output3.Produce(0);
            output4.Produce(1);

            Assert.AreEqual(neutralEndpointCollection.GetLastReceivedSignalAt(0), 0);
            Assert.AreEqual(neutralEndpointCollection.GetLastReceivedSignalAt(1), 1);
            Assert.AreEqual(neutralEndpointCollection.GetLastReceivedSignalAt(2), 0);
            Assert.AreEqual(neutralEndpointCollection.GetLastReceivedSignalAt(3), 1);
        }

        [TestMethod]
        public void NeutralEndpointCollectionGetAllReceivedSignals()
        {
            IOutputEndpoint
                output1 = new OutputEndpoint(),
                output2 = new OutputEndpoint(),
                output3 = new OutputEndpoint(),
                output4 = new OutputEndpoint();

            Int32
                receiveSignal1 = 0,
                receiveSignal2 = 0,
                receiveSignal3 = 0,
                receiveSignal4 = 0;

            ReceiveSignalHanlder[] handlers = new ReceiveSignalHanlder[]
            {
                (sender, signal) => { receiveSignal1 = signal; },
                (sender, signal) => { receiveSignal2 = signal; },
                (sender, signal) => { receiveSignal3 = signal; },
                (sender, signal) => { receiveSignal4 = signal; }
            };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.ConnectAt(0, output1);
            neutralEndpointCollection.ConnectAt(1, output2);
            neutralEndpointCollection.ConnectAt(2, output3);
            neutralEndpointCollection.ConnectAt(3, output4);

            var lastSignals = neutralEndpointCollection.GetLastReceivedSignals();
            foreach (var signal in lastSignals)
            {
                Assert.AreEqual(signal, 0);
            }

            output1.Produce(1);
            output2.Produce(1);
            output3.Produce(1);
            output4.Produce(1);

            lastSignals = neutralEndpointCollection.GetLastReceivedSignals();
            foreach (var signal in lastSignals)
            {
                Assert.AreEqual(signal, 1);
            }

            output1.Produce(0);
            output2.Produce(0);
            output3.Produce(0);
            output4.Produce(0);

            lastSignals = neutralEndpointCollection.GetLastReceivedSignals();
            foreach (var signal in lastSignals)
            {
                Assert.AreEqual(signal, 0);
            }

            output1.Produce(1);
            output2.Produce(0);
            output3.Produce(1);
            output4.Produce(0);

            lastSignals = neutralEndpointCollection.GetLastReceivedSignals();
            Assert.AreEqual(lastSignals.ElementAt(0), 1);
            Assert.AreEqual(lastSignals.ElementAt(1), 0);
            Assert.AreEqual(lastSignals.ElementAt(2), 1);
            Assert.AreEqual(lastSignals.ElementAt(3), 0);

            output1.Produce(0);
            output2.Produce(1);
            output3.Produce(0);
            output4.Produce(1);

            lastSignals = neutralEndpointCollection.GetLastReceivedSignals();
            Assert.AreEqual(lastSignals.ElementAt(0), 0);
            Assert.AreEqual(lastSignals.ElementAt(1), 1);
            Assert.AreEqual(lastSignals.ElementAt(2), 0);
            Assert.AreEqual(lastSignals.ElementAt(3), 1);
        }

        [TestMethod]
        public void NeutralEndpointCollectionProduceAllWithSameValue()
        {
            Int32
                receiveSignal1 = 0,
                receiveSignal2 = 0,
                receiveSignal3 = 0,
                receiveSignal4 = 0;
            IInputEndpoint
                input1 = new InputEndpoint(),
                input2 = new InputEndpoint(),
                input3 = new InputEndpoint(),
                input4 = new InputEndpoint();
            input1.Receive += (sender, signal) => { receiveSignal1 = signal; };
            input2.Receive += (sender, signal) => { receiveSignal2 = signal; };
            input3.Receive += (sender, signal) => { receiveSignal3 = signal; };
            input4.Receive += (sender, signal) => { receiveSignal4 = signal; };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.ConnectAt(0, input1);
            neutralEndpointCollection.ConnectAt(1, input2);
            neutralEndpointCollection.ConnectAt(2, input3);
            neutralEndpointCollection.ConnectAt(3, input4);

            neutralEndpointCollection.ProduceAll(new Int32[] { 1, 1, 1, 1 });
            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);

            neutralEndpointCollection.ProduceAll(new Int32[] { 0, 0, 0, 0 });
            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void NeutralEndpointCollectionProduceAllWithInsufficientValues()
        {
            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;
            IInputEndpoint
                input1 = new InputEndpoint(),
                input2 = new InputEndpoint(),
                input3 = new InputEndpoint(),
                input4 = new InputEndpoint();
            input1.Receive += (sender, signal) => { receiveSignal1 = signal; };
            input2.Receive += (sender, signal) => { receiveSignal2 = signal; };
            input3.Receive += (sender, signal) => { receiveSignal3 = signal; };
            input4.Receive += (sender, signal) => { receiveSignal4 = signal; };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.ConnectAt(0, input1);
            neutralEndpointCollection.ConnectAt(1, input2);
            neutralEndpointCollection.ConnectAt(2, input3);
            neutralEndpointCollection.ConnectAt(3, input4);

            neutralEndpointCollection.ProduceAll(new Int32[] { 1, 0, 1 });

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 0);

            neutralEndpointCollection.ProduceAll(new Int32[] { 0 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void NeutralEndpointCollectionProduceAllWithSuperfluousValues()
        {
            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;
            IInputEndpoint
                input1 = new InputEndpoint(),
                input2 = new InputEndpoint(),
                input3 = new InputEndpoint(),
                input4 = new InputEndpoint();
            input1.Receive += (sender, signal) => { receiveSignal1 = signal; };
            input2.Receive += (sender, signal) => { receiveSignal2 = signal; };
            input3.Receive += (sender, signal) => { receiveSignal3 = signal; };
            input4.Receive += (sender, signal) => { receiveSignal4 = signal; };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.ConnectAt(0, input1);
            neutralEndpointCollection.ConnectAt(1, input2);
            neutralEndpointCollection.ConnectAt(2, input3);
            neutralEndpointCollection.ConnectAt(3, input4);

            neutralEndpointCollection.ProduceAll(new Int32[] { 0, 0, 1, 1, 1, 0, 1 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);
        }

        [TestMethod]
        public void NeutralEndpointCollectionProduceAllWithEmptyValue()
        {
            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;
            IInputEndpoint
                input1 = new InputEndpoint(),
                input2 = new InputEndpoint(),
                input3 = new InputEndpoint(),
                input4 = new InputEndpoint();
            input1.Receive += (sender, signal) => { receiveSignal1 = signal; };
            input2.Receive += (sender, signal) => { receiveSignal2 = signal; };
            input3.Receive += (sender, signal) => { receiveSignal3 = signal; };
            input4.Receive += (sender, signal) => { receiveSignal4 = signal; };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.ConnectAt(0, input1);
            neutralEndpointCollection.ConnectAt(1, input2);
            neutralEndpointCollection.ConnectAt(2, input3);
            neutralEndpointCollection.ConnectAt(3, input4);

            neutralEndpointCollection.ProduceAll();

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void NeutralEndpointCollectionProduceAllWithVariousValues()
        {
            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;
            IInputEndpoint
                input1 = new InputEndpoint(),
                input2 = new InputEndpoint(),
                input3 = new InputEndpoint(),
                input4 = new InputEndpoint();
            input1.Receive += (sender, signal) => { receiveSignal1 = signal; };
            input2.Receive += (sender, signal) => { receiveSignal2 = signal; };
            input3.Receive += (sender, signal) => { receiveSignal3 = signal; };
            input4.Receive += (sender, signal) => { receiveSignal4 = signal; };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.ConnectAt(0, input1);
            neutralEndpointCollection.ConnectAt(1, input2);
            neutralEndpointCollection.ConnectAt(2, input3);
            neutralEndpointCollection.ConnectAt(3, input4);

            neutralEndpointCollection.ProduceAll(new Int32[] { 1, 0, 1, 0 });

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 0);

            neutralEndpointCollection.ProduceAll(new Int32[] { 0, 1, 0, 1 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 1);

            neutralEndpointCollection.ProduceAll(new Int32[] { 1, 0, 0, 1 });

            Assert.AreEqual(receiveSignal1, 1);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 1);

            neutralEndpointCollection.ProduceAll(new Int32[] { 0, 1, 1, 0 });

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 0);
        }

        [TestMethod]
        public void NeutralEndpointCollectionProduceAt()
        {
            Int32
                receiveSignal1 = 1,
                receiveSignal2 = 1,
                receiveSignal3 = 1,
                receiveSignal4 = 1;
            IInputEndpoint
                input1 = new InputEndpoint(),
                input2 = new InputEndpoint(),
                input3 = new InputEndpoint(),
                input4 = new InputEndpoint();
            input1.Receive += (sender, signal) => { receiveSignal1 = signal; };
            input2.Receive += (sender, signal) => { receiveSignal2 = signal; };
            input3.Receive += (sender, signal) => { receiveSignal3 = signal; };
            input4.Receive += (sender, signal) => { receiveSignal4 = signal; };

            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.ConnectAt(0, input1);
            neutralEndpointCollection.ConnectAt(1, input2);
            neutralEndpointCollection.ConnectAt(2, input3);
            neutralEndpointCollection.ConnectAt(3, input4);

            neutralEndpointCollection.ProduceAt(1, 1);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 0);

            neutralEndpointCollection.ProduceAt(3, 1);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 0);
            Assert.AreEqual(receiveSignal4, 1);

            neutralEndpointCollection.ProduceAt(2, 1);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 1);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);

            neutralEndpointCollection.ProduceAt(1, 0);

            Assert.AreEqual(receiveSignal1, 0);
            Assert.AreEqual(receiveSignal2, 0);
            Assert.AreEqual(receiveSignal3, 1);
            Assert.AreEqual(receiveSignal4, 1);
        }

        [TestMethod]
        public void NeutralEndpointCollectionGetSentSignalAt()
        {
            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.ProduceAll(new Int32[] { 1, 1, 1, 1 });
            for (var i = 0; i < neutralEndpointCollection.Count; i++)
            {
                Assert.AreEqual(neutralEndpointCollection.GetLastSentSignalAt(i), 1);
            }

            neutralEndpointCollection.ProduceAll();
            for (var i = 0; i < neutralEndpointCollection.Count; i++)
            {
                Assert.AreEqual(neutralEndpointCollection.GetLastSentSignalAt(i), 0);
            }

            neutralEndpointCollection.ProduceAt(1, 1);
            neutralEndpointCollection.ProduceAt(3, 1);
            Assert.AreEqual(neutralEndpointCollection.GetLastSentSignalAt(0), 0);
            Assert.AreEqual(neutralEndpointCollection.GetLastSentSignalAt(1), 1);
            Assert.AreEqual(neutralEndpointCollection.GetLastSentSignalAt(2), 0);
            Assert.AreEqual(neutralEndpointCollection.GetLastSentSignalAt(3), 1);

            neutralEndpointCollection.ProduceAll(new Int32[] { 1, 0, 1, 0 });
            Assert.AreEqual(neutralEndpointCollection.GetLastSentSignalAt(0), 1);
            Assert.AreEqual(neutralEndpointCollection.GetLastSentSignalAt(1), 0);
            Assert.AreEqual(neutralEndpointCollection.GetLastSentSignalAt(2), 1);
            Assert.AreEqual(neutralEndpointCollection.GetLastSentSignalAt(3), 0);
        }

        [TestMethod]
        public void NeutralEndpointCollectionGetAllSentSignals()
        {
            INeutralEndpointCollection<INeutralEndpoint> neutralEndpointCollection = new NeutralEndpointCollection<NeutralEndpoint>(4);
            neutralEndpointCollection.ProduceAll(new Int32[] { 1, 1, 1, 1 });
            var lastSentSignals = neutralEndpointCollection.GetLastSentSignals();
            for (var i = 0; i < neutralEndpointCollection.Count; i++)
            {
                Assert.AreEqual(lastSentSignals.ElementAt(i), 1);
            }

            neutralEndpointCollection.ProduceAll();
            lastSentSignals = neutralEndpointCollection.GetLastSentSignals();
            for (var i = 0; i < neutralEndpointCollection.Count; i++)
            {
                Assert.AreEqual(lastSentSignals.ElementAt(i), 0);
            }

            neutralEndpointCollection.ProduceAt(1, 1);
            neutralEndpointCollection.ProduceAt(3, 1);
            lastSentSignals = neutralEndpointCollection.GetLastSentSignals();
            Assert.AreEqual(lastSentSignals.ElementAt(0), 0);
            Assert.AreEqual(lastSentSignals.ElementAt(1), 1);
            Assert.AreEqual(lastSentSignals.ElementAt(2), 0);
            Assert.AreEqual(lastSentSignals.ElementAt(3), 1);

            neutralEndpointCollection.ProduceAll(new Int32[] { 1, 0, 1, 0 });
            lastSentSignals = neutralEndpointCollection.GetLastSentSignals();
            Assert.AreEqual(lastSentSignals.ElementAt(0), 1);
            Assert.AreEqual(lastSentSignals.ElementAt(1), 0);
            Assert.AreEqual(lastSentSignals.ElementAt(2), 1);
            Assert.AreEqual(lastSentSignals.ElementAt(3), 0);
        }
        #endregion
    }
}
