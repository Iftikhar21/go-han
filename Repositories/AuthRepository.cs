using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Data;
using go_han.Interface;
using go_han.Models;
using go_han.Repositories.IRepository;

namespace go_han.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _context;
        private readonly IPasswordUtils _passwordUtils;
        private readonly IUserRepository _userRepository;

        public AuthRepository(
            AppDbContext context,
            IPasswordUtils passwordUtils,
            IUserRepository userRepository
            )
        {
            this._context = context;
            this._passwordUtils = passwordUtils;
            this._userRepository = userRepository;
        }

        public async Task<User> Register(User user)
        {
            var existUser = await _userRepository.GetUserByEmailAsync(user.Email);
            if (existUser != null)
                return null!;

            user.PasswordHash = _passwordUtils.HashPassword(user.PasswordHash);
            await _userRepository.CreatedUserAsync(user);

            return user;
        }

        public async Task<User?> Login(string username, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(username);
            if (user == null)
                return null;

            if (!_passwordUtils.VerifyPassword(password, user.PasswordHash))
                return null;

            return user;
        }
    }
}