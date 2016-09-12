using NUnit.Framework;
using System.Threading;

namespace plitt.task.UnitTests
{
    [TestFixture]
    public class WebMethodTest
    {
        private WebMethod _testObject;

        [SetUp]
        public void SetUp()
        {
            _testObject = new WebMethod();
        }

        [Test]
        public void AsyncStart_CallsTheCallbackMethod()
        {
            var token = new Token { Done = false };
            _testObject.AsyncStart(1, () => WebMethodCallback(token));
            Assert.That(token.Done, Is.EqualTo(false));
            Thread.Sleep(60);
            Assert.That(token.Done, Is.EqualTo(true));
        }

        [Test]
        public void AsyncStart_IsBusyAfterTwentyMilliseconds()
        {
            var token = new Token { Done = false };
            _testObject.AsyncStart(25, () => WebMethodCallback(token));
            Assert.That(token.Done, Is.EqualTo(false));
            Thread.Sleep(20);
            Assert.That(token.Done, Is.EqualTo(false));
        }

        private void WebMethodCallback(Token token)
        {
            token.Done = true;
        }
    }
}
