using System.Collections.Generic;
using System.Threading.Tasks;
using Engineers.Models;

namespace Engineers.IService
{
    public interface IUserService
    {
        public Task<bool> LogIn(string phone, string password, bool rememberMe);

        public List<User> GetUsers();

        public Task<User> GetById(string userId);

        public Task<User> GetByName(string userName);

        public Task<string> Register(ApiUser apiUser, string password);

        public Task<string> Update(string userId, ApiUser apiUser);

        public Task<string> Delete(string userId);

        public Task<string> EditPassword(string userId, string newPassword, string oldPassword);
    }
}
