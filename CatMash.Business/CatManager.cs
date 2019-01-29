using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatMash.Domain;
using CatMash.Infra.Data;

namespace CatMash.Business
{
    public class CatManager : ICatManager
    {
        private readonly ICatsRepository _catsRepository;
        private Lazy<Task<IEnumerable<Cat>>> _lazyCats;

        public CatManager(ICatsRepository catsRepository)
        {
            _catsRepository = catsRepository;
            _lazyCats = new Lazy<Task<IEnumerable<Cat>>>(async () => await GetAllCats());
        }

        public async Task<IEnumerable<Cat>> GetAllCats()
        {
            return await _catsRepository.GetAllCatsAsync();
        }

        public async Task<IEnumerable<Cat>> GetRandomCats(int nbCats)
        {
            var cats = (await _catsRepository.GetAllCatsAsync()).ToArray();
            var catsSize = cats.Count();
            var toReturn = new List<Cat>();
            var storedIdx = new List<int>();

            var random = new Random();
            for (int i = 0; i < nbCats; i++)
            {
                var idx = random.Next(catsSize);
                while (storedIdx.Any(sIdx => sIdx == idx))
                {
                    idx = random.Next(catsSize);
                }
                storedIdx.Add(idx);
                toReturn.Add(cats[idx]);
            }

            return toReturn;
        }

        public async Task RegisterVote(Cat cat)
        {
            if (string.IsNullOrWhiteSpace(cat.Id))
            {
                throw new Exception("Id is mandatory.");
            }

            await _catsRepository.IncrementVote(cat);
        }
    }
}
