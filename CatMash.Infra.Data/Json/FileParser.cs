using System.IO;
using System.Threading.Tasks;

namespace CatMash.Infra.Data.Json
{
    public class FileParser : IFileParser
    {
        public bool FileExists(string path)
        {
            return File.Exists(path);
        }

        public async Task<string> ReadFileAsync(string filePath)
        {
            string content;
            using (var reader = new StreamReader(filePath))
            {
                content = await reader.ReadToEndAsync();
            }
            return content;
        }

        public async Task WriteFileAsync(string filePath, string jsonContent)
        {
            using (var writer = new StreamWriter(filePath))
            {
                await writer.WriteAsync(jsonContent);
            }
        }
    }
}
