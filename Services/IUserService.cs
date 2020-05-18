using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebTeam6.Data;

namespace WebTeam6.Services
{
    public interface IUserService
    {
        Task<List<User>> Get();
        Task<User> Get(string id);
        Task<User> Add(User user);
        Task<bool> Update(User user);
        Task<User> Delete(string id);
        Task<User> GetAuthorizedUser(Task<AuthenticationState> authenticationStateTask);
    }
}
