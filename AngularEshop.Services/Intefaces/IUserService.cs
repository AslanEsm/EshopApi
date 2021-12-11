using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AngularEshop.Entities.Account;
using AngularEshop.Services.DTOs;

namespace AngularEshop.Services.Intefaces
{
    public interface IUserService : IDisposable
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByUserName(string userName);
        Task<bool> IsUserExistById(int id);
        Task<bool> IsPasswordForUserValidAsync(string username, string Password);
        Task UpdateLastLoginDateAsync(User user);
        bool IsUserExistByEmail(string email);
        bool IsUserExistByUserName(string userName);
        Task<IList<string>> GetUserRolesById(int userId);
        Task<bool> IsAdmin(int userId);
        Task<RegisterResult> RegisterUserAsync(RegisterDTO register);
        Task<LoginResult> LoginUserAsync(LoginDTO login, bool checkAdminRole = false);
        Task<User> EditUserInfo(EditUserDto user, int userId);
    }
}