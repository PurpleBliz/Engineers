using System.Linq;
using Engineers.Models;
using Engineers.Api.Models;
using Engineers.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Engineers.Api.Service
{
    public class UsersService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly Response response = new();

        public UsersService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

            response = new()
            {
                Code = 200,
                Text = "OK",
                Data = null,
                Success = true
            };
        }

        public Response LogIn(string phone, string password, bool rememberMe)
        {
            var result = _signInManager.PasswordSignInAsync(phone, password, rememberMe, false).Result;

            response.Success = result.Succeeded;

            if (!response.Success) return response.ReturnBADResponse("Вход не выполнен");

            return response;
        }

        public Response GetUsers()
        {
            if (_userManager.Users.ToList().Count <= 0) 
                return response.ReturnBADResponse("Пользователи отсутствуют");

            response.Data = _userManager.Users.ToList();

            return response;
        }

        public Response GetByName(string userName)
        {
            response.Data = _userManager.FindByNameAsync(userName).Result;

            if (response.Data == null)
                return response.ReturnBADResponse($"Пользователь не найден [{userName}]");

            return response;
        }

        public Response GetById(string userId)
        {
            response.Data = 
                _userManager.Users.Include(u => u.Orders).Include(u => u.Reviews).FirstOrDefault(u => u.Id == userId);

            if (response.Data == null) 
                return response.ReturnBADResponse("Пользователь не найден");

            return response;
        }

        public Response Register(ApiUser apiUser, string password)
        {
            response.Data = apiUser;

            if (!apiUser.IsValid()) 
                return response.ReturnBADResponse("Пользователь не может быть зарегестрирован ! проверьте заполнение полей", apiUser);

            User oUser = apiUser.ConverToUser();

            response.Data = oUser;

            var result = _userManager.CreateAsync(oUser, password).Result;

            response.Success = result.Succeeded;

            if (!response.Success)
                return response.ReturnBADResponse(GetFullError(result), oUser);

            return response;
        }

        public Response Update(string userId, ApiUser apiUser)
        {
            var user = _userManager.FindByIdAsync(userId).Result;

            response.Data = user;

            if (user.Role == Roles.ADMIN_EN) 
                return response.ReturnBADResponse("Невозможно что то изменить у администратора", apiUser, 666);

            if (user == null)
                return response.ReturnBADResponse($"Обновление не выполнено ! [{user.UserName}]: not found");

            user.UpdateInfo(apiUser.ConverToUser());

            response.Data = user;

            var result = _userManager.UpdateAsync(user).Result;

            response.Success = result.Succeeded;

            if (!response.Success) response.ReturnBADResponse(GetFullError(result));

            return response;
        }

        public Response Delete(string userId)
        {
            var user = _userManager.FindByIdAsync(userId).Result;

            response.Data = user;

            if (user.Role == Roles.ADMIN_EN)
                return response.ReturnBADResponse("Невозможно удалить администратора", user, 666);

            if (user == null)
                return response.ReturnBADResponse($"Удаление не выполнено ! [{user.UserName}]: not found");

            var result = _userManager.DeleteAsync(user).Result;

            response.Success = result.Succeeded;

            if (!response.Success) response.ReturnBADResponse(GetFullError(result));

            return response;
        }

        public Response EditPassword(string userId, string newPassword, string oldPassword)
        {
            User user = _userManager.FindByIdAsync(userId).Result;

            if (user.Role == Roles.ADMIN_EN)
                return response.ReturnBADResponse("Невозможно поменять пароль у администратора", user, 666);

            var result = _userManager.ChangePasswordAsync(user, oldPassword, newPassword).Result;

            response.Success = result.Succeeded;

            if (!response.Success) response.ReturnBADResponse(GetFullError(result));

            return response;
        }

        private static string GetFullError(IdentityResult result)
        {
            string fullError = "";

            foreach (var error in result.Errors)
                fullError += $"{error.Description}\r\n";

            return fullError;
        }

        public Response GetOrders(string userId)
        {
            response.Data = _userManager.Users.Include(o => o.Orders).FirstOrDefault(user => user.Id == userId);

            if (response.Data is null)
                return response.ReturnBADResponse("Пользователь не найден");

            if ((response.Data as User).Orders is null)
                response.Text = "У данного пользователя нет заказов";

            return response;
        }

        public Response GetReViews(string userId)
        {
            response.Data = _userManager.Users.Include(o => o.Reviews).FirstOrDefault(user => user.Id == userId);

            if (response.Data == null)
                return response.ReturnBADResponse("Пользователь не найден");

            if ((response.Data as User).Reviews is null)
                response.Text = "У данного пользователя нет отзывов";

            return response;
        }
    }
}
