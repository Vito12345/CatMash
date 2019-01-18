using System.Collections.Generic;
using System.Threading.Tasks;
using CatMash.Domain;

namespace CatMash.Business
{
    public interface ICatManager
    {
        Task<IEnumerable<Cat>> GetAllCats();
        Task RegisterVote(Cat cat);
    }
}