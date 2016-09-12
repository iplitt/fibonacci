using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace plitt.task.UnitTests
{
    [TestFixture]
    public class CustomTaskTest
    {
        private CustomTask _testObject;

        [SetUp]
        public void SetUp()
        {
            _testObject = new CustomTask();
        }

        [Test]
        public void Start_StartsTheTask()
        {
            _testObject.Start(20, 100);
            Thread.Sleep(10);
            Assert.That(_testObject.Called, Is.EqualTo(false));
            Assert.That(_testObject.TimedOut, Is.EqualTo(false));
        }

        [Test]
        public void Cancel_CancelsTheTask()
        {
            _testObject.Start(1000, 100);
            Thread.Sleep(20);
            _testObject.Cancel();
            Thread.Sleep(20);
            Assert.That(_testObject.Called, Is.EqualTo(false));
            Assert.That(_testObject.TimedOut, Is.EqualTo(false));
        }

        [Test]
        public void Start_LongRunningTaskAbortsDueToTimeoutAndCallbackIsNotCalled()
        {
            _testObject.Start(5000, 10);
            Thread.Sleep(100);
            Assert.That(_testObject.TimedOut, Is.EqualTo(true));
            Assert.That(_testObject.Called, Is.EqualTo(false));
        }

    }
}
