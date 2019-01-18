using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatMash.Domain;

namespace CatMash.Infra.Data
{
    /// <summary>
    /// Repository to get and set cat votes through a JSON file
    /// </summary>
    public class CatsRepository : ICatsRepository
    {
        private const string JsonFilePath = "cats.json";

        public async Task IncrementVote(Cat cat)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Cat>> GetAllCatsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
