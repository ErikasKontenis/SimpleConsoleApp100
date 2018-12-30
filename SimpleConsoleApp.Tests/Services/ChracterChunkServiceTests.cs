using NUnit.Framework;
using SimpleConsoleApp.Services;
using System;

namespace SimpleConsoleApp.Tests.Services
{
    [TestFixture]
    public class CharacterChunkServiceTests
    {
        private ICharacterChunkService _characterChunkService;

        [SetUp]
        public void Setup()
        {
            _characterChunkService = new CharacterChunkService();
        }

        [Test]
        [TestCaseSource(nameof(ChunkTestCases))]
        public void ShouldGivenChunkSourceSuccess(ChunkTestCase testCase)
        {
            var result = _characterChunkService.Chunk(testCase.Lines, testCase.ChunkSize);
            Assert.That(result, Is.EqualTo(testCase.Result));
        }

        [Test]
        [TestCase(new object[] { null })]
        [TestCase(new object[] { new string[] { } })]
        public void WhenChunkLinesNullOrEmptyException(string[] lines)
        {
            var exception = Assert.Throws<ArgumentException>(() => _characterChunkService.Chunk(lines, 5));
            Assert.That(exception.Message, Is.EqualTo("lines parameter cannot be null or empty"));
        }

        [Test]
        [TestCase(new object[] { 0 })]
        [TestCase(new object[] { -99 })]
        public void WhenChunkSizeLesserThanOneException(int chunkSize)
        {
            var exception = Assert.Throws<ArgumentException>(() => _characterChunkService.Chunk(new string[] { "123" }, chunkSize));
            Assert.That(exception.Message, Is.EqualTo("chunkSize parameter must be greater than 0"));
        }

        private static ChunkTestCase[] ChunkTestCases()
        {
            return new ChunkTestCase[]
            {
                new ChunkTestCase()
                {
                    ChunkSize = 13,
                    Lines = new string[] { "Green metal stick" },
                    Result = $"Green metal{Environment.NewLine}stick"
                },
                new ChunkTestCase()
                {
                    ChunkSize = 7,
                    Lines = new string[] { "Establishment of the", "church" },
                    Result = $"Establi{Environment.NewLine}shment{Environment.NewLine}of the{Environment.NewLine}church"
                },
                new ChunkTestCase()
                {
                    ChunkSize = 9999,
                    Lines = new string[] { "Lorem ipsum", "dolor sit amet" },
                    Result = $"Lorem ipsum{Environment.NewLine}dolor sit amet"
                },
                new ChunkTestCase()
                {
                    ChunkSize = 3,
                    Lines = new string[] { "1234", "1", "1234" },
                    Result = $"123{Environment.NewLine}4 1{Environment.NewLine}123{Environment.NewLine}4"
                },
                new ChunkTestCase()
                {
                    ChunkSize = 1,
                    Lines = new string[] { "hello", "world" },
                    Result = $"h{Environment.NewLine}e{Environment.NewLine}l{Environment.NewLine}l{Environment.NewLine}o{Environment.NewLine}w{Environment.NewLine}o{Environment.NewLine}r{Environment.NewLine}l{Environment.NewLine}d"
                },
                new ChunkTestCase()
                {
                    ChunkSize = 1,
                    Lines = new string[] { "hello world" },
                    Result = $"h{Environment.NewLine}e{Environment.NewLine}l{Environment.NewLine}l{Environment.NewLine}o{Environment.NewLine}w{Environment.NewLine}o{Environment.NewLine}r{Environment.NewLine}l{Environment.NewLine}d"
                },
            };
        }
    }

    public class ChunkTestCase
    {
        public int ChunkSize { get; set; }

        public string[] Lines { get; set; }

        public string Result { get; set; }
    }
}
