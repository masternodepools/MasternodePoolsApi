using MasternodePools.Data.Entities;
using System.Threading.Tasks;

namespace MasternodePools.Data.Services.Abstraction
{
    public interface IEntityService<T>
    {
        Task CreateAsync(T user);

        Task<T> GetAsync(string id);
    }
}
