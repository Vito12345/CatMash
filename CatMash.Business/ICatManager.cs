using System.Collections.Generic;
using System.Threading.Tasks;
using CatMash.Domain;

namespace CatMash.Business
{
    public interface ICatManager
    {
        Task<IEnumerable<Cat>> GetAllCats();
        Task<IEnumerable<Cat>> GetRandomCats(int nbCats);
        Task RegisterVote(Cat cat);
    }
}
