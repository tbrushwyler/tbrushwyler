using System;
using NUnit.Framework;

namespace cumulo.ninja.landing.test
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void TrueTest()
        {
            Assert.AreEqual(true, true);
        }

        [Test]
        public void FalseTest()
        {
            Assert.AreEqual(true, false);
        }
    }
}
