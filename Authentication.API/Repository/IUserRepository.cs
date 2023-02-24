using Authentication.API.Models;

namespace Authentication.API.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUser(string username, string password);

    }
}
