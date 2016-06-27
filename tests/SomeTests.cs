using NUnit.Framework;
using AssemblyClient;

namespace AllTheTests
{
    [TestFixture]
    public class SomeTests
    {
        [TestCase(true)]
        public void CanAddNumbers(bool expected)
        {
            Assert.That(true, Is.EqualTo(expected));
        }

        [TestCase(1, 2, 3)]
        public void ClientAddsNumbers(int x, int y, int expected)
        {
            var client = new AssemblyApiClient();
            var result = client.AddNumbers(1, 2);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}