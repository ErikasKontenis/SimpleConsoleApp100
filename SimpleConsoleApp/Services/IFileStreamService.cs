using System.Threading.Tasks;

namespace SimpleConsoleApp.Services
{
    public interface IFileStreamService
    {
        Task<string[]> ReadFileAsync(string filePath);

        Task WriteFileAsync(string filePath, string content);
    }
}
