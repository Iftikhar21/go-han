using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace go_han.Interface
{
    public interface IPasswordUtils
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPassword);
    }
}