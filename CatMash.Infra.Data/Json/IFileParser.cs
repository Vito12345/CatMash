using System.Threading.Tasks;

namespace CatMash.Infra.Data.Json
{
    public interface IFileParser
    {
        Task<string> ReadFileAsync(string filePath);
        Task WriteFileAsync(string filePath, string jsonContent);
    }
}