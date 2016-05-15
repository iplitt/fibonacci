using NUnit.Framework;

namespace plitt.fibonacci.UnitTests
{
    [TestFixture]
    public class FibonacciGeneratorUnitTest
    {
        private FibonacciGenerator _testObject;

        [SetUp]
        public void SetUp()
        {
            _testObject = new FibonacciGenerator();
        }


        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 2)]
        [TestCase(4, 3)]
        [TestCase(5, 5)]
        [TestCase(6, 8)]
        [TestCase(47, 2971215073)]
        [TestCase(48, 4807526976)]
        [TestCase(49, 7778742049)]
        public void GetFibonaci_ReturnsValue(int n, long expected)
        {
            var actual = _testObject.GetFibonaci(n);
            Assert.AreEqual(expected, actual);
        }

    }
}
