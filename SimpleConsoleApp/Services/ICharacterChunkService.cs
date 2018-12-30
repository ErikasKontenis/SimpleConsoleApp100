namespace SimpleConsoleApp.Services
{
    public interface ICharacterChunkService
    {
        string Chunk(string[] lines, int chunkSize);
    }
}
