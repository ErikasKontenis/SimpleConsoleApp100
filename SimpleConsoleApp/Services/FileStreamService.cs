using System.IO;
using System.Threading.Tasks;

namespace SimpleConsoleApp.Services
{
    public class FileStreamService : IFileStreamService
    {
        public async Task<string[]> ReadFileAsync(string filePath)
        {
            return await File.ReadAllLinesAsync(filePath);
        }

        public async Task WriteFileAsync(string filePath, string content)
        {
            await File.WriteAllTextAsync(filePath, content);
        }
    }
}
