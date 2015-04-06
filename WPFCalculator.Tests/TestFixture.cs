using NUnit.Framework;
using Wpfa;
using Wpfa.Injection;

namespace WPFCalculator.Tests
{
    [ProcessIsolation("WPFCalculator.exe")]
    [TestFixture]
    public class TestFixture : ProcessIsolationTestBase
    {
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
