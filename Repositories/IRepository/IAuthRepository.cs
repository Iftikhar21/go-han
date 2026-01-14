using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Models;

namespace go_han.Repositories.IRepository
{
    public interface IAuthRepository
    {
        public Task<User> Register(User user);
        public Task<User?> Login(string username, string password);
    }
}