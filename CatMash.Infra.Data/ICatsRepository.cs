using System.Collections.Generic;
using System.Threading.Tasks;
using CatMash.Domain;

namespace CatMash.Infra.Data
{
    public interface ICatsRepository
    {
        Task<IEnumerable<Cat>> GetAllCatsAsync();
        Task IncrementVote(Cat cat);
    }
}
