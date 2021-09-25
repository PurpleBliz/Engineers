using Engineers.Api.Models;
using Engineers.Models;

namespace Engineers.Api.IService
{
    public interface IUserService
    {
        public Response LogIn(string phone, string password, bool rememberMe);

        public Response GetUsers();

        public Response GetById(string userId);

        public Response GetByName(string userName);

        public Response GetOrders(string userId);

        public Response GetReViews(string userId);

        public Response Register(ApiUser apiUser, string password);

        public Response Update(string userId, ApiUser apiUser);

        public Response Delete(string userId);

        public Response EditPassword(string userId, string newPassword, string oldPassword);
    }
}
