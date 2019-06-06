using MasternodePools.Data.Context;
using MasternodePools.Data.Entities;
using MasternodePools.Data.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MasternodePools.Data.Services
{
    public class UserService : IEntityService<User>
    {
        private UserContext _userContext;

        public UserService(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task CreateAsync(User user)
        {
            _userContext.Users.Add(user);
            await _userContext.SaveChangesAsync();
        }
         
        public async Task<User> GetAsync(string id)
            => await _userContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }
}
