using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using go_han.Models;

namespace go_han.Interface
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
    }
}