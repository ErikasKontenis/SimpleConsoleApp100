using NUnit.Framework;
using SimpleConsoleApp.Extensions;
using System.Text;

namespace SimpleConsoleApp.Tests.Extensions
{
    [TestFixture]
    public class StringBuilderExtensionsTests
    {
        [Test]
        [TestCase(new object[] { "one language", 4, "language" })]
        [TestCase(new object[] { "1 language", 0, "1 language" })]
        [TestCase(new object[] { "1 language", -1, "" })]
        public void WhenStringBuilderToStringSuccess(string source, int startIndex, string result)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(source);

            Assert.That(stringBuilder.ToString(startIndex), Is.EqualTo(result));
        }

        [Test]
        [TestCase(new object[] { "one language", 'a', 9 })]
        [TestCase(new object[] { "one language", '1', -1 })]
        [TestCase(new object[] { "", 'a', -1 })]
        public void WhenStringBuilderLastIndexOfSuccess(string source, char value, int result)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(source);

            Assert.That(stringBuilder.LastIndexOf(value), Is.EqualTo(result));
        }
    }
}
