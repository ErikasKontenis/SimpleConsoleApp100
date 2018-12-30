using SimpleConsoleApp.Services;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SimpleConsoleApp
{
    public class AppFacade
    {
        private const string RESULT_FILE_NAME = "result.txt";
        private readonly IFileStreamService _fileStreamService;
        private readonly ICharacterChunkService _characterChunkService;

        public AppFacade(IFileStreamService fileStreamService, ICharacterChunkService characterChunkService)
        {
            _fileStreamService = fileStreamService;
            _characterChunkService = characterChunkService;
        }

        public async Task RunAsync(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentException("Console app accepts only two arguments: file source path and the maximum number of characters in one line.");
            }

            var filePath = args[0];
            var chunkSize = int.TryParse(args[1], out int result) ? result : 0;

            var fileLines = await _fileStreamService.ReadFileAsync(filePath);
            var chunkedContent = _characterChunkService.Chunk(fileLines, chunkSize);
            await _fileStreamService.WriteFileAsync(Path.Combine(AppContext.BaseDirectory, RESULT_FILE_NAME), chunkedContent);

            Console.WriteLine($"All job done! Output file: {Path.Combine(AppContext.BaseDirectory, RESULT_FILE_NAME)}");
        }
    }
}
