using SimpleConsoleApp.Services;
using System.Threading.Tasks;

namespace SimpleConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var appFacade = new AppFacade(new FileStreamService(), new CharacterChunkService());
            await appFacade.RunAsync(args);
        }
    }
}
