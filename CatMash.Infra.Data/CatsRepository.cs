using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CatMash.Common.Config;
using CatMash.Domain;
using CatMash.Domain.Configuration;
using CatMash.Infra.Data.DataObject;
using CatMash.Infra.Data.Json;
using Newtonsoft.Json;

namespace CatMash.Infra.Data
{
    /// <summary>
    /// Repository to get and set cat votes through a JSON file
    /// </summary>
    public class CatsRepository : ICatsRepository
    {
        private readonly string _jsonFilePath;
        private readonly IFileParser _fileParser;

        public CatsRepository(IFileParser fileParser, IConfigRepo<CatMashConfig> configRepo)
        {
            _jsonFilePath = configRepo.Configuration.SourceJsonFile;
            _fileParser = fileParser;
        }

        public async Task IncrementVote(Cat cat)
        {
            var cats = await GetCatsFromJsonFileAsync();
            var catToVote = cats.SingleOrDefault(c => c.Id == cat.Id);
            if (catToVote == null)
            {
                throw new System.Exception("The cat you have voted for doesn't exist.");
            }

            catToVote.Votes++;
            await StoreCatsInJsonFileAsync(cats);
        }

        public async Task<IEnumerable<Cat>> GetAllCatsAsync()
        {
            return await GetCatsFromJsonFileAsync();
        }

        #region Private methods

        private async Task<IEnumerable<Cat>> GetCatsFromJsonFileAsync()
        {
            try
            {
                var jsonContent = await _fileParser.ReadFileAsync(_jsonFilePath);
                var original = JsonConvert.DeserializeObject<JsonCat>(jsonContent);
                return original?.Images ?? new List<Cat>();
            }
            catch (IOException ex)
            {
                //_logger.Error("Error while getting cats from file", ex);
                return new List<Cat>();
            }
            catch (JsonException ex)
            {
                //_logger.Error("Error while parse cats from Json file", ex);
                return new List<Cat>();
            }
        }

        private async Task StoreCatsInJsonFileAsync(IEnumerable<Cat> cats)
        {
            try
            {
                var jsonContent = JsonConvert.SerializeObject(new JsonCat { Images = cats });
                await _fileParser.WriteFileAsync(_jsonFilePath, jsonContent);
            }
            catch (IOException ex)
            {
                //_logger.Error("Error while writing cats to Json file", ex);
            }
        }

        #endregion
    }
}
