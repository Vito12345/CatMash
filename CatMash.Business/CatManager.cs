using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatMash.Domain;
using CatMash.Infra.Data;

namespace CatMash.Business
{
    public class CatManager : ICatManager
    {
        private readonly ICatsRepository _catsRepository;

        public CatManager(ICatsRepository catsRepository)
        {
            _catsRepository = catsRepository;
        }

        public async Task<IEnumerable<Cat>> GetAllCats()
        {
            throw new NotImplementedException();
        }

        public async Task RegisterVote(Cat cat)
        {
            throw new NotImplementedException();
        }
    }
}
