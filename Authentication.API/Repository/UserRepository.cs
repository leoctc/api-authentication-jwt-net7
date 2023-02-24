using Authentication.API.Models;
using Authentication.API.Repository;

namespace Authentication.API.Data
{
    public class UserRepository: IUserRepository
    {
        public async Task<User> GetUser(string username, string password)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "admin", Password = "123", Role = "admin" },
                new User { Id = 2, Username = "leonardo", Password = "123", Role = "employee" }
            };

            return users.Where(x => x.Username.ToLower() == username.ToLower() 
                                 && x.Password == password.ToLower())
                        .FirstOrDefault();
        }

    }
}
