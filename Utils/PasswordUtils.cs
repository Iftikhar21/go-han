using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Interface;
using go_han.Models;
using Microsoft.AspNetCore.Identity;

namespace go_han.Utils
{
    public class PasswordUtils : IPasswordUtils
    {
        private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null!, password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null!, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}