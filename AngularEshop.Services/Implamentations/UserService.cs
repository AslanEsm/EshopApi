using AngularEshop.Common.Exceptions;
using AngularEshop.Common.Utilities;
using AngularEshop.Data.Contracts;
using AngularEshop.Entities.Access;
using AngularEshop.Entities.Account;
using AngularEshop.Services.DTOs;
using AngularEshop.Services.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularEshop.Services.Implamentations
{
    public class UserService : IUserService
    {
        #region Constructor

        private readonly IRepository<User> _useRepository;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManage;
        private readonly IRepository<Role> _roleRepository;
        private readonly RoleManager<Role> _roleManager;

        public UserService(
            IRepository<User> useRepository,
            UserManager<User> userManager,
            SignInManager<User> signInManage,
            IRepository<Role> roleRepository,
            RoleManager<Role> roleManager)
        {
            _useRepository = useRepository;
            _userManager = userManager;
            _signInManage = signInManage;
            _roleRepository = roleRepository;
            _roleManager = roleManager;
        }

        #endregion Constructor

        #region User

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _useRepository.TableNoTracking.ToListAsync();
            return users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _userManager
                .FindByIdAsync(id.ToString());

            if (user == null)
                throw new NotFoundException("کاربری با این مشخصات وجود ندارد.");

            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email.ToLower().Trim());
            if (user == null)
                throw new NotFoundException("کاربری با این مشخصات وجود ندارد.");

            return user;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                throw new NotFoundException("کاربری با این مشخصات وجود ندارد.");

            return user;
        }

        public async Task<bool> IsPasswordForUserValidAsync(string username, string Password)
        {
            var user = await GetUserByUserName(username);
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, Password);
            if (!isPasswordValid)
                throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

            return true;
        }

        public Task UpdateLastLoginDateAsync(User user)
        {
            user.LastLoginDate = DateTimeOffset.Now;
            return _userManager.UpdateAsync(user);
        }

        public bool IsUserExistByEmail(string email)
        {
            var IsExist = _useRepository.TableNoTracking
                .Any(u => u.Email == email);
            return IsExist;
        }

        public bool IsUserExistByUserName(string userName)
        {
            var IsExist = _useRepository.TableNoTracking
                .Any(u => u.UserName == userName);
            return IsExist;
        }

        public async Task<bool> IsUserExistById(int id)
        {
            var isExist = await _useRepository.TableNoTracking
                .AnyAsync(u => u.Id == id);
            return isExist;
        }

        public async Task<IList<string>> GetUserRolesById(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null)
                throw new NotFoundException("لیست رول ها خالی است");

            return roles;
        }

        public async Task<bool> IsAdmin(int userId)
        {
            var user = await GetUserByIdAsync(userId);

            var isInAdminRole = await _userManager.IsInRoleAsync(user, "Admin");

            return isInAdminRole;
        }

        #endregion User

        #region Register

        public async Task<RegisterResult> RegisterUserAsync(RegisterDTO register)
        {
            if (IsUserExistByEmail(register.Email))
                return RegisterResult.EmailExists;

            if (IsUserExistByUserName(register.UserName))
                return RegisterResult.UserNameExists;

            var user = new User()
            {
                Email = register.Email.SanitizeText(),
                UserName = register.UserName.SanitizeText(),
                Address = register.Address.SanitizeText(),
            };

            var result = await _userManager.CreateAsync(user, register.PasswordHash);

            if (!result.Succeeded)
                return RegisterResult.ServerError;

            return RegisterResult.Success;
        }

        #endregion Register

        #region Login

        public async Task<LoginResult> LoginUserAsync(LoginDTO login, bool checkAdminRole = false)
        {
            var user = await GetUserByUserName(login.UserName);
            var res = await _signInManage.CheckPasswordSignInAsync(user, login.PasswordHash, true);

            if (res.RequiresTwoFactor)
                return LoginResult.RequiresTwoFactor;

            if (res.IsLockedOut)
                return LoginResult.IsLockedOut;

            if (res.IsNotAllowed)
                return LoginResult.IsNotAllowed;

            if (user == null)
                return LoginResult.InCorrectData;

            if (!user.IsActive)
                return LoginResult.NotActivated;

            if (checkAdminRole)
            {
                //check user have admin role
                var isInAdminRole = await IsAdmin(user.Id);

                if (!isInAdminRole)
                {
                    return LoginResult.NotAdmin;
                }

                await _signInManage
                    .PasswordSignInAsync(login.UserName, login.PasswordHash, login.RememberMe, true);

                return LoginResult.Success;
            }

            await _signInManage
               .PasswordSignInAsync(login.UserName, login.PasswordHash, login.RememberMe, true);

            return LoginResult.Success;
        }

        #endregion Login

        #region EditUser

        public async Task<User> EditUserInfo(EditUserDto user, int userId)
        {
            var myUser = await _userManager.FindByIdAsync(userId.ToString());

            if (myUser == null)
                throw new NotFoundException("همچین کاربری وجود ندارد");

            var newUser = new User()
            {
                Address = myUser.Address,
                PhoneNumber = myUser.PhoneNumber,
                LastUpdateDate = DateTime.Now
            };

            var result = await _userManager.UpdateAsync(newUser);

            if (!result.Succeeded)
                throw new LogicException("خطایی اتفاق افتاد لطفا مجددا تلاش کنید");

            return newUser;
        }

        #endregion EditUser

        #region Dispose

        public void Dispose()
        {
            _useRepository?.Dispose();
            _roleRepository?.Dispose();
            _roleManager?.Dispose();
            _userManager?.Dispose();
        }

        #endregion Dispose
    }
}