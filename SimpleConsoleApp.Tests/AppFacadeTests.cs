using Moq;
using NUnit.Framework;
using SimpleConsoleApp.Services;
using System;
using System.Threading.Tasks;

namespace SimpleConsoleApp.Tests
{
    [TestFixture]
    public class AppFacadeTests
    {
        private Mock<IFileStreamService> _fileStreamService;
        private Mock<ICharacterChunkService> _characterChunkService;
        private AppFacade _appFacade;

        [SetUp]
        public void Setup()
        {
            _fileStreamService = new Mock<IFileStreamService>();
            _characterChunkService = new Mock<ICharacterChunkService>();
            _appFacade = new AppFacade(_fileStreamService.Object, _characterChunkService.Object);
        }

        [Test]
        [TestCase(new object[] { new string[] { "filePath", "5" } })]
        public async Task WhenAppFacadeRunsSuccessfully(string[] args)
        {
            _fileStreamService
                .Setup(o => o.ReadFileAsync(args[0]))
                .ReturnsAsync(new string[] { "one line" });

            _characterChunkService
                .Setup(o => o.Chunk(new string[] { "one line" }, int.Parse(args[1])))
                .Returns("result");

            await _appFacade.RunAsync(args);

            _fileStreamService.Verify(o => o.ReadFileAsync(args[0]), Times.Once);
            _characterChunkService.Verify(o => o.Chunk(new string[] { "one line" }, int.Parse(args[1])), Times.Once);
            _fileStreamService.Verify(o => o.WriteFileAsync(It.IsAny<string>(), "result"), Times.Once);
        }

        [Test]
        [TestCase(new object[] { new string[] { } })]
        [TestCase(new object[] { new string[] { "" } })]
        [TestCase(new object[] { new string[] { "", "", "" } })]
        public void WhenArgumentsCountIncorrectException(string[] args)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _appFacade.RunAsync(args));
            Assert.That(exception.Message, Is.EqualTo("Console app accepts only two arguments: file source path and the maximum number of characters in one line."));
        }
    }
}
