using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engineers.Context;
using Engineers.Models;
using Engineers.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Engineers.Service
{
    public class UsersService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsersService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> LogIn(string phone, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(phone, password, rememberMe, false);

            return result.Succeeded;
        }

        public List<User> GetUsers()
        {
            if (_userManager.Users.ToList().Count <= 0)
                return null;

            return _userManager.Users.ToList();
        }

        public async Task<User> GetById(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<string> Register(ApiUser apiUser, string password)
        {
            User oUser = apiUser.ConverToUser();

            var result = await _userManager.CreateAsync(oUser, password);

            if (!result.Succeeded)
                return GetFullError(result);

            return $"Пользователь [{oUser.UserName}] успешно зарегестрирован !";
        }

        public async Task<string> Update(string userId, ApiUser apiUser)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                user.UpdateInfo(apiUser.ConverToUser());

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                    return $"Данные пользователя [{user.UserName}] обновлены !";
                else return GetFullError(result);
            }

            return $"Обновление не выполнено ! [{user.UserName}]: not found";
        }

        public async Task<string> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                    return $"Delete user:{user.UserName} is done !";
                else
                    return GetFullError(result);
            }

            return $"Error delete user:{user.UserName} not found !";
        }

        public async Task<string> EditPassword(string userId, string newPassword, string oldPassword)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                IdentityResult result =
                    await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

                if (result.Succeeded)
                    return "Успешно !";
                else
                    return GetFullError(result);
            }
            else
                return "Пользователь не найден";
        }

        private static string GetFullError(IdentityResult result)
        {
            string fullError = "";

            foreach (var error in result.Errors)
                fullError += $"{error.Description}\r\n";

            return fullError;
        }

        public Task<User> GetByName(string userName)
        {
            return _userManager.FindByNameAsync(userName);
        }
    }
}
