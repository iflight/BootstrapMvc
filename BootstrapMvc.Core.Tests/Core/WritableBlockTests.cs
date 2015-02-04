﻿using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BootstrapMvc.Core
{
    [TestClass]
    public class WritableBlockTests
    {
        private MockRepository mocks;
        private Mock<IBootstrapContext> contextMock;

        [TestInitialize]
        public void Initialize()
        {
            mocks = new MockRepository(MockBehavior.Strict);
            contextMock = mocks.Create<IBootstrapContext>();
        }

        [TestMethod]
        public void Test_WriteTo_Calls_WriteTo_On_NextBlock()
        {
            var b1 = new DummyWritableBlock(contextMock.Object) { Content = "1" };
            var b2 = new DummyWritableBlock(contextMock.Object) { Content = "2" };

            b1.AppendNextBlock(b2);

            using (var sw = new StringWriter())
            {
                b1.WriteTo(sw);
                Assert.AreEqual("12", sw.ToString());
            }
        }

        [TestMethod]
        public void Test_Append_Appends_To_End()
        {
            var b1 = new DummyWritableBlock(contextMock.Object) { Content = "1" };
            var b2 = new DummyWritableBlock(contextMock.Object) { Content = "2" };
            var b3 = new DummyWritableBlock(contextMock.Object) { Content = "3" };

            b1.AppendNextBlock(b2);
            b1.AppendNextBlock(b3);

            using (var sw = new StringWriter())
            {
                b1.WriteTo(sw);
                Assert.AreEqual("123", sw.ToString());
            }
        }

        [TestMethod]
        public void Test_WriteSpaceSuffix()
        {
            var b1 = new DummyWritableBlock(contextMock.Object) { Content = "1" };
            var b2 = new DummyWritableBlock(contextMock.Object) { Content = "2" };

            b1.Append(b2);

            b1.WriteWhitespaceSuffix = true;

            using (var sw = new StringWriter())
            {
                b1.WriteTo(sw);
                Assert.AreEqual("1 2", sw.ToString());
            }
        }
    }
}
